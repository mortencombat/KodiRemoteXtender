using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using System.Security.Cryptography;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.IO.Compression;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KodiRemoteXtender
{

    public enum XBMCEvent
    {
        PlayPause = 1,
        Play = 2,
        Pause = 3,
        Stop = 4,

        SubtitleOff = 10,
        SubtitlePrev = 11,
        SubtitleNext = 12,

        AudioPrev = 15,
        AudioNext = 16,

        SkipPrevious = 20,
        SkipBackward = 21,
        SkipForward = 22,
        SkipNext = 23,
        Seek = 29,

        ShowInfo = 40

    }

    public class Log
    {
        private List<LogEntry> logEntries;

        public Log()
        {
            logEntries = new List<LogEntry>();
        }

        public void Clear()
        {
            logEntries.Clear();
        }

        public void LogEvent(EventType Type, string Event, string Action)
        {
            LogEntry entry = new LogEntry(Type, Event, Action);
            logEntries.Add(entry);
            if (view != null)
            {
                if (listTypes.HasFlag(Type))
                {
                    view.AddLogEntry(entry.ListViewItem);
                }
            }
        }

        public enum EventType
        {
            ForwardToExternalPlayer = 1,
            ModifyResponse = 2
        }

        private Log.EventType listTypes;
        public Log.EventType ListEventTypes { get { return listTypes; } set { listTypes = value; } }

        private fSettings view;
        public fSettings View { set { view = value; } }

        public void ResetView()
        {
            if (view == null) return;
            lock (view)
            {
                view.ClearLogEntries();
                foreach (LogEntry entry in logEntries)
                {
                    if (!listTypes.HasFlag(entry.Type)) continue;
                    view.AddLogEntry(entry.ListViewItem);
                }
            }
        }

    }

    public class LogEntry
    {
        private DateTime time; public DateTime Time { get { return time;} }
        private Log.EventType type; public Log.EventType Type { get { return type; } }
        private string eventText; public string Event { get { return eventText; } }
        private string actionText; public string Action { get { return actionText; } }

        public LogEntry(Log.EventType Type, string Event, string Action)
        {
            this.time = DateTime.Now;
            this.type = Type;
            this.eventText = Event;
            this.actionText = Action;
        }

        public ListViewItem ListViewItem 
        { 
            get 
            {
                return new ListViewItem(new string[3] { time.ToString("s"), eventText, actionText });
            }
        }
    }

    public enum PlayerStatus
    {
        NotRunning = 1,
        Unknown = 2,
        Running = 4
    }

    public interface IXtenderTarget
    {
        string Key { get; }
        bool CanSeek { get; }
        bool HasSubtitleInfo { get; }
        bool HasAudiostreamInfo { get; }

        void Initialize(Log Log, Hashtable Settings, fSettings Form);
        string Status { get; }
        PlayerStatus PlayerIsRunning(bool AllowRequests = false);
        bool RaiseEvent(XBMCEvent XBMCEvent, Hashtable XBMCMetadata = null);
        PlayerState GetPlayerState();
        void Shutdown();
    }

    public enum MediaState
    {
        Stopped = 0,
        Paused = 1,
        Playing = 2
    }

    public class Subtitle
    {
        public uint Index { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        public string JSON
        { get { return "{\"index\":" + Index.ToString() + ",\"language\":\"" + Language + "\",\"name\":\"" + Name + "\"}"; } }

        public Subtitle(uint Index, string Language, string Name)
        {
            this.Index = Index;
            this.Language = Language;
            this.Name = Name;
        }
    }

    public class Audiostream
    {
        public uint Index { get; set; }
        public uint Bitrate { get; set; }
        public byte Channels { get; set; }
        public string Codec { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        public string JSON
        { get { return "{\"index\":" + Index.ToString() + ",\"language\":\"" + Language + "\",\"bitrate\":" + Bitrate.ToString() + ",\"channels\":" + Channels.ToString() + ",\"codec\":\"" + Codec + "\",\"name\":\"" + Name + "\"}"; } }

        public Audiostream(uint Index, string Language, byte Channels, string Codec, uint Bitrate, string Name)
        {
            this.Index = Index;
            this.Language = Language;
            this.Channels = Channels;
            this.Codec = Codec;
            this.Bitrate = Bitrate;
            this.Name = Name;
        }
    }

    public class PlayerState
    {
        private IXtenderTarget player;
        public IXtenderTarget Player { get { return player; } }

        public string Filename { get; set; }

        private MediaState state;
        public MediaState State { get { return state; } set { state = value; lastEdited = DateTime.Now; } }
        
        private long time;
        public long Time { get { return time; } set { time = value; lastEdited = DateTime.Now; } }

        private long totalTime;
        public long TotalTime { get { return totalTime; } set { totalTime = value; lastEdited = DateTime.Now; } }

        private Subtitle currentSubtitle;
        public Subtitle CurrentSubtitle { get { return currentSubtitle; } }

        private List<Subtitle> subtitles;
        public List<Subtitle> Subtitles { get { return subtitles; } }

        private Audiostream currentAudiostream;
        public Audiostream CurrentAudiostream { get { return currentAudiostream; } }

        private List<Audiostream> audiostreams;
        public List<Audiostream> Audiostreams { get { return audiostreams; } }

        private DateTime lastEdited;
        public int Age { get { return (int)Math.Round((DateTime.Now - lastEdited).TotalSeconds, 0); } }

        public PlayerState(IXtenderTarget Player)
        {
            player = Player;
        }

        public string GetTimeJSON(Int64 Milliseconds)
        {
            //"time":{"hours":0,"milliseconds":0,"minutes":0,"seconds":0}
            // Returns: {"hours":xx,"milliseconds":xx,"minutes":xx,"seconds":xx}
            int hours = 0, minutes = 0, seconds = 0;
            while (Milliseconds >= 3600000) { hours++; Milliseconds -= 3600000; }
            while (Milliseconds >= 60000) { minutes++; Milliseconds -= 60000; }
            while (Milliseconds >= 1000) { seconds++; Milliseconds -= 1000; }
            return "{\"hours\":" + hours.ToString() + ",\"milliseconds\":" + Milliseconds.ToString() + ",\"minutes\":" + minutes.ToString() + ",\"seconds\":" + seconds.ToString() + "}";
        }

        public string GetTimePercentage()
        {
            System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");
            decimal percentage = Math.Round((decimal)time / (decimal)totalTime * 100, 15);
            return percentage.ToString(provider);
        }

    }

    public class HttpSession
    {
        // Helper class for sending requests and receiving responses, with automatic cookie management for logins, etc.
        private HttpWebRequest request;
        public HttpWebRequest Request { get { return request; } set { request = value; } }

        private HttpWebResponse response;
        public HttpWebResponse Response { get { return response; } }

        private CookieContainer cookiebox;
        public CookieContainer Cookies { get { return cookiebox; } }

        private bool sendMultipart;
        public bool SendMultipart { get { return sendMultipart; } set { sendMultipart = value; } }

        private string requestBody;
        public string RequestBody { get { return requestBody; } set { requestBody = value; } }

        private MemoryStream requestBodyStream;
        public MemoryStream RequestBodyStream { get { return requestBodyStream; } set { requestBodyStream = value; } }

        private byte[] responseBodyRaw;
        public byte[] ResponseBodyRaw { get { return responseBodyRaw; } }

        private string responseBody;
        public string ResponseBody { get { return responseBody; } }

        private HtmlAgilityPack.HtmlDocument htmlDocument;
        public HtmlAgilityPack.HtmlDocument HtmlDocument
        {
            get
            {
                if (htmlDocument == null && responseBody.Length > 0) { htmlDocument = new HtmlAgilityPack.HtmlDocument(); htmlDocument.LoadHtml(responseBody); }
                return htmlDocument;
            }
        }

        public enum RequestResult
        {
            UnknownError = -999,
            RequestUndefined = -900,
            ProtocolError = -2,
            RequestTimedOut = -1,
            ResponseOK = 1
        }

        public HttpSession(string uri = "http://www.google.com", string method = "POST")
        {
            cookiebox = new CookieContainer();
            Reset(uri, method);
        }

        public void Reset(string uri, string method)
        {
            Dispose();
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.72 Safari/537.36";
            request.KeepAlive = true;
            request.ServicePoint.Expect100Continue = false;
            request.AllowAutoRedirect = false;
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;
            request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,da;q=0.6");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Timeout = 30000;
            request.ReadWriteTimeout = 30000;
            request.CookieContainer = cookiebox;
            requestBody = "";
            responseBody = "";
            htmlDocument = null;
        }

        public RequestResult ExecuteRequest()
        {
            if (request == null) return RequestResult.RequestUndefined;
            if (response != null) { response.Close(); }

            if (requestBody.Length > 0 || requestBodyStream != null)
            {
                try
                {
                    if (sendMultipart)
                    {
                        // send as multipart
                        string[] multiparts = Regex.Split(requestBody, @"<!>");
                        byte[] bytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            foreach (string part in multiparts)
                            {
                                //Determine if part is plain text or "<!>" line.
                                if (File.Exists(part))
                                {
                                    bytes = File.ReadAllBytes(part);
                                }
                                else
                                {
                                    bytes = System.Text.Encoding.UTF8.GetBytes(part.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                                }
                                ms.Write(bytes, 0, bytes.Length);
                            }

                            request.ContentLength = ms.Length;
                            using (Stream stream = request.GetRequestStream())
                            {
                                ms.WriteTo(stream);
                            }
                        }
                    }
                    else
                    {
                        // send normally
                        byte[] postBytes = null;

                        if (requestBodyStream != null)
                        { postBytes = requestBodyStream.ToArray(); }
                        else
                        { postBytes = System.Text.Encoding.UTF8.GetBytes(requestBody); }

                        request.ContentLength = postBytes.Length;

                        Stream stream = request.GetRequestStream();
                        stream.Write(postBytes, 0, postBytes.Length);
                        stream.Close();
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError) { return RequestResult.ProtocolError; } else { return RequestResult.UnknownError; }
                }
                catch (Exception)
                {
                    return RequestResult.UnknownError;
                }
            }
            else
            {
                request.ContentLength = 0;
            }

            htmlDocument = null;
            return getResponse();
        }

        private RequestResult getResponse()
        {
            try
            {
                response = (HttpWebResponse)request.GetResponse();

                foreach (Cookie cookie in response.Cookies) cookie.Path = "/";
                cookiebox.Add(response.Cookies);

                Stream stream;
                switch (response.ContentEncoding.ToUpperInvariant())
                {
                    case "GZIP":
                        stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                        break;
                    case "DEFLATE":
                        stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress);
                        break;
                    default:
                        stream = response.GetResponseStream();
                        break;
                }

                var ms = new MemoryStream();
                stream.CopyTo(ms);
                responseBodyRaw = ms.ToArray();

                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                responseBody = encode.GetString(responseBodyRaw);

                return RequestResult.ResponseOK;
            }
            catch (WebException e)
            {
                if (response != null) { response.Close(); }
                if (e.Status == WebExceptionStatus.ProtocolError) { return RequestResult.ProtocolError; } else { return RequestResult.UnknownError; }
            }
            catch (Exception)
            {
                if (response != null) { response.Close(); }
                return RequestResult.UnknownError;
            }
        }

        public void Dispose()
        {
            if (response != null) { response.Close(); response.Dispose(); response = null; }
            if (request != null) request = null;
        }

    }
}

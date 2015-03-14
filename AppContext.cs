using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Fiddler;

namespace KodiRemoteXtender
{
    public class AppContext : ApplicationContext
    {
        private fSettings form;
        private List<IXtenderTarget> targets;

        private AppSettings settings;
        public AppSettings Settings { get { return settings; } }

        private Log log;
        public Log Log { get { return log; } }

        private string status;
        public string Status { get { return status; } }
        private void updateStatus(string Status)
        {
            status = Status;
            form.UpdateStatus(enabled, Status);
        }

        private bool isLocalMachine
        {
            get
            {
                return (settings.KodiHostname.Equals("localhost") || settings.KodiHostname.Equals("127.0.0.1"));
            }
        }

        public IXtenderTarget GetTarget(string Key)
        {
            if (targets == null) return null;

            foreach (IXtenderTarget target in targets)
            { if (target.Key == Key) return target; }

            return null;
        }

        public AppContext()
        {
            // Check that application is not already running.
            settings = new AppSettings();

            targets = new List<IXtenderTarget>();
            targets.Add(new MPCHC());

            form = new fSettings();
            form.Running = settings.Enabled;
            form.CurrentContext = this;

            enabled = false;
            updateStatus("Stopped.");

            log = new Log();

            if (settings.Enabled) startXtender();
        }

        public void SaveSettings()
        {
            settings.Enabled = form.Running;
            settings.FormSize = form.Size;
            settings.Save();
        }

        public void Exit()
        {
            stopXtender();

            SaveSettings();

            form.Close();
            form.Dispose();

            this.ExitThread();
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set 
            { 
                settings.Enabled = value;
                if (value != enabled) { if (value) { startXtender(); } else { stopXtender(); } } 
            }
        }

        private void startXtender()
        {
            if (enabled) return;
            if (Fiddler.FiddlerApplication.IsStarted()) { Fiddler.FiddlerApplication.Shutdown(); Thread.Sleep(500); }

            Fiddler.CONFIG.IgnoreServerCertErrors = true;
            FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;
            oFCSF = (oFCSF & ~FiddlerCoreStartupFlags.RegisterAsSystemProxy & ~FiddlerCoreStartupFlags.DecryptSSL);

            // Start FiddlerCore-based intercepter
            Fiddler.FiddlerApplication.Startup(settings.RemotePort, oFCSF);
            Fiddler.FiddlerApplication.BeforeRequest += new SessionStateHandler(Xtender_BeforeRequest);
            Fiddler.FiddlerApplication.BeforeResponse += new SessionStateHandler(Xtender_BeforeResponse);

            enabled = true;
            updateStatus("Running.");

            // Initialize Xtender targets
            SetTargetStatus("MPC-HC", settings.MPCHC_Enabled);
        }

        public void SetTargetStatus(string Key, bool Enabled)
        {
            IXtenderTarget target = GetTarget(Key);

            switch(Key)
            { 
                case "MPC-HC":
                    if (target == null) { target = new MPCHC(); targets.Add(target); }

                    if (Enabled && enabled)
                    {
                        Hashtable set = new Hashtable();
                        set.Clear();
                        set.Add("url", settings.MPCHC_WebInterfaceURL);
                        set.Add("islocal", this.isLocalMachine);
                        target.Initialize(log, set, form);
                    }
                    else
                    {
                        target.Shutdown();
                    }
                    break;
            }
        }

        private void stopXtender()
        {
            if (!enabled) return;

            foreach (IXtenderTarget target in targets) target.Shutdown();
            Fiddler.FiddlerApplication.Shutdown();

            enabled = false;
            updateStatus("Stopped.");
        }

        private void xtendEvent(XBMCEvent xbmcEvent, Hashtable xbmcMetadata = null)
        {
            foreach (IXtenderTarget target in targets)
                target.RaiseEvent(xbmcEvent, xbmcMetadata);
        }

        private PlayerState currentPlayerState;

        private void Xtender_BeforeRequest(Session target)
        {
            if (target.port != settings.RemotePort) return;
            if (targets.Count == 0) return;
            target.port = settings.KodiPort;
            if (!this.isLocalMachine) target.hostname = settings.KodiHostname;

            Regex regex, regexCmd = new Regex("\\\"method\\\"\\:\\\"(\\w+\\.\\w+)\\\"");
            Match match;

            target.utilDecodeRequest();
            string body = target.GetRequestBodyAsString();
            match = regexCmd.Match(body);
            if (match.Success)
            {
                target.bBufferResponse = true;
                string cmd = match.Groups[1].ToString();

                Hashtable metadata;
                System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");

                switch (cmd)
                {
                    case "Player.PlayPause":
                        xtendEvent(XBMCEvent.PlayPause);
                        break;
                    case "Player.SetSubtitle":
                        regex = new Regex("\"subtitle\":\"(next|off|previous)\"");
                        match = regex.Match(body);
                        if (match.Success)
                        {
                            switch (match.Groups[1].ToString())
                            {
                                case "off": xtendEvent(XBMCEvent.SubtitleOff); break;
                                case "previous": xtendEvent(XBMCEvent.SubtitlePrev); break;
                                default: xtendEvent(XBMCEvent.SubtitleNext); break;
                            }
                        }
                        else
                        { xtendEvent(XBMCEvent.SubtitleNext); }
                        break;
                    case "Player.SetAudioStream":
                        regex = new Regex("\"stream\":\"(next|previous)\"");
                        match = regex.Match(body);
                        if (match.Success)
                        {
                            switch (match.Groups[1].ToString())
                            {
                                case "previous": xtendEvent(XBMCEvent.AudioPrev); break;
                                default: xtendEvent(XBMCEvent.AudioNext); break;
                            }
                        }
                        else
                        { xtendEvent(XBMCEvent.SubtitleNext); }
                        break;
                    case "Player.GoTo":
                        regex = new Regex("\"to\":\"(next|previous)\"");
                        match = regex.Match(body);
                        if (match.Success)
                        {
                            switch (match.Groups[1].ToString())
                            {
                                case "next": xtendEvent(XBMCEvent.SkipNext); break;
                                case "previous": xtendEvent(XBMCEvent.SkipPrevious); break;
                            }
                        }
                        break;
                    case "Player.Seek":
                        regex = new Regex("\"value\":\"(?:small|medium|large)?(forward|backward)\"");
                        match = regex.Match(body);
                        if (match.Success)
                        {
                            xtendEvent((match.Groups[1].ToString() == "forward") ? XBMCEvent.SkipForward : XBMCEvent.SkipBackward);
                            break;
                        }

                        regex = new Regex("\"value\":([0-9]{1,3}(?:\\.[0-9]+)?)");
                        match = regex.Match(body);
                        if (match.Success)
                        {
                            // Percentage = capture group
                            decimal percentage = decimal.Parse(match.Groups[1].Value, provider);

                            // Get PlayerState because player may need absolute time instead of percentage
                            PlayerState playerState = null;
                            if (currentPlayerState != null) { lock (currentPlayerState) playerState = currentPlayerState; }
                            if (playerState != null) { if (playerState.Age > 5) playerState = null; }

                            foreach (IXtenderTarget player in targets)
                            { playerState = player.GetPlayerState(); if (playerState != null) break; }
                            if (playerState == null) break;
                            long totaltime = playerState.TotalTime;
                            if (currentPlayerState != null) { lock (currentPlayerState) currentPlayerState = playerState; }

                            metadata = new Hashtable();
                            metadata.Add("percentage", percentage);
                            metadata.Add("totaltime", totaltime);
                            xtendEvent(XBMCEvent.Seek, metadata);
                            break;
                        }

                        break;
                    case "Input.Info":
                        xtendEvent(XBMCEvent.ShowInfo);
                        break;
                }
            }

        }

        private void Xtender_BeforeResponse(Session target)
        {
            if (target.port != settings.KodiPort) return;
            if (targets.Count == 0) return;

            target.utilDecodeRequest();
            string request = target.GetRequestBodyAsString();
            target.utilDecodeResponse();
            string response = target.GetResponseBodyAsString();

            Regex regexCmd = new Regex("\\\"method\\\"\\:\\\"(\\w+\\.\\w+)\\\"");
            Match match;

            match = regexCmd.Match(request);
            if (match.Success)
            {
                bool responseModified = false;
                string mods = "";
                string cmd = match.Groups[1].ToString();
                switch (cmd)
                {
                    case "Player.GetProperties":

                        // First check if the response requires modification. If not, it is not necessary to get player state.
                        // -- TODO --

                        // Get PlayerState
                        PlayerState playerState = null;
                        foreach (IXtenderTarget player in targets)
                        { playerState = player.GetPlayerState(); if (playerState != null) break; }
                        if (playerState == null) break;
                        if (currentPlayerState == null)
                        { currentPlayerState = playerState; }
                        else
                        { lock (currentPlayerState) currentPlayerState = playerState; }

                        // Check for subtitle request, modify if needed
                        if (Regex.Match(response, "\"(?:current)?subtitle(?:s|enabled)?\":").Success)
                        {
                            Subtitle currentSubtitle = null;
                            List<Subtitle> subtitles;
                            if (!playerState.Player.HasSubtitleInfo)
                            {
                                // Player does not provide subtitle info, create dummy subtitle to enable subtitle toggling in remote.
                                currentSubtitle = new Subtitle(0, "eng", "");
                                subtitles = new List<Subtitle>();
                                subtitles.Add(currentSubtitle);
                            }
                            else
                            {
                                currentSubtitle = playerState.CurrentSubtitle;
                                subtitles = playerState.Subtitles;
                            }

                            response = Regex.Replace(response, "\"subtitleenabled\":(false|true)", "\"subtitleenabled\":" + ((currentSubtitle != null) ? "true" : "false"));
                            if (currentSubtitle != null)
                            {
                                response = Regex.Replace(response, "\"currentsubtitle\":{}", "\"currentsubtitle\":" + currentSubtitle.JSON);
                                response = Regex.Replace(response, "\"currentsubtitle\":{\"index\":[0-9]+,\"language\":\"\\w*\",\"name\":\"\\w*\"}", "\"currentsubtitle\":" + currentSubtitle.JSON);
                            }
                            string subtitlesJSON = "";
                            foreach (Subtitle subtitle in subtitles) { if (subtitlesJSON.Length > 0) subtitlesJSON += ","; subtitlesJSON += subtitle.JSON; }
                            response = Regex.Replace(response, "\"subtitles\":\\[\\]", "\"subtitles\":[" + subtitlesJSON + "]");

                            responseModified = true;
                            if (mods.Length > 0) mods += ", "; mods += "subtitles";
                        }

                        // Check for audiostream request, modify if needed
                        if (Regex.Match(response, "\"(?:currentaudiostream|audiostreams)\":").Success)
                        {
                            Audiostream currentAudiostream = null;
                            List<Audiostream> audiostreams;
                            if (!playerState.Player.HasAudiostreamInfo)
                            {
                                currentAudiostream = new Audiostream(0, "eng", 1, "pcm", 1, "Unknown");
                                audiostreams = new List<Audiostream>();
                                audiostreams.Add(currentAudiostream);
                                //audiostreams.Add(new Audiostream(1, "eng", 1, "pcm", 1, "Unknown"));
                            }
                            else
                            {
                                currentAudiostream = playerState.CurrentAudiostream;
                                audiostreams = playerState.Audiostreams;
                            }

                            if (currentAudiostream != null)
                            {
                                response = Regex.Replace(response, "\"currentaudiostream\":{}", "\"currentaudiostream\":" + currentAudiostream.JSON);
                            }

                            string audiostreamsJSON = "";
                            foreach (Audiostream audiostream in audiostreams) { if (audiostreamsJSON.Length > 0) audiostreamsJSON += ","; audiostreamsJSON += audiostream.JSON; }
                            response = Regex.Replace(response, "\"audiostreams\":\\[\\]", "\"audiostreams\":[" + audiostreamsJSON + "]");

                            if (mods.Length > 0) mods += ", "; mods += "audiostreams";
                            responseModified = true;
                        }

                        // Check for time/totaltime request, modify if needed
                        Regex regexTime, regexTotalTime;
                        regexTime = new Regex("\"time\":{\"(hours|minutes|seconds|milliseconds)\":([0-9]+),\"(hours|minutes|seconds|milliseconds)\":([0-9]+),\"(hours|minutes|seconds|milliseconds)\":([0-9]+),\"(hours|minutes|seconds|milliseconds)\":([0-9]+)}");
                        regexTotalTime = new Regex("\"totaltime\":{\"(hours|minutes|seconds|milliseconds)\":([0-9]+),\"(hours|minutes|seconds|milliseconds)\":([0-9]+),\"(hours|minutes|seconds|milliseconds)\":([0-9]+),\"(hours|minutes|seconds|milliseconds)\":([0-9]+)}");
                        match = regexTotalTime.Match(response);
                        if (match.Success)
                        {
                            int seconds = 0;
                            for (int i = 1; i < 8; i++)
                            {
                                switch (match.Groups[i].Value)
                                {
                                    case "hours": seconds += int.Parse(match.Groups[i + 1].Value) * 3600; break;
                                    case "minutes": seconds += int.Parse(match.Groups[i + 1].Value) * 60; break;
                                    case "seconds": seconds += int.Parse(match.Groups[i + 1].Value); break;
                                }
                            }

                            if (seconds <= 1)
                            {
                                response = regexTime.Replace(response, "\"time\":" + playerState.GetTimeJSON(playerState.Time));
                                response = regexTotalTime.Replace(response, "\"totaltime\":" + playerState.GetTimeJSON(playerState.TotalTime));
                                response = Regex.Replace(response, "\"percentage\":[0-9]{1,3}(?:\\.[0-9]{1,5})?", "\"percentage\":" + playerState.GetTimePercentage());
                                responseModified = true;
                                if (mods.Length > 0) mods += ", "; mods += "time/duration";
                            }
                        }

                        // Check for canseek request, modify if needed
                        match = Regex.Match(response, "\"canseek\":(false|true)");
                        if (match.Success)
                        {
                            if ((match.Groups[1].Value == "true") != playerState.Player.CanSeek)
                            {
                                response = Regex.Replace(response, "\"canseek\":(?:false|true)", "\"canseek\":" + ((playerState.Player.CanSeek) ? "true" : "false"));
                                responseModified = true;
                                if (mods.Length > 0) mods += ", "; mods += "can seek";
                            }
                        }

                        if (responseModified) log.LogEvent(Log.EventType.ModifyResponse, "Player.GetProperties", "[" + playerState.Player + "] Response modified: " + mods);

                        break;
                    case "XBMC.GetInfoLabels":
                        response = Regex.Replace(response, "\"VideoPlayer.VideoAspect\":\"(?:[1-9]+\\.[0-9]+)?\"", "\"VideoPlayer.VideoAspect\":\"1.78\"");
                        response = Regex.Replace(response, "\"VideoPlayer.VideoResolution\":\"[1-9]*\"", "\"VideoPlayer.VideoResolution\":\"1080\"");
                        responseModified = true;
                        break;
                }

                if (responseModified) target.utilSetResponseBody(response);
            }
        }

    }
}

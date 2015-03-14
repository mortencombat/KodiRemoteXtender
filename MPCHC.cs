using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

namespace KodiRemoteXtender
{
    class MPCHC : IXtenderTarget, INotifyPropertyChanged
    {
        private string url;
        private Log log;
        private bool isLocal;   // true if target is running on same local machine as RemoteXtender

        fSettings form;

        public string Key { get { return "MPC-HC"; } }
        public bool CanSeek { get { return true; } }
        public bool HasSubtitleInfo { get { return false; } }
        public bool HasAudiostreamInfo { get { return false; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private string status;
        public string Status { get {return status;} }
        private void updateStatus(string Status)
        {
            if (Status != status)
            {
                status = Status;
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Status")); }
            }
        }

        public PlayerStatus PlayerIsRunning(bool AllowRequests = false)
        {
            if (isLocal)
            {
                // Check if MPC-HC process is running
                Process[] process = Process.GetProcessesByName("mpc-hc");
                return (process.GetLength(0) == 0) ? PlayerStatus.NotRunning : PlayerStatus.Running;
            }
            else
            {
                if (AllowRequests && url.Length > 0)
                {
                    // TODO - make simple request to web interface if URL is specified

                }

                return PlayerStatus.Unknown;
            }
        }

        private bool enabled;

        public MPCHC()
        {
            isLocal = false;
            url = "";

            enabled = false;
            updateStatus("Disabled.");
        }

        public void Initialize(Log Log, Hashtable Settings, fSettings Form)
        {
            // Prepare request objects.
            // Settings:
            //      url = web interface url
            //      islocal = true if remotextender is running on same machine as Kodi/external player

            this.log = Log;
            this.form = Form;

            if (Settings.ContainsKey("url"))
            {
                url = Settings["url"].ToString();
                if (url.Substring(0, 7) != "http://") url = "http://" + url;
                if (url.Substring(url.Length - 1, 1) != "/") url += "/";
                
                isLocal = false;
                if (Settings.ContainsKey("islocal")) isLocal = (bool)Settings["islocal"];

                enabled = true;
                updateStatus("Enabled.");
            }
            else
            {
                enabled = false;
                url = "";
                updateStatus("Disabled (web interface URL is missing).");
            }
        }

        public bool RaiseEvent(XBMCEvent XBMCEvent, Hashtable XBMCMetadata)
        {
            if (!enabled) return false;
            if (this.PlayerIsRunning() == PlayerStatus.NotRunning) return false;
            
            int cmd = 0;
            string ext = "";

            switch (XBMCEvent)
            {
                case KodiRemoteXtender.XBMCEvent.Seek:
                    if (!XBMCMetadata.ContainsKey("totaltime") || !XBMCMetadata.ContainsKey("percentage")) break;
                    cmd = -1;
                    int hrs = 0, mins = 0, secs = 0;
                    decimal time = Convert.ToDecimal(XBMCMetadata["totaltime"]) * (decimal)XBMCMetadata["percentage"] / 100;
                    while (time > 3600000) { hrs++; time -= 3600000; }
                    while (time > 60000) { mins++; time -= 60000; }
                    while (time > 1000) { secs++; time -= 1000; }
                    ext = "&position=" + hrs.ToString("00") + "%3A" + mins.ToString("00") + "%3A" + secs.ToString("00");
                    break;
                case KodiRemoteXtender.XBMCEvent.PlayPause:
                    cmd = 889; break;
                case KodiRemoteXtender.XBMCEvent.Play:
                    cmd = 887; break;
                case KodiRemoteXtender.XBMCEvent.Pause:
                    cmd = 888; break;
                case KodiRemoteXtender.XBMCEvent.Stop:
                    cmd = 890; break;
                case KodiRemoteXtender.XBMCEvent.AudioPrev:
                    cmd = 953; break;
                case KodiRemoteXtender.XBMCEvent.AudioNext:
                    cmd = 952; break;
                case KodiRemoteXtender.XBMCEvent.SubtitlePrev:
                    cmd = 955; break;
                case KodiRemoteXtender.XBMCEvent.SubtitleOff:
                case KodiRemoteXtender.XBMCEvent.SubtitleNext:
                    cmd = 954; break;
                case KodiRemoteXtender.XBMCEvent.SkipBackward:
                    cmd = 903; break;
                case KodiRemoteXtender.XBMCEvent.SkipForward:
                    cmd = 904; break;
                case KodiRemoteXtender.XBMCEvent.SkipPrevious:
                    cmd = 921; break;
                case KodiRemoteXtender.XBMCEvent.SkipNext:
                    cmd = 922; break;
                case KodiRemoteXtender.XBMCEvent.ShowInfo:
                    if (isLocal)
                    {
                        // Move mouse along bottom edge of screen.
                        form.MoveCursor(new Point(100, Screen.PrimaryScreen.Bounds.Height));
                        form.MoveCursor(new Point(200, Screen.PrimaryScreen.Bounds.Height));
                    }
                    break;
                default:
                    return false;
            }

            if (cmd != 0)
            {
                HttpSession http = new HttpSession(url + "command.html", "POST");
                http.RequestBody = "wm_command=" + cmd.ToString() + ext;
                HttpSession.RequestResult result = http.ExecuteRequest();
                http.Dispose(); http = null;
                if (result != HttpSession.RequestResult.ResponseOK)
                {
                    log.LogEvent(Log.EventType.ForwardToExternalPlayer, XBMCEvent.ToString(), "[MPC-HC] Forwarding failed.");
                    return false;
                }
                else
                {
                    log.LogEvent(Log.EventType.ForwardToExternalPlayer, XBMCEvent.ToString(), "[MPC-HC] Forwarded (command ID " + cmd.ToString() + ").");
                }
            }

            return true;
        }

        public PlayerState GetPlayerState()
        {
            if (!enabled) return null;
            if (this.PlayerIsRunning() == PlayerStatus.NotRunning) return null;

            HttpSession http = new HttpSession(url + "status.html", "GET");
            HttpSession.RequestResult result = http.ExecuteRequest();
            if (result != HttpSession.RequestResult.ResponseOK) { http.Dispose(); http = null; return null; }

            Regex regex = new Regex("OnStatus\\(\"([^\\\\/:*?\"<>|]*)\", \"([a-zA-Z]+)\", ([0-9]+), \"[0-9]{2}:[0-9]{2}:[0-9]{2}\", ([0-9]+), \"[0-9]{2}:[0-9]{2}:[0-9]{2}\", [0-9]{1,3}, [0-9]{1,3}, \"([^*?\"<>|]*)\"\\)");
            Match match = regex.Match(http.ResponseBody);
            http.Dispose(); http = null; 
            if (match.Success)
            {
                PlayerState state = new PlayerState(this);
                switch(match.Groups[2].Value)
                {
                    case "Paused": state.State = MediaState.Paused; break;
                    case "Playing": state.State = MediaState.Playing; break;
                    default: state.State = MediaState.Stopped; break;
                }
                state.Time = Int64.Parse(match.Groups[3].Value);
                state.TotalTime = Int64.Parse(match.Groups[4].Value);
                state.Filename = match.Groups[5].Value;
                return state;
            }

            return null;
        }

        public void Shutdown()
        {
            url = "";
            enabled = false;
            updateStatus("Disabled.");
        }

    }
}

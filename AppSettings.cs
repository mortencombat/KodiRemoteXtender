using System;
using System.Configuration;
using System.Drawing;

namespace KodiRemoteXtender
{
    public class AppSettings : ApplicationSettingsBase
    {
        // GENERAL SETTINGS

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool Enabled
        {
            get { return ((bool)this["Enabled"]); }
            set { this["Enabled"] = (bool)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool RunOnStartup
        {
            get { return ((bool)this["RunOnStartup"]); }
            set { this["RunOnStartup"] = (bool)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("localhost")]
        public string KodiHostname
        {
            get { return (string)this["KodiHostname"]; }
            set { this["KodiHostname"] = (string)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("8080")]
        public int KodiPort
        {
            get { return ((int)this["KodiPort"]); }
            set { this["KodiPort"] = (int)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("8081")]
        public int RemotePort
        {
            get { return ((int)this["RemotePort"]); }
            set { this["RemotePort"] = (int)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("800, 600")]
        public Size FormSize
        {
            get { return ((Size)this["FormSize"]); }
            set { this["FormSize"] = (Size)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("100, 100")]
        public Point FormLocation
        {
            get { return ((Point)this["FormLocation"]); }
            set { this["FormLocation"] = (Point)value; }
        }

        // LOG SETTINGS

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool LogResponseModifications
        {
            get { return ((bool)this["LogResponseModifications"]); }
            set { this["LogResponseModifications"] = (bool)value; }
        }

        // MPC-HC

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool MPCHC_Enabled
        {
            get { return ((bool)this["MPCHC_Enabled"]); }
            set { this["MPCHC_Enabled"] = (bool)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("http://localhost:13579")]
        public string MPCHC_WebInterfaceURL
        {
            get { return (string)this["MPCHC_WebInterfaceURL"]; }
            set { this["MPCHC_WebInterfaceURL"] = (string)value; }
        }

    }
}

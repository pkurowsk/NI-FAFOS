﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FAFOS.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string FranchiseeID {
            get {
                return ((string)(this["FranchiseeID"]));
            }
            set {
                this["FranchiseeID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ProfilePics {
            get {
                return ((string)(this["ProfilePics"]));
            }
            set {
                this["ProfilePics"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.\\SEDGESQL;AttachDbFilename=\"C:\\Users\\Philip\\Documents\\## School ##\\Y" +
            "ear 3\\SE 3350 - Software Design\\NI-FAFOS\\SEdge2013\\SystemDatabase\\EXPRESS\\FAFOS." +
            "mdf\";Integrated Security=True;Connect Timeout=30;User Instance=True")]
        public string FAFOS {
            get {
                return ((string)(this["FAFOS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.\\SEDGESQL;AttachDbFilename=\"C:\\Users\\Philip\\Documents\\## School ##\\Y" +
            "ear 3\\SE 3350 - Software Design\\NI-FAFOS\\SEdge2013\\SystemDatabase\\EXPRESS\\FAFOS_" +
            "HQ.mdf\";Integrated Security=True;Connect Timeout=30;User Instance=True")]
        public string HQ {
            get {
                return ((string)(this["HQ"]));
            }
        }
    }
}

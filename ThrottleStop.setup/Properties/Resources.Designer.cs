﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThrottleStop.setup.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ThrottleStop.setup.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] RwDrv {
            get {
                object obj = ResourceManager.GetObject("RwDrv", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-16&quot;?&gt;
        ///&lt;Task version=&quot;1.2&quot; xmlns=&quot;http://schemas.microsoft.com/windows/2004/02/mit/task&quot;&gt;
        ///  &lt;RegistrationInfo&gt;
        ///    &lt;Author&gt;{Author}&lt;/Author&gt;
        ///    &lt;Description&gt;{Description}&lt;/Description&gt;
        ///    &lt;URI&gt;{URI}&lt;/URI&gt;
        ///  &lt;/RegistrationInfo&gt;
        ///  &lt;Triggers&gt;
        ///    &lt;LogonTrigger&gt;
        ///      &lt;Enabled&gt;true&lt;/Enabled&gt;
        ///      &lt;Delay&gt;PT1M&lt;/Delay&gt;
        ///    &lt;/LogonTrigger&gt;
        ///  &lt;/Triggers&gt;
        ///  &lt;Principals&gt;
        ///    &lt;Principal id=&quot;Author&quot;&gt;
        ///      &lt;GroupId&gt;S-1-5-4&lt;/GroupId&gt;
        ///      &lt;RunLevel&gt;HighestAvailable&lt;/Ru [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string task {
            get {
                return ResourceManager.GetString("task", resourceCulture);
            }
        }
    }
}

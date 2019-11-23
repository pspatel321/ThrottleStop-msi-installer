using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ThrottleStop.setup")]
[assembly: AssemblyDescription("Contains install/uninstall actions for base ThrottleStop package")]
[assembly: AssemblyCompany("pspatel321")]
[assembly: AssemblyProduct("ThrottleStop")]
[assembly: AssemblyCopyright("Copyright © 2019 Parth Patel")]
[assembly: AssemblyMetadata("WebUrl", @"https://github.com/pspatel321/ThrottleStop-msi-installer")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("479a1907-3661-4add-b7f2-c32153da13e0")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.*")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

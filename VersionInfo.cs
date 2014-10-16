//Disables the warning C0436 regarding Type conflicts with the version class
#pragma warning disable 0436

using System.Reflection;

[assembly: AssemblyVersion(Version.BaseNumber)]
[assembly: AssemblyFileVersion(Version.Number)]
[assembly: AssemblyInformationalVersion(Version.Number + Version.ReleaseStatus)]

[assembly: AssemblyCompany(Version.CompanyName)]
[assembly: AssemblyCopyright("Copyright © " + Version.CompanyName + " " + Version.Year)]
[assembly: AssemblyTrademark(Version.CompanyName + " " + Version.Year)]

public static class Version
{
    public const string BaseNumber = "1.0.0.0";
    public const string Number = "1.0.1.1";
    public const string CompanyName = "Who Solutions Ltd";
    public const string Year = "2014";
    public const string ReleaseStatus = "-B1";
}
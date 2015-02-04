using System;
using System.Linq;
using WixSharp;

class Script
{
    static public void Main()
    {
        try
        {
            // alternatively, use environment variable WIXSHARP_WIXDIR settable via WixSharp's install.cmd
            //WixSharp.Compiler.WixLocation = @"C:\Users\Peter\Downloads\WixSharp.1.0.3.0\Wix_bin\bin";

            const string AppName = @"Pairs.Client.exe";

            Project project =
                new Project("Pairs",
                    new Dir(@"%ProgramFiles%\PhDGames\Pairs",
                        new File(AppName),
                        new File(@"Xceed.Wpf.Toolkit.dll"))
                    // to get IntegerUpDown control missing from standard WPF tookit
                    )
                {
                    MajorUpgradeStrategy = new MajorUpgradeStrategy
                    {
                        UpgradeVersions = VersionRange.OlderThanThis,
                        PreventDowngradingVersions = VersionRange.NewerThanThis,
                        NewerProductInstalledErrorMessage = "Newer version already installed"
                    },
                    UpgradeCode = new Guid("DB8E5060-47A9-4267-9EEB-4596352B98D3"),
                    GUID = new Guid("0C92849D-483F-41A5-BD60-5B33E0D3D507"),
                    UI = WUI.WixUI_InstallDir,
                    OutFileName = "PairsInstaller",
                    Manufacturer = "PhdGames",
                    BackgroundImage = @"Nessy.bmp",
                    BannerImage = @"Nessy.bmp",
                    Description = "Pairs Memory Game - Can you beat the computer?",
                    Version = new Version(1, 1, 1, 0),
                    AddRemoveProgramsIcon = @"Nessy.ico"
                };

            project
                .FindFile(f => f.Name.EndsWith(AppName))
                .First()
                .Shortcuts = new[]
                {
                    new FileShortcut("Pairs", "%Desktop%") {Advertise = true, IconFile = "Nessy.ico"}
                };



            Compiler.BuildMsi(project);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }

        //Console.WriteLine("Hit Enter to terminate");
        //Console.ReadLine();
    }
}
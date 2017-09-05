namespace DatabaseSetup
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using DbUp;
    using DbUp.Helpers;

    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.Web.Administration;

    class Program
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString;
            var databaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
            var epiServerDatabaseName = ConfigurationManager.AppSettings["EpiServerDatabaseName"];
            var websiteName = ConfigurationManager.AppSettings["WebSiteName"];

            Console.Write($"Database Name = {epiServerDatabaseName}");
            Console.Write($"SQL Server = {databaseServer}");
            Console.Write($"ConnectionString = {connectionString}");

            log.Info($"Database Name = {epiServerDatabaseName}");
            log.Info($"SQL Server = {databaseServer}");
            log.Info($"ConnectionString = {connectionString}");

            try
            {
                var connection = new ServerConnection(databaseServer);
                var server = new Server(connection);

                TryDeleteDatabase(server, epiServerDatabaseName, connectionString);

                var path = Path.GetFullPath(
                    Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "..\\"));

                log.Info($"Current Path = {path}");

                // Delete Database Tables, SPROCS etc...
                RunFreshSqlScript(connectionString, $@"{path}\Scripts");

                // Install Epi tables
                RunFreshSqlScript(connectionString, $@"{path}\Install");

                // Copy Files
                Copy(path + "EPiUpdatePackage", Directory.GetCurrentDirectory());

                var websitePath = GetWebsitePath(websiteName);
                ExecuteCommand($@"update.bat {websitePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                log.Error(e);
                log.Error($"SQL Server = {databaseServer}");
                log.Error($"ConnectionString = {connectionString}");
                log.Error($"DatabaseName = {epiServerDatabaseName}");
            }
        }

        private static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private static string GetWebsitePath(string websiteName)
        {
            using (var serverManager = new ServerManager())
            {
                var site = serverManager.Sites.Single(x => x.Name == websiteName);
                var applicationRoot =
                    site.Applications.Single(a => a.Path == "/");
                var virtualRoot =
                    applicationRoot.VirtualDirectories.Single(v => v.Path == "/");

                var path = virtualRoot.PhysicalPath;
                log.Info($"Website Path = {path}");

                return path;
            }
        }

        private static void TryDeleteDatabase(Server server, string databaseName, string connectionString)
        {
            try
            {
                if (server.Databases[databaseName] != null)
                {
                    server.KillDatabase(databaseName);
                }


                EnsureDatabase.For.SqlDatabase(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                log.Error(e);
            }
        }

        private static void ExecuteCommand(string command)
        {
            log.Info($"Command {command}");
            var processInfo =
                new ProcessStartInfo("cmd.exe", "/c " + command)
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    };

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (sender, e) => log.Info("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (sender, e) => log.Error("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();
            process.Close();
        }

        private static void RunFreshSqlScript(string connectionString, string scriptPath)
        {
            try
            {
                var upgrader = DeployChanges.To.SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(scriptPath)
                    .LogToConsole()
                    .WithTransaction()
                    .JournalTo(new NullJournal())
                    .Build();
                var result = upgrader.PerformUpgrade();
                Console.Write(result.Successful);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                log.Error(e);
            }
        }
    }
}

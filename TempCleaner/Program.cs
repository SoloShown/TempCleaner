using System;
using System.IO;
using System.Threading;

class TempCleaner
{
    static void Main()
    {

        PrintLogo();
        LoadingAnimation();

        while (true)
        {
            Console.Clear();
            PrintLogo();
            ShowMenu();

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CleanTemp();
                    break;
                case "2":
                    CleanPrefetch();
                    break;
                case "3":
                    DeleteUSNJournal();
                    break;
                case "4":
                    CleanTemp();
                    CleanPrefetch();
                    DeleteUSNJournal();
                    break;
                case "5":
                    ShowSystemInfo();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice! Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void PrintLogo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("   ▄▄▄▄▄        ▄▄         ▄▄▄▄▄");
        Console.WriteLine("  ██▀▀▀▀█▄       ██       ██▀▀▀▀█▄ █▄");
        Console.WriteLine("  ▀██▄  ▄▀       ██       ▀██▄  ▄▀ ██");
        Console.WriteLine("    ▀██▄▄  ▄███▄ ██ ▄███▄   ▀██▄▄  ████▄ ▄███▄▀█▄ █▄ ██▀████▄");
        Console.WriteLine("  ▄   ▀██▄ ██ ██ ██ ██ ██ ▄   ▀██▄ ██ ██ ██ ██ ██▄██▄██ ██ ██");
        Console.WriteLine("  ▀██████▀▄▀███▀▄██▄▀███▀ ▀██████▀▄██ ██▄▀███▀  ▀██▀██▀▄██ ▀█");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("                      TEMP CLEANER SYSETM");
        Console.WriteLine();
    }

    static void LoadingAnimation()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Loading ");
        char[] spinner = { '|', '/', '-', '\\' };
        for (int i = 0; i < 20; i++)
        {
            Console.Write(spinner[i % 4]);
            Thread.Sleep(100);
            Console.Write("\b");
        }
        Console.WriteLine();
    }

    static void ShowMenu()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        Console.WriteLine("===== TEMP CLEANER MENU =====");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("1 - Clean TEMP");
        Console.WriteLine("2 - Clean PREFETCH");
        Console.WriteLine("3 - Delete USN Journal");
        Console.WriteLine("4 - Full Clean (recommended)");
        Console.WriteLine("5 - System Info");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("6 - Exit");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.Write("Select option: ");
    }

    static void ProgressBar(string action)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(action + " ");
        for (int i = 0; i <= 30; i++)
        {
            Console.Write("█");
            Thread.Sleep(50);
        }
        Console.WriteLine();
    }

    static void CleanTemp()
    {
        string tempPath = Path.GetTempPath();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Cleaning TEMP folder...");
        ProgressBar("Progress");
        try
        {
            DirectoryInfo di = new DirectoryInfo(tempPath);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
            Console.WriteLine("TEMP cleaned!");
        }
        catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Some files could not be deleted."); }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void CleanPrefetch()
    {
        string prefetchPath = @"C:\Windows\Prefetch";
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Cleaning PREFETCH folder...");
        ProgressBar("Progress");
        try
        {
            DirectoryInfo di = new DirectoryInfo(prefetchPath);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            Console.WriteLine("PREFETCH cleaned!");
        }
        catch
        {
            Console.WriteLine("Cannot clean PREFETCH. Run as Administrator.");
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void DeleteUSNJournal()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Deleting USN Journal...");
        ProgressBar("Progress");
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo("fsutil", "usn deletejournal /D C:")
            {
                Verb = "runas",
                CreateNoWindow = true,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi).WaitForExit();
            Console.WriteLine("USN Journal deleted!");
        }
        catch
        {
            Console.WriteLine("Cannot delete USN Journal. Run as Administrator.");
        }
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ShowSystemInfo()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("System Information:");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"OS Version: {Environment.OSVersion}");
        Console.WriteLine($"Machine Name: {Environment.MachineName}");
        Console.WriteLine($"User Name: {Environment.UserName}");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

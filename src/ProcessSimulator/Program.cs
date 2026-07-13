using System;
using System.Threading;

namespace ProcessSimulator;

public delegate void ProgressReporter(string stepName, int percent);

internal class Program
{
    private static void Main()
    {
        ProgressReporter progressReporter = ShowProgressBar;
        progressReporter += ReportHalfwayWarning;
        
        Console.CursorVisible = false;
        Console.WriteLine("=== Process Simulator ===");
        Console.WriteLine();

        RunSimulation(progressReporter);

        Console.WriteLine("All process steps completed.");
        Console.CursorVisible = true;
    }

    private static void RunSimulation(ProgressReporter progressReporter)
    {
        string[] steps =
        {
            "Downloading data",
            "Validating input",
            "Processing records",
            "Generating report",
            "Publishing results",
            "Cleaning up"
        };

        foreach (string step in steps)
        {
            for (int percent = 0; percent <= 100; percent += 5)
            {
                progressReporter(step, percent);
                Thread.Sleep(80);
            }
        }
    }

    private static void ShowProgressBar(string stepName, int percent)
    {
        const int width = 30;
        const char filledChar = '█';
        const char emptyChar = '░';
        const char barStartChar = '⟦';
        const char barEndChar = '⟧';

        int filled = percent * width / 100;

        string bar = new string(filledChar, filled) + new string(emptyChar, width - filled);
        Console.Write($"\r{stepName,-22} {barStartChar}{bar}{barEndChar} {percent,3}%");

        if (percent == 100)
        {
            Console.WriteLine();
        }
    }

    private static void ReportHalfwayWarning(string stepName, int percent)
    {
        if (percent == 50)
        {
            Console.WriteLine($"  Warning: {stepName} is only halfway done.");
        }
    }
}

using System;
using System.Threading;

namespace ProcessSimulator;

internal class Program
{
    private static void Main()
    {
        ProcessRunner runner = new ProcessRunner();
        runner.ProgressChanged += (sender, e) => ShowProgressBar(e.StepName, e.Percent);
        runner.StepCompleted += (sender, e) => ReportHalfwayWarning(e.StepName, e.Percent);
        runner.ProcessCompleted += (sender, e) => Console.WriteLine("All process steps completed.");
        Console.CursorVisible = false;
        Console.WriteLine("=== Process Simulator ===");
        Console.WriteLine();


        Console.WriteLine("All process steps completed.");
        Console.CursorVisible = true;
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
public class ProgressEventArgs : EventArgs {
    public string StepName { get; set; }
    public int Percent { get; set; }
}
public class ProcessRunner
{
    public void RunProcess()
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
                ProgressChanged?.Invoke(this, new ProgressEventArgs { StepName = step, Percent = percent });                Thread.Sleep(80);
            }
            OnStepCompleted(step);
        }
        OnProcessCompleted();
    }
    public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    public event EventHandler<ProgressStepEventArgs> ProgressStepChanged;
    public event EventHandler<ProcessStepEventArgs>? StepCompleted;
    public event EventHandler? ProcessCompleted;
}
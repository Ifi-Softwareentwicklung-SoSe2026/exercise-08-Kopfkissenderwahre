using System;

namespace ProcessSimulator;

internal static class Program
{
    private static void Main()
    {
        Console.CursorVisible = false;
        Console.WriteLine("=== Process Simulator ===");
        Console.WriteLine();

        var processRunner = new ProcessRunner();
        var progressBar = new ConsoleProgressBar();
        var warningDisplay = new ConsoleHalfwayWarning();
        var lifecycleDisplay = new ConsoleProcessLifecycleDisplay();

        processRunner.StepStarted += lifecycleDisplay.HandleStepStarted;
        processRunner.ProgressChanged += progressBar.HandleProgressChanged;
        processRunner.ProgressChanged += warningDisplay.HandleProgressChanged;
        processRunner.StepCompleted += lifecycleDisplay.HandleStepCompleted;
        processRunner.ProcessCompleted += lifecycleDisplay.HandleProcessCompleted;

        processRunner.Run();

        Console.CursorVisible = true;
    }
}
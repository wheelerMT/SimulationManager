// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

using System.Diagnostics;

namespace AutoRunner.Core;

public class JobProcess : Process
{
    private readonly TaskCompletionSource<bool> _taskCompletionSource;

    private JobProcess()
    {
        EnableRaisingEvents = true;
        _taskCompletionSource = new TaskCompletionSource<bool>();

        // Attach to the Exited event
        Exited += (sender, e) =>
        {
            _taskCompletionSource.SetResult(true);
            Console.WriteLine($"JobProcess: Process {ProcessName} has exited.");
        };
    }

    public async Task WaitForExitAsync()
    {
        if (!Start()) throw new InvalidOperationException($"JobProcess: Process {ProcessName} failed to start.");

        await _taskCompletionSource.Task;
    }

    // Factory to create a new JobProcess
    public static JobProcess StartNew(string jobName, string arguments = "")
    {
        var jobProcess = new JobProcess
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = jobName,
                Arguments = arguments,
                UseShellExecute = true,
                CreateNoWindow = true
            }
        };
        return jobProcess;
    }
}
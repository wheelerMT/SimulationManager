// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

using System.Diagnostics;
using AutoRunner.Core.Interfaces;

namespace AutoRunner.Core;

public class JobProcess : IJobProcess
{
    private readonly Process _process;
    private readonly TaskCompletionSource<bool> _taskCompletionSource;

    private JobProcess()
    {
        _process = new Process();
        _process.EnableRaisingEvents = true;
        _taskCompletionSource = new TaskCompletionSource<bool>();

        // Attach to the Exited event
        _process.Exited += (sender, e) =>
        {
            _taskCompletionSource.SetResult(true);
            Console.WriteLine($"JobProcess: Process {ProcessName} has exited.");
        };
    }

    public bool Start()
    {
        return _process.Start();
    }

    public string ProcessName => _process.ProcessName;

    public ProcessStartInfo StartInfo
    {
        get => _process.StartInfo;
        set => _process.StartInfo = value;
    }

    public bool EnableRaisingEvents
    {
        get => _process.EnableRaisingEvents;
        set => _process.EnableRaisingEvents = value;
    }

    public event EventHandler Exited
    {
        add => _process.Exited += value;
        remove => _process.Exited -= value;
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
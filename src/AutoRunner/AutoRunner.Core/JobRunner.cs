// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

using System.Diagnostics;

namespace AutoRunner.Core;

public class JobRunner
{
    private readonly SystemInfo _systemInfo = new();
    public Queue<Process> JobQueue { get; } = new();
    public List<Process> RunningJobs { get; } = [];

    public void QueueJob(Process job)
    {
        JobQueue.Enqueue(job);
    }

    private async Task _startNextJob()
    {
        var job = JobQueue.Dequeue();
        job.Start();
        RunningJobs.Add(job);
        await job.WaitForExitAsync();
    }

    public void StopJobs()
    {
        foreach (var job in RunningJobs) job.Kill(true);
    }

    public async Task RunJobs()
    {
        while (JobQueue.Count > 0)
            do
            {
                await _startNextJob();
            } while (RunningJobs.Count < _systemInfo.CoresToUse);

            // Check if JobQueue needs updating
    }
}
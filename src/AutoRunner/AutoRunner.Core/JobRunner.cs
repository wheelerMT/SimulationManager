// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

namespace AutoRunner.Core;

public class JobRunner
{
    private SystemInfo _systemInfo = new();
    public Queue<Job> JobsToDo { get; } = new();
    public List<Job> JobsRunning { get; } = [];
    public List<Job> JobsFinished { get; } = [];

    public void QueueJob(Job job)
    {
        JobsToDo.Enqueue(job);
    }

    public void StartNextJob()
    {
        var job = JobsToDo.Dequeue();

        // Creates a new Process tied to Job.JobName 
        job.Process = JobProcess.StartNew(job.JobName);

        // Start the process
        job.Process.Start();
        Console.WriteLine($"Job started: {job.JobName}");
        JobsRunning.Add(job);
    }

    private void _finishJob(Job job)
    {
        _markJobAsFinished(job);
        JobsRunning.Remove(job);
        JobsFinished.Add(job);
    }

    private static void _markJobAsFinished(Job job)
    {
        job.Finished = true;
        job.EndTime = DateTime.Now;
        if (job is { StartTime: not null, EndTime: not null })
            job.JobDuration = job.EndTime.Value - job.StartTime.Value;
    }
}
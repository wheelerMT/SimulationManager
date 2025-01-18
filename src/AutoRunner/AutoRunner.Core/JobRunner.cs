// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

using System.Diagnostics;

namespace AutoRunner.Core;

public class JobRunner
{
    public Queue<Job> JobsToDo { get; } = new();
    public List<Job> JobsRunning { get; } = [];
    public List<Job> JobsFinished { get; } = [];


    public void QueueJob(Job job)
    {
        JobsToDo.Enqueue(job);
    }

    public void StartJob()
    {
        var job = JobsToDo.Dequeue();
        JobsRunning.Add(job);

        job.Process = new Process(); // TODO: Implement process logic
    }

    public void FinishJob(Job job)
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
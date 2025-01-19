// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

namespace AutoRunner.Core;

public record Job(string JobName)
{
    public JobProcess? Process;
    public bool Finished { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan JobDuration { get; set; }
}
// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

namespace AutoRunner.Core.Tests;

[TestClass]
public class JobRunnerTests
{
    private JobRunner _jobRunner;

    [TestInitialize]
    public void Setup()
    {
        _jobRunner = new JobRunner();
    }

    [TestMethod]
    public void QueueJob_AddsJobToQueue()
    {
        var job = new Job("TestJob");

        _jobRunner.QueueJob(job);

        Assert.AreEqual(1, _jobRunner.JobsToDo.Count);
        Assert.AreEqual(job, _jobRunner.JobsToDo.Peek());
    }
}
// Copyright (c) Matt Wheeler
// Licensed under the MIT License.

using System.Diagnostics;

namespace AutoRunner.Core.Interfaces;

public interface IJobProcess
{
    string ProcessName { get; }
    ProcessStartInfo StartInfo { get; set; }
    bool EnableRaisingEvents { get; set; }
    bool Start();
    Task WaitForExitAsync();
    event EventHandler Exited;
}
﻿using System;
using System.Runtime.InteropServices;

namespace Vostok.Commons.Utilities
{
    public static class RuntimeDetector
    {
        public static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
        public static bool IsDotNetCore { get; } = RuntimeEnvironment.GetRuntimeDirectory().Contains("NETCore");
        public static bool IsDotNetFramework { get; } = RuntimeEnvironment.GetRuntimeDirectory().Contains(@"Microsoft.NET\Framework");
    }
}
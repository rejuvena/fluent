#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Runtime.CompilerServices;

namespace Fluent.Core.Cecil.Tests
{
    public static class TestMimic
    {
        [UsesMimickedType]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string MimicTest() => new MimickingType().Hi;
    }
}
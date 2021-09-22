#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System;
using Fluent.Core.Cecil;
// ReSharper disable InconsistentNaming

namespace Fluent.Mimics
{
    [TypeMimicker("Terraria.GameContent.Liquid", "LiquidRenderer")]
    public static class LiquidRendererMimic
    {
        // private, readonly
        internal static int[] WATERFALL_LENGTH = Array.Empty<int>();

        // private, readonly
        internal static float[] DEFAULT_OPACITY = Array.Empty<float>();
    }
}
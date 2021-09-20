#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using Terraria.ID;

namespace Fluent.API
{
    public static class LiquidLoader
    {
        internal static readonly List<ModLiquid> Liquids = new();
        internal static readonly List<GlobalLiquid> Globals = new();

        public static int LiquidCount => LiquidID.Count + Liquids.Count;

        public static ModLiquid GetLiquid(int type) =>
            type >= LiquidID.Count && type < LiquidCount ? Liquids[type - LiquidID.Count] : null;

        public static void RegisterLiquid(ModLiquid liquid)
        {
            Liquids.Add(liquid);
            liquid.Type = LiquidCount - 1;
        }

        public static void RegisterGlobal(GlobalLiquid global)
        {
            Globals.Add(global);
        }

        public static void Unload()
        {
            Liquids.Clear();
            Globals.Clear();
        }
    }
}
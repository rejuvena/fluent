#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using Fluent.API.Defaults;
using JetBrains.Annotations;

namespace Fluent.API
{
    public static class LiquidLoader
    {
        // Don't construct these in the static constructor
        // in order to ensure Liquids is instantiated first.
        [UsedImplicitly]
        public static WaterLiquid Water;

        [UsedImplicitly]
        public static LavaLiquid Lava;

        [UsedImplicitly]
        public static HoneyLiquid Honey;

        internal static Dictionary<byte, ModLiquid> Liquids = new();
        internal static List<GlobalLiquid> Globals = new();

        public static int LiquidCount => Liquids.Count;

        public static ModLiquid GetLiquid(byte type) => Liquids.TryGetValue(type, out ModLiquid liquid) ? liquid : null;

        public static void RegisterLiquid(ModLiquid liquid)
        {
            Liquids.Add(liquid.Type, liquid);
            liquid.Type = (byte) (LiquidCount - 1);
        }

        public static void RegisterGlobal(GlobalLiquid global)
        {
            Globals.Add(global);
        }

        public static void Load()
        {
            Liquids = new Dictionary<byte, ModLiquid>();

            Water = new WaterLiquid();
            Lava = new LavaLiquid();
            Honey = new HoneyLiquid();
        }

        public static void Unload()
        {
            Liquids.Clear();
            Globals.Clear();
        }
    }
}
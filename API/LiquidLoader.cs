#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using Fluent.API.Defaults;
using Fluent.API.Modules;
using JetBrains.Annotations;
using TomatoLib.Common.Systems;

namespace Fluent.API
{
    public sealed class LiquidLoader : SingletonSystem<LiquidLoader>
    {
        // Don't construct these in the static constructor
        // in order to ensure Liquids is instantiated first.
        [UsedImplicitly]
        public WaterLiquid Water;

        [UsedImplicitly]
        public LavaLiquid Lava;

        [UsedImplicitly]
        public HoneyLiquid Honey;

        internal Dictionary<byte, ModLiquid> Liquids = new();
        internal List<GlobalLiquid> Globals = new();

        private List<ILiquidLoaderModule> Modules { get; } = new();

        public int LiquidCount => Liquids.Count;

        public ModLiquid GetLiquid(byte type) => Liquids.TryGetValue(type, out ModLiquid liquid) ? liquid : null;

        public void RegisterLiquid(ModLiquid liquid)
        {
            Liquids.Add(liquid.Type, liquid);
            liquid.Type = (byte) (LiquidCount - 1);
        }

        public void RegisterGlobal(GlobalLiquid global)
        {
            Globals.Add(global);
        }

        // If someone ever needs this...
        public void AddModule(ILiquidLoaderModule module) => Modules.Add(module);

        public override void Load()
        {
            Liquids = new Dictionary<byte, ModLiquid>();

            Water = new WaterLiquid();
            Lava = new LavaLiquid();
            Honey = new HoneyLiquid();

            foreach (ILiquidLoaderModule module in Modules)
                module.Load();
        }


        public void PostSetup()
        {
            foreach (ILiquidLoaderModule module in Modules)
                module.PostSetup(Liquids);
        }

        public override void Unload()
        {
            base.Unload();

            foreach (ILiquidLoaderModule module in Modules)
                module.Unload();

            Liquids.Clear();
            Globals.Clear();
        }
    }
}
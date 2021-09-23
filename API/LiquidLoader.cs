#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using Fluent.API.Defaults;
using Fluent.API.Modules;
using Fluent.API.Modules.Defaults;
using Terraria.ModLoader;
using TomatoLib.Common.Systems;

namespace Fluent.API
{
    public sealed class LiquidLoader : SingletonSystem<LiquidLoader>
    {
        internal Dictionary<byte, ModLiquid> Liquids = new();
        internal List<GlobalLiquid> Globals = new();

        private List<ILiquidLoaderModule> Modules { get; }

        public int LiquidCount => Liquids.Count;

        public LiquidLoader()
        {
            Modules = new List<ILiquidLoaderModule>
            {
                new LiquidRendererModule
                {
                    Loader = this
                }
            };
        }

        public ModLiquid GetLiquid(byte type) => Liquids.TryGetValue(type, out ModLiquid liquid) ? liquid : null;

        public void RegisterLiquid(ModLiquid liquid)
        {
            Liquids.Add(liquid.Type, liquid);
            liquid.Type = (byte) (LiquidCount - 1);
            ModTypeLookup<ModLiquid>.Register(liquid);
        }

        public void RegisterGlobal(GlobalLiquid global)
        {
            Globals.Add(global);
            ModTypeLookup<GlobalLiquid>.Register(global);
        }

        // If someone ever needs this...
        public void AddModule(ILiquidLoaderModule module) => Modules.Add(module);

        public override void OnModLoad()
        {
            Liquids = new Dictionary<byte, ModLiquid>();

            _ = new WaterLiquid();
            _ = new LavaLiquid();
            _ = new HoneyLiquid();

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
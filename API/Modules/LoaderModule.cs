#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using Terraria.ModLoader;

namespace Fluent.API.Modules
{
    public abstract class LoaderModule : ILiquidLoaderModule
    {
        public LiquidLoader Loader { get; set; }

        public virtual void Load()
        {
            ModContent.GetInstance<Fluent>().ModLogger.Info($"Loading module: {GetType().Name}");
        }

        public virtual void PostSetup(Dictionary<byte, ModLiquid> modLiquids)
        {
            ModContent.GetInstance<Fluent>().ModLogger.Info($"Setting up module: {GetType().Name}");
        }

        public virtual void Unload()
        {
            ModContent.GetInstance<Fluent>().ModLogger.Info($"Unloading module: {GetType().Name}");
        }
    }
}
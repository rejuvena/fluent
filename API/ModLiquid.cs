#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ModLoader;

namespace Fluent.API
{
    public abstract class ModLiquid : ModType
    {
        public int Type { get; internal set; }

        protected sealed override void Register()
        {
            LiquidLoader.RegisterLiquid(this);
            SetStaticDefaults();
        }
    }
}
#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ModLoader;

namespace Fluent.API
{
    public abstract class GlobalLiquid : ModType
    {
        protected sealed override void Register()
        {
            LiquidLoader.Instance.RegisterGlobal(this);
            SetStaticDefaults();
        }
    }
}
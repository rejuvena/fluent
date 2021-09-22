#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ID;
using Terraria.ModLoader;

namespace Fluent.API
{
    public abstract class ModLiquid : ModType
    {
        public byte Type { get; internal set; }

        public ModLiquidWaterStyle LiquidStyle { get; }

        public abstract int WaterfallLength { get; }

        public abstract float DefaultOpacity { get; }

        protected ModLiquid()
        {
            LiquidStyle = new ModLiquidWaterStyle(this);
        }

        protected sealed override void Register()
        {
            LiquidLoader.Instance.RegisterLiquid(this);
            ((ILoadable) LiquidStyle).Load(Mod);
            SetStaticDefaults();
        }

        public virtual int ChooseWaterfallStyle() => 0;

        public virtual int GetSplashDust() => DustID.Water;

        public virtual void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 0.88f;
            g = 0.96f;
            b = 1.015f;
        }
    }
}
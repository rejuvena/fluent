#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ModLoader;

namespace Fluent.API
{
    [Autoload(false)]
    public class ModLiquidWaterStyle : ModWaterStyle
    {
        public ModLiquid Liquid { get; }

        public override string Name { get; }

        public ModLiquidWaterStyle(ModLiquid liquid)
        {
            Liquid = liquid;

            Name = liquid.FullName + "_WaterStyle";
        }

        public override int ChooseWaterfallStyle() => Liquid.ChooseWaterfallStyle();

        public override int GetSplashDust() => Liquid.GetSplashDust();

        public override int GetDropletGore() => 0;

        public override void LightColorMultiplier(ref float r, ref float g, ref float b) =>
            Liquid.LightColorMultiplier(ref r, ref g, ref b);
    }
}
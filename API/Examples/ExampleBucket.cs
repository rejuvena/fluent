#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Fluent.API.Defaults;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Fluent.API.Examples
{
    /// <summary>
    ///     Bucket that places five bytes of water (out of 255), converts itself to a Honey Bucket when consumed, and only gets consumed on use during the day.
    /// </summary>
    public class ExampleBucket : BaseBucket
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.WaterBucket}";

        public override byte LiquidType => ModContent.GetInstance<WaterLiquid>().Type;

        public override byte LiquidPlacementAmount => 5;

        public override int EmptyItem { get; set; } = ItemID.HoneyBucket;

        public override bool ConsumeOnUsage => Main.dayTime;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Example Bucket");
            Tooltip.SetDefault("'Extra saucy?'");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.CloneDefaults(ItemID.WaterBucket);
        }
    }
}
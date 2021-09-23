#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ID;
using Terraria.ModLoader;

namespace Fluent.API
{
    // Behavior handled in Core\Globals\BucketGlobalItem.cs
    public abstract class BaseBucket : ModItem, IBucketItem
    {
        public abstract byte LiquidType { get; }

        public virtual byte LiquidPlacementAmount { get; set; } = byte.MaxValue;

        public virtual ModLiquid Liquid => LiquidLoader.Instance.GetLiquid(LiquidType);

        public virtual int EmptyItem { get; set; } = ItemID.EmptyBucket;

        // TODO: Just permit ConsumeItem?
        public virtual bool ConsumeOnUsage => true;
    }
}
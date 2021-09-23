#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

namespace Fluent.API
{
    public interface IBucketItem
    {
        byte LiquidType { get; }

        byte LiquidPlacementAmount { get; }

        ModLiquid Liquid { get; }

        int EmptyItem { get; }

        bool ConsumeOnUsage { get; }
    }
}
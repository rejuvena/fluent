#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ID;
using Terraria.ModLoader;

namespace Fluent.API.Defaults
{
    public class WaterLiquid : ModLiquid
    {
        public override int WaterfallLength => 10;

        public override float DefaultOpacity => 0.6f;

        internal WaterLiquid()
        {
            Type = LiquidID.Water; 
            ((ILoadable) this).Load(ModContent.GetInstance<Fluent>());
            ContentInstance.Register(this);
        }
    }
}
#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Terraria.ID;
using Terraria.ModLoader;

namespace Fluent.API.Defaults
{
    [Autoload(false)]
    public class LavaLiquid : ModLiquid
    {
        public override int WaterfallLength => 3;

        public override float DefaultOpacity => 0.95f;

        internal LavaLiquid()
        {
            Type = LiquidID.Lava;
            ((ILoadable) this).Load(ModContent.GetInstance<Fluent>());
        }
    }
}
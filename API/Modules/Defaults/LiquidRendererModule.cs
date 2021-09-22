#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using System.Linq;
using Fluent.Core.Cecil;
using Fluent.Mimics;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Liquid;
using OnLiquidRenderer = On.Terraria.GameContent.Liquid.LiquidRenderer;
using InternalPrepareDrawOrig = On.Terraria.GameContent.Liquid.LiquidRenderer.orig_InternalPrepareDraw;

namespace Fluent.API.Modules.Defaults
{
    public class LiquidRendererModule : LoaderModule
    {
        public override void PostSetup(Dictionary<byte, ModLiquid> modLiquids)
        {
            base.PostSetup(modLiquids);
            
            OnLiquidRenderer.InternalPrepareDraw += LiquidRendererOnInternalPrepareDraw;
        }

        public override void Unload()
        {
            base.Unload();

            OnLiquidRenderer.InternalPrepareDraw -= LiquidRendererOnInternalPrepareDraw;
        }

        [UsesMimickedType]
        public void LiquidRendererOnInternalPrepareDraw(InternalPrepareDrawOrig orig, LiquidRenderer self, Rectangle drawArea)
        {
            // int[] newWaterfallLength = Loader.Liquids.Values.Select(x => x.WaterfallLength).ToArray();
            float[] newDefaultOpacity = Loader.Liquids.Values.Select(x => x.DefaultOpacity).ToArray();

            // LiquidRendererMimic.WATERFALL_LENGTH = newWaterfallLength;
            LiquidRendererMimic.DEFAULT_OPACITY = newDefaultOpacity;

            orig(self, drawArea);
        }
    }
}
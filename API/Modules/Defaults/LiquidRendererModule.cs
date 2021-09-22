#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;
using System.Linq;
using Fluent.Core.Cecil;
using Fluent.Mimics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Liquid;
using Terraria.ID;
using OnLiquidRenderer = On.Terraria.GameContent.Liquid.LiquidRenderer;
using ILLiquidRenderer = IL.Terraria.GameContent.Liquid.LiquidRenderer;
using OnTileDrawing = On.Terraria.GameContent.Drawing.TileDrawing;

namespace Fluent.API.Modules.Defaults
{
    public class LiquidRendererModule : LoaderModule
    {
        public override void PostSetup(Dictionary<byte, ModLiquid> modLiquids)
        {
            base.PostSetup(modLiquids);

            OnLiquidRenderer.InternalPrepareDraw += LiquidRendererOnInternalPrepareDraw;
            ILLiquidRenderer.InternalDraw += ModifyDrawing;
            OnTileDrawing.DrawPartialLiquid += ModifyLiquidBehindTiles;
        }

        public override void Unload()
        {
            base.Unload();

            OnLiquidRenderer.InternalPrepareDraw -= LiquidRendererOnInternalPrepareDraw;
            ILLiquidRenderer.InternalDraw -= ModifyDrawing;
            OnTileDrawing.DrawPartialLiquid -= ModifyLiquidBehindTiles;
        }

        [UsesMimickedType]
        public void LiquidRendererOnInternalPrepareDraw(
            OnLiquidRenderer.orig_InternalPrepareDraw orig,
            LiquidRenderer self, 
            Rectangle drawArea)
        {
            // int[] newWaterfallLength = Loader.Liquids.Values.Select(x => x.WaterfallLength).ToArray();
            float[] newDefaultOpacity = Loader.Liquids.Values.Select(x => x.DefaultOpacity).ToArray();

            // LiquidRendererMimic.WATERFALL_LENGTH = newWaterfallLength;
            LiquidRendererMimic.DEFAULT_OPACITY = newDefaultOpacity;

            orig(self, drawArea);
        }

        public void ModifyDrawing(ILContext il)
        {
            /*ILCursor c = new(il);

            for (int i = 0; i < 5; i++)
                if (!c.TryGotoNext(MoveType.AfterLabel, x => x.MatchLdloca(11)))
                {
                    Fluent.Instance.ModLogger.PatchFailure("Terraria.GameContent.Liquid.LiquidRenderer", 
                        "InternalDraw", "ldloca", "11");
                    return;
                }

            if (!c.TryGotoNext(MoveType.AfterLabel, x => x.MatchStobj<Color>()))
            {
                Fluent.Instance.ModLogger.PatchFailure("Terraria.GameContent.Liquid.LiquidRenderer",
                    "InternalDraw", "stobj", "Microsoft.Xna.Color");
                return;
            }
            
            // load vertex colors
            c.Emit(OpCodes.Ldloc_S, (byte) 11);

            // get type
             c.Emit(OpCodes.Ldloc_3);
             c.Emit(OpCodes.Ldfld, typeof(LiquidRenderer).GetCachedNestedType("LiquidDrawCache").GetCachedField("Type"));

             c.EmitDelegate<Func<VertexColors, byte, VertexColors>>((colors, type) =>
            {
                Color color = colors.BottomLeftColor;
                color.A = (byte) (Loader.GetLiquid(type).DefaultOpacity / byte.MaxValue);
                return new VertexColors(color);
            });

            // set the returned value
            c.Emit(OpCodes.Stloc_S, (byte) 11);*/
        }

        public void ModifyLiquidBehindTiles(
            OnTileDrawing.orig_DrawPartialLiquid orig,
            TileDrawing self,
            Tile tileCache,
            Vector2 position,
            Rectangle liquidSize, 
            int liquidType, 
            Color aColor)
        {
            SlopeType slope = tileCache.Slope;

            aColor *= Loader.GetLiquid((byte) tileCache.LiquidType).DefaultOpacity;

            if (!TileID.Sets.BlocksWaterDrawingBehindSelf[tileCache.type] || slope == SlopeType.Solid)
            {
                Main.spriteBatch.Draw(TextureAssets.Liquid[liquidType].Value, position, liquidSize, aColor, 0f, default, 1f, SpriteEffects.None, 0f);
                return;
            }

            liquidSize.X += 18 * ((int) slope - 1);

            Main.spriteBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, liquidSize, aColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
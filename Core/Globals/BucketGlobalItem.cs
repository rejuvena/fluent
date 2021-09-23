#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using Fluent.API;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities;

namespace Fluent.Core.Globals
{
    public class BucketGlobalItem : GlobalItem
    {
        public override void HoldItem(Item item, Player player)
        {
            base.HoldItem(item, player);

            if (item.ModItem is not IBucketItem || !player.InInteractionRange(Player.tileTargetX, Player.tileTargetY))
                return;

            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = item.type;
        }

        // TODO: LiquidLoader calls.
        public override bool? UseItem(Item item, Player player)
        {
            bool? retVal = base.UseItem(item, player);

            if (retVal.HasValue && !retVal.Value)
                return false;

            if (item.ModItem is not IBucketItem bucket)
                return retVal;

            // Can't use the item? Too bad!
            if (!IsPlayerUs(player) || !player.InInteractionRange(Player.tileTargetX, Player.tileTargetY))
                return false; //retVal;

            // Get tile to place water in.
            Tile tile = TileHelpers.ParanoidTileRetrieval(Player.tileTargetX, Player.tileTargetY, out bool inWorld);

            // Not in the world? NaCat.
            if (!inWorld)
                return false;

            // Rest of this mimics vanilla behavior.

            // Block attempts to drop liquids on liquid tiles that don't match the type.
            if (tile.LiquidAmount != 0 && tile.LiquidType != bucket.LiquidType)
                return false;

            SoundEngine.PlaySound(19, player.position);
            tile.LiquidType = bucket.LiquidType;

            // Eventual optional modded behavior?
            /*
             tile.LiquidAmount = (byte) (tile.LiquidAmount + bucket.LiquidPlacementAmount);

                // TODO: This isn't compatible with buckets that could remove liquid (why would that be a thing?). Fix?
                if (tile.LiquidAmount < bucket.LiquidPlacementAmount)
                    tile.LiquidAmount = byte.MaxValue;
            */

            tile.LiquidAmount = bucket.LiquidPlacementAmount;

            WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);

            if (bucket.ConsumeOnUsage)
            {
                item.stack--;
                player.PutItemInInventoryFromItemUsage(bucket.EmptyItem, player.selectedItem);
            }

            // TODO: Use NetLiquidModule!
            // This should really just send a liquid update notification?
            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);

            return true;
        }

        public static bool IsPlayerUs(Player player) => Main.netMode != NetmodeID.MultiplayerClient
                                                        || player.whoAmI == Main.myPlayer;
    }
}
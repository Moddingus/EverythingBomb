using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EverythingBomb.Content.Projectiles;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using EverythingBomb.Content.Items;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.Assets;
using Newtonsoft.Json;
using Terraria.GameContent;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace EverythingBomb
{
	public class GlobalBomb : GlobalItem
    {
        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (item.type == ModContent.ItemType<BlockBomb>())
            {
                Texture2D texture = TextureAssets.Npc[5].Value;
                string name = item.Name.Split('(', ')')[1];
                for (int i = 0; i < ItemLoader.ItemCount; i++)
                {
                    if (ContentSamples.ItemsByType[i].Name == name)
                    {
                        texture = TextureAssets.Item[i].Value;
                        break;
                    }
                }
                spriteBatch.Draw(texture, item.position - Main.screenPosition + new Vector2(15, 16), new Rectangle(0, 0, texture.Width, texture.Height), lightColor, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 0);
            }
            base.PostDrawInWorld(item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
    }
}


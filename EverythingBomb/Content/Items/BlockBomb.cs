using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EverythingBomb.Content.Projectiles;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Assets;
using Newtonsoft.Json;
using System;
using Terraria.GameContent;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace EverythingBomb.Content.Items
{
	public class BlockBomb : ModItem
	{
		int tileType;
		int itemType = 1;
		string name;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BlockBomb"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault(
				"Will destroy your house\n" +
				"Warning: Expirmental Item");
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 26;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.shoot = 10;
			Item.shootSpeed = 5;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = 99;
		}

		public override void AddRecipes()
		{
            List<int> epicIngredients = new List<int>(); //Thanks absolute aquarian
            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                Item sample = ContentSamples.ItemsByType[i];
                if (sample.createTile >= 0)
                {
                    if (Main.tileSolid[sample.createTile])
                        epicIngredients.Add(i);
                }
            }

            foreach (int itemType in epicIngredients)
            {
                CreateRecipe().AddIngredient(itemType, 25).AddIngredient(ItemID.Bomb, 1).Register();
            }
        }
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = 0;
			int a = Projectile.NewProjectile(Item.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<BlockBombProj>(), 20 + ContentSamples.ItemsByType[itemType].value/100, 8, player.whoAmI, tileType, tileType);
			Main.projectile[a].ai[0] = tileType;
		}
		public override void OnCreate(ItemCreationContext context)
		{
			if (context is RecipeCreationContext recipeContext)
			{
                tileType = recipeContext.ConsumedItems[0].createTile;
				itemType = recipeContext.ConsumedItems[0].type;
                name = recipeContext.ConsumedItems[0].Name;
				Item.SetNameOverride($"BlockBomb ({name})");

			}
		}
        public override void SaveData(TagCompound tag)
        {
			tag["recipeItemName"] = name;
			tag["tileType"] = tileType;
            tag["itemType"] = itemType;
        }
        public override void LoadData(TagCompound tag)
        {
			name = tag.GetString("recipeItemName");
			tileType = tag.GetInt("tileType");
			itemType = tag.GetInt("itemType");
            Item.SetNameOverride($"BlockBomb ({name})");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Main.instance.LoadItem(itemType);
            Texture2D BlockTexture = TextureAssets.Item[itemType].Value;
            if (itemType != 1)
            {
                spriteBatch.Draw(BlockTexture, position + new Vector2(15, 16), new Rectangle(0, 0, BlockTexture.Width, BlockTexture.Height), Color.White, 0, Vector2.Zero, scale * 0.7f, SpriteEffects.None, 0);
            }
        }
        public override bool CanStack(Item item2)
        {
            if (item2.Name == Item.Name)
			{
				return true;
			}
			return false;
        }
        public override bool CanStackInWorld(Item item2)
        {
            if (item2.Name == Item.Name)
            {
                return true;
            }
            return false;
        }
    }
}
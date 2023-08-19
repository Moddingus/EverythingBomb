using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System.Linq;
using Terraria.ModLoader;

namespace EverythingBomb.Content.Projectiles
{
	public class BlockBombProj : ModProjectile
	{
        int t = -1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Block Bomb");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 32;

            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 60 * 3; //3 seconds
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            if (t == -1)
            {
                t = (int)Projectile.ai[1];
                Main.NewText(t);
            }
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 vel = Vector2.One.RotatedBy(MathHelper.TwoPi / i);
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Torch, vel.X * 4, vel.Y * 4, 0, default, 1.5f);
                Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.Center + Vector2.Normalize(Projectile.velocity) * 25, vel * 2, GoreID.Smoke1);
            }
                Vector2 tilepos = Projectile.Center / 16;
            int j;
            if (!Main.tile[(int)tilepos.X, (int)tilepos.Y].HasTile)
            {
                for (int i = -4; i <= 4; i += 1)
                {
                    for (j = -3; j <= 5; j += 1)
                    {
                        Vector2 offset = new Vector2(i, j);
                        Vector2 vector = tilepos + offset;
                        if (!Main.tile[(int)tilepos.X, (int)tilepos.Y].HasTile)
                        {
                            if (i == -4 || i == 4)
                            {
                                if (j + 4 > 3 && j + 4 < 7)
                                {
                                    WorldGen.PlaceTile((int)tilepos.X + j, (int)tilepos.Y + i, t, false, true);
                                    Tile tile = Main.tile[(int)tilepos.X + j, (int)tilepos.Y + i];
                                    tile.Slope = SlopeType.Solid;
                                }
                            }
                            if (i == -3 || i == 3)
                            {
                                if (j + 4 > 2 && j + 4 < 8)
                                {
                                    WorldGen.PlaceTile((int)tilepos.X + j, (int)tilepos.Y + i, t, false, true);
                                    Tile tile = Main.tile[(int)tilepos.X + j, (int)tilepos.Y + i];
                                    tile.Slope = SlopeType.Solid;
                                }
                            }
                            if (i == -2 || i == 2)
                            {
                                if (j + 4 > 1 && j + 4 < 9)
                                {
                                    WorldGen.PlaceTile((int)tilepos.X + j, (int)tilepos.Y + i, t, false, true);
                                    Tile tile = Main.tile[(int)tilepos.X + j, (int)tilepos.Y + i];
                                    tile.Slope = SlopeType.Solid;
                                }
                            }
                            if (i == -1 || i == 0 || i == 1)
                            {
                                WorldGen.PlaceTile((int)tilepos.X + j, (int)tilepos.Y + i, t, false, true);
                                Tile tile = Main.tile[(int)tilepos.X + j, (int)tilepos.Y + i];
                                tile.Slope = SlopeType.Solid;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.WithinRange(Projectile.Center, 72))
                    {
                        npc.StrikeNPC(Projectile.damage, 5, (Projectile.Center.X > npc.Center.X) ? -1 : 1);
                    }
                }
            }
            j = 0;
        }
        public override bool? CanCutTiles()
        {
            return true;
        }
    }
}


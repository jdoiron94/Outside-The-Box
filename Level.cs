using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace KineticCamp {

    public class Level {

        private Entity player;
        private Texture2D map;
        private List<Entity> npcs = new List<Entity>();
        private List<Projectile> projectiles = new List<Projectile>();

        public Level(Entity player, Texture2D map, Entity[] npcs) {
            this.player = player;
            this.map = map;
            this.npcs.AddRange(npcs);
        }

        public Entity getPlayer() {
            return player;
        }

        public Texture2D getMap() {
            return map;
        }

        public List<Entity> getNpcs() {
            return npcs;
        }

        public List<Projectile> getProjectiles() {
            return projectiles;
        }

        public int getX() {
            return (int) player.getLocation().X;
        }

        public int getY() {
            return (int) player.getLocation().Y;
        }

        public void deriveX(int x) {
            player.deriveX(x);
            foreach (Entity e in npcs) {
                if (e != null) {
                    e.deriveX(x);
                }
            }
        }

        public void deriveY(int y) {
            player.deriveY(y);
            foreach (Entity e in npcs) {
                if (e != null) {
                    e.deriveY(y);
                }
            }
        }

        public void addProjectile(Projectile projectile) {
            projectiles.Add(projectile);
        }

        public void draw(SpriteBatch batch) {
            batch.Draw(map, new Rectangle((int) player.getLocation().X, (int) player.getLocation().Y, map.Width, map.Height), Color.White);
            batch.Draw(player.getTexture(), player.getBounds(), Color.White);
            foreach (Entity e in npcs) {
                if (e != null && !e.isDead() && e.isOnScreen()) {
                    e.draw(batch);
                }
            }
            batch.Draw(player.getProjectile().getTexture(), player.getProjectile().getPosition(), Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace KineticCamp {

    public class CollisionManager {

        /*
         * Collision class meant to deal with ensuring player interactions with other objects and entities
         * do not collide with one another, ensuring no clipping.
         */

        private Entity player;
        private Level level;

        private Rectangle r1;
        private Rectangle r2;

        public CollisionManager(Entity player, Level level) {
            this.player = player;
            this.level = level;
        }

        /*
         * Returns the player instance
         */
        public Entity getPlayer() {
            return player;
        }

        /*
         * Returns the level instance
         */
        public Level getLevel() {
            return level;
        }

        /*
         * Returns true if x0 collides with x1, assuming t0's width; otherwise, false
         */
        public bool collides(Vector2 x0, Vector2 x1, Texture2D t0) {
            return Vector2.Distance(x0, x1) <= 4 + t0.Width;
        }

        public bool collides(Entity e0, Entity e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);

            return e0Rect.Intersects(e1Rect); 
        }

        public bool collides(Entity e0, GameObject e1)
        {
            Rectangle e0Rect = new Rectangle((int)e0.getDestination().X, (int)e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int)e1.getDestination().X, (int)e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);

            return e0Rect.Intersects(e1Rect);
        }

        public bool collides(GameObject e0, Entity e1)
        {
            Rectangle e0Rect = new Rectangle((int)e0.getDestination().X, (int)e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int)e1.getDestination().X, (int)e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);

            return e0Rect.Intersects(e1Rect);
        }

        public bool collides(GameObject e0, GameObject e1)
        {
            Rectangle e0Rect = new Rectangle((int)e0.getDestination().X, (int)e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int)e1.getDestination().X, (int)e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);

            return e0Rect.Intersects(e1Rect);
        }


        public bool isValid(Entity me) {
            foreach (GameObject g in level.getObjects()) {
                if (g != null) {
                    if (collides(me, g)) {
                        return false;
                    }
                }
            }
            foreach (Entity e in level.getNpcs()) {
                if (e != null && e != me) {
                    if (collides(me, e)) {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool isValid(GameObject me) {
            foreach (GameObject g in level.getObjects()) {
                if (g != null && g != me) {
                    if (collides(me, g)) {
                        return false;
                    }
                }
            }
            foreach (Entity e in level.getNpcs()) {
                if (e != null) {
                    if (collides(me, e)) {
                        return false;
                    }
                }
            }
            return true;
        }

        public void update(GameTime time) {

        }
    }
}

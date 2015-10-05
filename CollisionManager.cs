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

        /*
         * Returns true if the supposed position does not collide with other objects or entities; otherwise, false
         */
        public bool isValid(Vector2 position, Entity me) {
            foreach (GameObject g in level.getObjects()) {
                if (g != null) {
                    if (collides(position - new Vector2(0, me.getMovedY()), g.getLocation() + new Vector2(me.getMovedX(), 0), g.getTexture())) {
                        Console.WriteLine("player collision with GameObject");
                        return false;
                    }
                }
            }
            foreach (Entity e in level.getNpcs()) {
                if (e != null) {
                    if (collides(position - new Vector2(0, me.getMovedY()), e.getLocation() + new Vector2(me.getMovedX(), 0), e.getTexture())) {
                        Console.WriteLine("player collision with Goku");
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

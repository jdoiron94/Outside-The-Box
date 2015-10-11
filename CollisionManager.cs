using Microsoft.Xna.Framework;

namespace KineticCamp {

    public class CollisionManager {

        /*
         * Collision class meant to deal with ensuring player interactions with other objects and entities
         * do not collide with one another, ensuring no clipping.
         */

        private readonly Player player;
        private readonly Level level;
        
        public CollisionManager(Player player, Level level) {
            this.player = player;
            this.level = level;
        }

        /*
         * Returns the player instance
         */
        public Player getPlayer() {
            return player;
        }

        /*
         * Returns the level instance
         */
        public Level getLevel() {
            return level;
        }

        /*
         * Returns true if two entities collide; otherwise, false
         */
        public bool collides(Entity e0, Entity e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);
            return e0Rect.Intersects(e1Rect); 
        }

        /*
         * Returns true if an entity and object collide; otherwise, false
         */
        public bool collides(Entity e0, GameObject e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);
            return e0Rect.Intersects(e1Rect);
        }

        /*
         * Returns true if two objects collide; otherwise, false
         */
        public bool collides(GameObject e0, GameObject e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);
            return e0Rect.Intersects(e1Rect);
        }

        /*
         * Returns true if an entity's destination is valid and does not collide; otherwise, false
         */
        public bool isValid(Entity ent) {
            foreach (GameObject g in level.getObjects()) {
                if (g != null && g.isOnScreen(level.getGame())) {
                    if (collides(ent, g)) {
                        return false;
                    }
                }
            }
            foreach (Entity e in level.getNpcs()) {
                if (e != null && e != ent && e.isOnScreen(level.getGame())) {
                    if (collides(ent, e)) {
                        return false;
                    }
                }
            }
            return true;
        }

        /*
         * Returns true if an object's destination is valid and does not collide; otherwise, false
         */
        public bool isValid(GameObject obj) {
            foreach (GameObject g in level.getObjects()) {
                if (g != null && g != obj && g.isOnScreen(level.getGame())) {
                    if (collides(obj, g)) {
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

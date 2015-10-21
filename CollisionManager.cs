using Microsoft.Xna.Framework;
using System;

namespace KineticCamp {

    public class CollisionManager {

        /*
         * Collision class meant to deal with ensuring player interactions with other objects and entities
         * do not collide with one another, ensuring no clipping.
         */

        private readonly Player player;
        private Level level;
        
        public CollisionManager(Player player, Level level) {
            this.player = player;
            this.level = level;
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        /// <summary>
        /// Returns an instance of the level
        /// </summary>
        /// <returns>Returns an instance of the level</returns>
        public Level getLevel() {
            return level;
        }

        public void setLevel(Level level)
        {
            this.level = level; 
        }

        public bool playerSpotted(Level level)
        {
            Player player = level.getPlayer();
            Rectangle playerRec = player.getBounds();

            foreach (Npc npc in level.getNpcs())
            {
                Rectangle lineOfSight = npc.getLineOfSight();
                if (playerRec.Intersects(lineOfSight))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns whether or not two entities collide
        /// </summary>
        /// <param name="e0">The first entity</param>
        /// <param name="e1">The second entity</param>
        /// <returns>Returns true if the two entities collide; otherwise, false</returns>
        public bool collides(Entity e0, Entity e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);
            return e0Rect.Intersects(e1Rect); 
        }

        /// <summary>
        /// Returns whether or not an entity collides with a game object
        /// </summary>
        /// <param name="e0">The entity</param>
        /// <param name="e1">The game object</param>
        /// <returns>Returns true if the entity collides with the game object; otherwise, false</returns>
        public bool collides(Entity e0, GameObject e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);
            return e0Rect.Intersects(e1Rect);
        }

        public bool collides(Entity e0, Wall e1)
        {
            Rectangle e0Rect = new Rectangle((int)e0.getDestination().X, (int)e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = e1.getWallBounds(); 
            return e0Rect.Intersects(e1Rect);
        }

        public bool collides(Projectile p, Entity e) {
            return p.getBounds().Intersects(e.getBounds());
        }

        public bool collides(Projectile p, GameObject g) {
            return p.getBounds().Intersects(g.getBounds());
        }

        /// <summary>
        /// Returns whether or not two game objects collide
        /// </summary>
        /// <param name="e0">The first game object</param>
        /// <param name="e1">The second game object</param>
        /// <returns>Returns true if the two game objects collide; otherwise, false</returns>
        public bool collides(GameObject e0, GameObject e1) {
            Rectangle e0Rect = new Rectangle((int) e0.getDestination().X, (int) e0.getDestination().Y, e0.getTexture().Width, e0.getTexture().Height);
            Rectangle e1Rect = new Rectangle((int) e1.getDestination().X, (int) e1.getDestination().Y, e1.getTexture().Width, e1.getTexture().Height);
            return e0Rect.Intersects(e1Rect);
        }

        /// <summary>
        /// Returns whether or not an entity's movement is valid
        /// </summary>
        /// <param name="ent">The entity</param>
        /// <returns>Returns true if the entity's movement does not collide; otherwise, false</returns>
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
            foreach (Wall w in level.getWalls())
            {
                if(w !=null && w.isOnScreen(level.getGame()))
                {
                    if(collides(ent, w))
                    {
                        return false; 
                    }
                }
            }
            return ent != player ? !collides(ent, player) : true;
        }

        /// <summary>
        /// Returns whether or not a game object's movement is valid
        /// </summary>
        /// <param name="obj">The game object</param>
        /// <returns>Returns true if the game object's movement does not collide; otherwise, false</returns>
        public bool isValid(GameObject obj) {
            foreach (GameObject g in level.getObjects()) {
                if (g != null && g != obj && g.isOnScreen(level.getGame())) {
                    if (collides(obj, g)) {
                        return false;
                    }
                }
            }
            foreach (Wall w in level.getWalls())
            {
                if (w != null && w.isOnScreen(level.getGame()))
                {
                    if (collides(obj, w))
                    {
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

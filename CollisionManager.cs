namespace OutsideTheBox {

    /// <summary>
    /// Collision class which deals with interactions of objects, entities, and projectiles.
    /// It ensures such assets do not collide with one another
    /// </summary>

    public class CollisionManager {

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

        /// <summary>
        /// Sets the collision manager's current level
        /// </summary>
        /// <param name="level">The level to be set</param>
        public void setLevel(Level level) {
            this.level = level;
        }

        /// <summary>
        /// Checks if the player has been spotted in the level
        /// </summary>
        /// <param name="level">The level to check</param>
        /// <returns>Returns true if the player was within the npc's los; otherwise, false</returns>
        public bool playerSpotted(Level level) {
            foreach (Npc npc in level.getNpcs()) {
                if (level.getPlayer().getDestinationBounds().Intersects(npc.getLineOfSight())) {
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
            return e0.getDestinationBounds().Intersects(e1.getDestinationBounds());
        }

        /// <summary>
        /// Returns whether or not an entity collides with a game object
        /// </summary>
        /// <param name="e0">The entity</param>
        /// <param name="e1">The game object</param>
        /// <returns>Returns true if the entity collides with the game object; otherwise, false</returns>
        public bool collides(Entity e0, GameObject e1) {
            return e0.getDestinationBounds().Intersects(e1.getDestinationBounds());
        }

        /// <summary>
        /// Returns whether or not an entity collides with a wall
        /// </summary>
        /// <param name="e0">The entity</param>
        /// <param name="e1">The wall</param>
        /// <returns>Returns true if the entity collides with the wall; otherwise, false</returns>
        public bool collides(Entity e0, Wall e1) {
            return e0.getDestinationBounds().Intersects(e1.getBounds());
        }

        /// <summary>
        /// Returns whether or not a projectile collides with an entity
        /// </summary>
        /// <param name="p">The projectile</param>
        /// <param name="e">The entity</param>
        /// <returns>Returns true if the projectile collides with the entity; otherwise, false</returns>
        public bool collides(Projectile p, Entity e) {
            return p.getBounds().Intersects(e.getBounds());
        }

        /// <summary>
        /// Returns whether or not a projectile collides with an object
        /// </summary>
        /// <param name="p">The projectile</param>
        /// <param name="g">The object</param>
        /// <returns>Returns true if the projectile collides with the object; otherwise, false</returns>
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
            return e0.getDestinationBounds().Intersects(e1.getDestinationBounds());
        }

        /// <summary>
        /// Returns whether or not an entity's movement is valid
        /// </summary>
        /// <param name="ent">The entity</param>
        /// <returns>Returns true if the entity's movement does not collide; otherwise, false</returns>
        public bool isValid(Entity ent) {
            foreach (GameObject g in level.getObjects()) {
                if (g.isOnScreen(level.getGame())) {
                    if (collides(ent, g)) {
                        return false;
                    }
                }
            }
            foreach (Entity e in level.getNpcs()) {
                if (e != ent && e.isOnScreen(level.getGame())) {
                    if (collides(ent, e)) {
                        return false;
                    }
                }
            }
            foreach (Wall w in level.getWalls()) {
                if (w.isOnScreen(level.getGame())) {
                    if (collides(ent, w)) {
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
            foreach (Npc n in level.getNpcs()) {
                if (n.isOnScreen(level.getGame()) && collides(n, obj)) {
                    return false;
                }
            }
            foreach (GameObject g in level.getObjects()) {
                if (g != obj && g.isOnScreen(level.getGame()) && collides(obj, g)) {
                    return false;
                }
            }
            foreach (Wall w in level.getWalls()) {
                if (w.isOnScreen(level.getGame()) && collides(obj, w)) {
                    return false;
                }
            }
            foreach (Door d in level.getDoors()) {
                if (d.isOnScreen(level.getGame()) && collides(obj, d)) {
                    return false;
                }
            }
            return !collides(player, obj);
        }
    }
}

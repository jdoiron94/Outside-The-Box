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
        /// Returns the object's colliding entity
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <returns>Returns the entity colliding with the object; otherwise, null</returns>
        public Entity getEntityCollision(GameObject o) {
            foreach (Npc n in level.getNpcs()) {
                if (o.getDestinationBounds().Intersects(n.getDestinationBounds())) {
                    return n;
                }
            }
            return o.getDestinationBounds().Intersects(player.getDestinationBounds()) ? player : null;
        }

        /// <summary>
        /// Returns the object's colliding object
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <returns>Returns the object colliding with the specified object; otherwise, null</returns>
        public GameObject getObjectCollision(GameObject o) {
            foreach (GameObject g in level.getObjects()) {
                if (g != o && o.getDestinationBounds().Intersects(g.getDestinationBounds())) {
                    return g;
                }
            }
            foreach (Wall w in level.getWalls()) {
                if (w != o && o.getDestinationBounds().Intersects(w.getBounds())) {
                    return w;
                }
            }
            foreach (Door d in level.getDoors()) {
                if (d != o && o.getDestinationBounds().Intersects(d.getBounds())) {
                    return d;
                }
            }
            return null;
        }

        public void updatePressButtons(Entity e) {
            foreach (PressButton p in level.getPressButtons()) {
                bool pushed = false;
                if (!p.isDeactivated()) {
                    foreach (GameObject g in level.getObjects()) {
                        if (p != g && g.getDestinationBounds().Intersects(p.getBounds())) {
                            pushed = true;
                        }
                    }
                    foreach (Npc n in level.getNpcs()) {
                        if (n.getDestinationBounds().Intersects(p.getBounds())) {
                            pushed = true;
                        }
                    }
                    if (e.getDestinationBounds().Intersects(p.getBounds())) {
                        pushed = true;
                    }
                }
                p.setPushed(pushed);
            }
        }

        /// <summary>
        /// Returns the entity's colliding entity
        /// </summary>
        /// <param name="e">The entity to check</param>
        /// <returns>Returns the entity colliding with the specified entity; otherwise, null</returns>
        public Entity getEntityCollision(Entity e) {
            foreach (Npc n in level.getNpcs()) {
                if (n != e && e.getDestinationBounds().Intersects(n.getDestinationBounds())) {
                    return n;
                }
            }
            return e != player && e.getDestinationBounds().Intersects(player.getDestinationBounds()) ? player : null;
        }

        /// <summary>
        /// Returns the entity's colliding object
        /// </summary>
        /// <param name="e">The entity to check</param>
        /// <returns>Returns the object colliding with the entity; otherwise, null</returns>
        public GameObject getObjectCollision(Entity e) {
            foreach (GameObject g in level.getObjects()) {
                if (e.getDestinationBounds().Intersects(g.getDestinationBounds())) {
                    return g;
                }
            }
            foreach (Wall w in level.getWalls()) {
                if (e.getDestinationBounds().Intersects(w.getBounds())) {
                    return w;
                }
            }
            foreach (Door d in level.getDoors()) {
                if (e.getDestinationBounds().Intersects(d.getBounds())) {
                    return d;
                }
            }
            if (e == player) {
                foreach (Token t in level.getTokens()) {
                    if (e.getDestinationBounds().Intersects(t.getBounds()) && !t.isCollected()) {
                        return t;
                    }
                    foreach (Key k in level.getKeys()) {
                        if (e.getDestinationBounds().Intersects(k.getBounds()) && !k.isCollected()) {
                            return k;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns whether or not the entity collides with another entity
        /// </summary>
        /// <param name="e">The entity to check</param>
        /// <returns>Returns true if any other entity collides with the specified one; otherwise, false</returns>
        public bool hitEntity(Entity e) {
            return getEntityCollision(e) != null;
        }

        /// <summary>
        /// Returns whether or not the object collides with an entity
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <returns>Returns true if the object collides with an entity; otherwise, false</returns>
        public bool hitEntity(GameObject o) {
            return getEntityCollision(o) != null;
        }

        /// <summary>
        /// Returns whether or not the entity collides with an object
        /// </summary>
        /// <param name="e">The entity to check</param>
        /// <returns>Returns true if the entity collides with an object; otherwise, false</returns>
        public bool hitObject(Entity e) {
            return getObjectCollision(e) != null;
        }

        /// <summary>
        /// Returns whether or not the object collides with another object
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <returns>Returns true if the object collides with another object; otherwise, false</returns>
        public bool hitObject(GameObject o) {
            return getObjectCollision(o) != null;
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
        /// <param name="o">The object</param>
        /// <returns>Returns true if the projectile collides with the object; otherwise, false</returns>
        public bool collides(Projectile p, GameObject o) {
            return p.getBounds().Intersects(o.getBounds());
        }

        /// <summary>
        /// Returns whether or not an entity's movement is valid
        /// </summary>
        /// <param name="e">The entity</param>
        /// <returns>Returns true if the entity's movement does not collide; otherwise, false</returns>
        public bool isValid(Entity e) {
            return !hitEntity(e) && !hitObject(e);
        }

        /// <summary>
        /// Returns whether or not a game object's movement is valid
        /// </summary>
        /// <param name="o">The game object</param>
        /// <returns>Returns true if the game object's movement does not collide; otherwise, false</returns>
        public bool isValid(GameObject o) {
            return !hitEntity(o) && !hitObject(o);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace KineticCamp {

    public class Level {

        /*
         * Class which holds all pertinent information to any given level.
         */

        private Entity player;
        private Texture2D map;
        private List<Entity> npcs;
        private List<GameObject> objects;
        private List<Projectile> projectiles;
        private GameObject selectedObject;

        public Level(Entity player, Texture2D map, Entity[] npcs, GameObject[] objects) {
            this.player = player;
            this.map = map;
            this.npcs = new List<Entity>(npcs.Length);
            this.npcs.AddRange(npcs);
            this.objects = new List<GameObject>(objects.Length);
            this.objects.AddRange(objects);
            projectiles = new List<Projectile>();
            this.selectedObject = null;
        }

        /*
         * Returns the player instance
         */
        public Entity getPlayer() {
            return player;
        }

        /*
         * Returns the level's map texture
         */
        public Texture2D getMap() {
            return map;
        }

        /*
         * Returns a list of all npcs in the level
         */
        public List<Entity> getNpcs() {
            return npcs;
        }

        /*
         * Returns a list of all objects in the level
         */
        public List<GameObject> getObjects() {
            return objects;
        }

        /*
         * Returns a list of all active projectiles in the level
         */
        public List<Projectile> getProjectiles() {
            return projectiles;
        }

        public GameObject getSelectedObject()
        {
            return selectedObject;
        }

        public void setSelectedObject(int index)
        {
            this.selectedObject = objects[index];
        }

        /*
         * Derives the player's, NPCs', and objects' locations in terms of their x coordinates
         */
        public void deriveX(int x) {
            player.deriveX(x);
            foreach (GameObject g in objects) {
                if (g != null) {
                    g.deriveX(x);
                }
            }
            foreach (Entity e in npcs) {
                if (e != null) {
                    e.deriveX(x);
                    Console.WriteLine("npc x: " + e.getLocation().X);
                }
            }
        }

        /*
         * Derives the player's, NPCs', and objects' locations in terms of their y coordinates
         */
        public void deriveY(int y) {
            player.deriveY(y);
            foreach (GameObject g in objects) {
                if (g != null) {
                    g.deriveY(y);
                }
            }
            foreach (Entity e in npcs) {
                if (e != null) {
                    e.deriveY(y);
                    Console.WriteLine("npc y: " + e.getLocation().Y);
                }
            }
        }

        /*
         * Adds an active projectile to the level
         */
        public void addProjectile(Projectile projectile) {
            projectiles.Add(projectile);
        }

        /*
         * Updates the list of projectiles, removing inactive projectiles from the list 
         */
        public void updateProjectiles() {
            for (int i = 0; i < projectiles.Count; i++) {
                Projectile projectile = projectiles[i];
                if (projectile != null) {
                    projectile.update(player);
                    if (!projectile.isActive()) {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        /*
         * Updates the list of npcs, removing dead npcs from the list
         */
        public void updateNpcs() {
            for (int i = 0; i < npcs.Count; i++) {
                Entity npc = npcs[i];
                if (npc != null) {
                    if (npc.isDead()) {
                        npcs.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        /*
         * Draws the level's map, player, NPCs, and objects currently on screen in the game
         */
        public void draw(SpriteBatch batch, int mode) {
            batch.Draw(map, new Vector2(0, 0), Color.White);

            foreach (Projectile projectile in projectiles) {
                if (projectile != null) {
                    projectile.draw(batch);
                }
            }
            foreach (GameObject obj in objects) {
                if (obj != null && obj.isOnScreen()) {
                    if (mode == 0)
                    {
                        obj.draw(batch);
                    }
                    else if (mode == 1)
                    {
                        if (obj.isLiftable())
                        {
                            obj.drawSelectable(batch);
                        }
                        else
                        {
                            obj.draw(batch);
                        }
                    }
                    else
                    {
                        if (obj.isSelected())
                        {
                            obj.drawSelected(batch);
                        }
                        else if(obj.isLiftable())
                        {
                            obj.drawSelectable(batch);
                        }
                        else
                        {
                            obj.draw(batch);
                        }
                    }
                }
            }
            batch.Draw(player.getTexture(), player.getBounds(), Color.White);
            foreach (Entity e in npcs) {
                if (e != null && e.isOnScreen()) {
                    e.draw(batch);
                }
            }
        }
    }
}

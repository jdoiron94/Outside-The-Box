using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace KineticCamp {

    public class Level {

        /*
         * Class which holds all pertinent information to any given level.
         */

        private byte mode;

        private readonly Game1 game;
        private readonly Player player;
        private readonly Texture2D map;

        private GameObject selectedObject;
        private InputManager inputManager;

        private readonly List<Npc> npcs;
        private readonly List<GameObject> objects;
        private readonly List<DisplayBar> displayBars;

        private List<Projectile> projectiles;

        public Level(Game1 game, Player player, Texture2D map, Npc[] npcs, GameObject[] objects, DisplayBar[] displayBars) {
            this.game = game;
            this.player = player;
            this.map = map;
            this.npcs = new List<Npc>(npcs.Length);
            this.npcs.AddRange(npcs);
            this.objects = new List<GameObject>(objects.Length);
            this.objects.AddRange(objects);
            this.displayBars = new List<DisplayBar>(displayBars.Length);
            this.displayBars.AddRange(displayBars);
            selectedObject = null;
            projectiles = new List<Projectile>();
        }

        /*
         * Returns an instance of the game
         */
        public Game1 getGame() {
            return game;
        }

        /*
         * Returns the player instance
         */
        public Player getPlayer() {
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
        public List<Npc> getNpcs() {
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

        /*
         * Returns the currently selected object
         */
        public GameObject getSelectedObject() {
            return selectedObject;
        }

        /*
         * Sets the selected object
         */
        public void setSelectedObject(int index) {
            selectedObject = objects[index];
        }

        /*
         * Returns the telekinesis mode
         */
        public byte getMode() {
            return mode;
        }

        /*
         * Sets the telekinesis mode
         */
        public void setMode(byte mode) {
            this.mode = mode;
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
            foreach (Npc e in npcs) {
                if (e != null) {
                    e.deriveX(x);
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
            foreach (Npc e in npcs) {
                if (e != null) {
                    e.deriveY(y);
                }
            }
        }

        /*
         * Sets the level's input manager
         */
        public void setInputManager(InputManager inputManager) {
            this.inputManager = inputManager;
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
                    projectile.update(game, projectile.getOwner());
                    if (projectile.isActive()) {
                        foreach (Npc e in npcs) {
                            if (projectile.getOwner() == e) {
                                Rectangle pos = new Rectangle((int) projectile.getPosition().X, (int) projectile.getPosition().Y, projectile.getTexture().Width, projectile.getTexture().Height);
                                if (pos.Intersects(new Rectangle((int) player.getLocation().X + (player.getTexture().Width / 2), (int) player.getLocation().Y + (player.getTexture().Height / 2), player.getTexture().Width, player.getTexture().Height))) {
                                    projectile.setActive(false);
                                    inputManager.getPlayerManager().damagePlayer(5);
                                    Console.WriteLine(inputManager.getPlayerManager().getHealth());
                                }
                                break;
                            }
                            if (e != null && e.isOnScreen(game)) {
                                Rectangle pos = new Rectangle((int) projectile.getPosition().X, (int) projectile.getPosition().Y, projectile.getTexture().Width, projectile.getTexture().Height);
                                if (pos.Intersects(new Rectangle((int) e.getDestination().X + (e.getTexture().Width / 2), (int) e.getDestination().Y + (e.getTexture().Height / 2), e.getTexture().Width, e.getTexture().Height))) {
                                    projectile.setActive(false);
                                    e.deriveHealth(-5);
                                    Console.WriteLine("npc health: " + e.getCurrentHealth());
                                    break;
                                }
                            }
                        }
                        if (projectile.isActive()) {
                            foreach (GameObject e in objects) {
                                if (e != null && e.isOnScreen(game)) {
                                    Rectangle pos = new Rectangle((int) projectile.getPosition().X, (int) projectile.getPosition().Y, projectile.getTexture().Width, projectile.getTexture().Height);
                                    if (pos.Intersects(new Rectangle((int) e.getDestination().X + (e.getTexture().Width / 2), (int) e.getDestination().Y + (e.getTexture().Height / 2), e.getTexture().Width, e.getTexture().Height))) {
                                        projectile.setActive(false);
                                        break;
                                    }
                                }
                            }
                        }
                    }
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
        public void updateNpcs(GameTime time) {
            for (int i = 0; i < npcs.Count; i++) {
                Npc npc = npcs[i];
                if (npc != null) {
                    if (npc.isDead()) {
                        npcs.RemoveAt(i);
                        i--;
                    } else {
                        if (npc.getPath() != null) {
                            npc.getPath().update();
                        } else {
                            if (npc.isWithin(player)) {
                                npc.react(time, player);
                            }
                        }
                    }
                }
            }
        }

        /*
         * Draws the level's map, player, NPCs, and objects currently on screen in the game
         */
        public void draw(SpriteBatch batch) {
            batch.Draw(map, Vector2.Zero, Color.White);
            foreach (Projectile projectile in projectiles) {
                if (projectile != null) {
                    projectile.draw(batch);
                }
            }
            foreach (GameObject obj in objects) {
                if (obj != null && obj.isOnScreen(game)) {
                    obj.draw(batch, mode);
                }
            }
            foreach (Npc e in npcs) {
                if (e != null && e.isOnScreen(game)) {
                    e.draw(batch);
                }
            }
            batch.Draw(player.getTexture(), player.getBounds(), Color.White);
            foreach (DisplayBar d in displayBars) {
                if (d != null) {
                    d.draw(batch);
                }
            }
        }
    }
}

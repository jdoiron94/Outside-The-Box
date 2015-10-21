﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace KineticCamp {

    public class Level {

        /*
         * Class which holds all pertinent information to any given level.
         */

        private byte mode;
        private bool active;

        private readonly Game1 game;
        private readonly Player player;
        private readonly Texture2D map;

        private GameObject selectedObject;
        private InputManager inputManager;
        private CollisionManager collisionManager;
        private PlayerManager playerManager;

        private readonly List<Npc> npcs;
        private readonly List<GameObject> objects;
        private readonly List<DisplayBar> displayBars;
        private readonly List<ThoughtBubble> thoughts;

        private List<Token> Tokens;
        private List<Door> Doors;
        private readonly List<Wall> walls;

        private bool debug;
        private int index;
        private bool Key;

        private List<Projectile> projectiles;

        public Level(Game1 game, Player player, Texture2D map, Npc[] npcs, GameObject[] objects, DisplayBar[] displayBars, Token[] Tokens, Door[] Doors, Wall[] walls, ThoughtBubble[] thoughts, int index) {
            this.game = game;
            this.player = player;
            this.map = map;
            this.npcs = new List<Npc>(npcs.Length);
            this.npcs.AddRange(npcs);
            this.objects = new List<GameObject>(objects.Length);
            this.objects.AddRange(objects);
            this.displayBars = new List<DisplayBar>(displayBars.Length);
            this.displayBars.AddRange(displayBars);
            this.Tokens = new List<Token>(Tokens.Length);
            this.Tokens.AddRange(Tokens);
            this.Doors = new List<Door>(Doors.Length);
            this.Doors.AddRange(Doors);
            this.walls = new List<Wall>(walls.Length);
            this.walls.AddRange(walls);
            this.thoughts = new List<ThoughtBubble>(thoughts.Length);
            this.thoughts.AddRange(thoughts);
            active = true;
            selectedObject = null;
            debug = false;
            projectiles = new List<Projectile>();
            this.index = index;
        }

        /// <summary>
        /// Returns an instance of the game
        /// </summary>
        /// <returns>Returns an instance of the game</returns>
        public Game1 getGame() {
            return game;
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        /// <summary>
        /// Returns an instance of the level's map
        /// </summary>
        /// <returns>Returns an instance of the level's map</returns>
        public Texture2D getMap() {
            return map;
        }

        /// <summary>
        /// Returns all of the level's NPCs
        /// </summary>
        /// <returns>Returns a list of all of the NPCs in the level</returns>
        public List<Npc> getNpcs() {
            return npcs;
        }

        public List<ThoughtBubble> getThoughts() {
            return thoughts;
        }

        /// <summary>
        /// Returns all of the level's game objects
        /// </summary>
        /// <returns>Returns a list of all of the game objects in the level</returns>
        public List<GameObject> getObjects() {
            return objects;
        }

        /// <summary>
        /// Returns all of the level's active projectiles
        /// </summary>
        /// <returns>Returns a list of all of the active projectiles in the level</returns>
        public List<Projectile> getProjectiles() {
            return projectiles;
        }


        /// <summary>
        /// Returns all of the level's game tokens
        /// </summary>
        /// <returns>Returns a list of all of the game objects in the level</returns>
        public List<Token> getTokens() {
            return Tokens;
        }

        public List<Door> getDoors() {
            return Doors;
        }

        public List<Wall> getWalls() {
            return walls;
        }

        /// <summary>
        /// Returns the currently levitated object
        /// </summary>
        /// <returns>Returns the currently levitated object</returns>
        public GameObject getSelectedObject() {
            return selectedObject;
        }

        /// <summary>
        /// Sets the currently levitated object
        /// </summary>
        /// <param name="index">The object index to set as selected</param>
        public void setSelectedObject(int index) {
            selectedObject = objects[index];
        }

        /// <summary>
        /// Returns the current telekinesis mode
        /// </summary>
        /// <returns>Returns the current telekinesis mode</returns>
        public byte getMode() {
            return mode;
        }

        public bool isActive() {
            return active;
        }

        public void setActive(bool active) {
            this.active = active;
        }

        /// <summary>
        /// Sets the telekinesis mode
        /// </summary>
        /// <param name="mode">The telekinesis mode to be set</param>
        public void setMode(byte mode) {
            this.mode = mode;
        }

        public void removeToken(Token t) {
            Tokens.Remove(t);
        }

        /// <summary>
        /// Derives the player, all objects, and NPCs by the specified x amount
        /// </summary>
        /// <param name="x">The x amount to be derived by</param>
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

        /// <summary>
        /// Derives the player, all objects, and NPCs by the specified y amount
        /// </summary>
        /// <param name="y">The y amount to be derived by</param>
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

        /// <summary>
        /// Sets the input manager and player manager for the level
        /// </summary>
        /// <param name="inputManager">The InputManager instance to be set for the level</param>
        public void setInputManager(InputManager inputManager) {
            this.inputManager = inputManager;
            collisionManager = inputManager.getCollisionManager();
            playerManager = inputManager.getPlayerManager();
        }


        /// <summary>
        /// Toggles the debug setting to its inverse
        /// </summary>
        public void toggleDebug() {
            debug = !debug;
        }

        /// <summary>
        /// Adds a new projectile to the level's list of active projectiles
        /// </summary>
        /// <param name="projectile">The projectile to add to the projectile list</param>
        public void addProjectile(Projectile projectile) {
            projectiles.Add(projectile);
        }

        /// <summary>
        /// Updates the list of active projectiles, checking for collisions with objects, NPCs, and the player,
        /// while also removing inactive projectiles
        /// </summary>
        public void updateProjectiles() {
            for (int i = 0; i < projectiles.Count; i++) {
                Projectile projectile = projectiles[i];
                if (projectile != null) {
                    projectile.update(game, projectile.getOwner());
                    if (projectile.isActive()) {
                        foreach (Npc e in npcs) {
                            if (e != null && e.isOnScreen(game)) {
                                if (projectile.getOwner() == e) {
                                    if (collisionManager.collides(projectile, player)) {
                                        projectile.setActive(false);
                                        playerManager.damagePlayer(5);
                                        Console.WriteLine(playerManager.getHealth());
                                    }
                                    break;
                                } else if (collisionManager.collides(projectile, e)) {
                                    projectile.setActive(false);
                                    e.deriveHealth(-5);
                                    Console.WriteLine("npc health: " + e.getCurrentHealth());
                                    break;
                                }
                            }
                        }
                        if (projectile.isActive()) {
                            foreach (GameObject e in objects) {
                                if (e != null && e.isOnScreen(game) && collisionManager.collides(projectile, e)) {
                                    projectile.setActive(false);
                                    break;
                                }
                            }
                        }
                        if (projectile.isActive()) {
                            foreach (Wall w in walls) {
                                if (w != null && w.isOnScreen(game) && collisionManager.collides(projectile, w)) {
                                    projectile.setActive(false);
                                    break;
                                }
                            }
                        }
                        if (projectile.isActive()) {
                            foreach (Door d in Doors) {
                                if (d != null && d.isOnScreen(game) && collisionManager.collides(projectile, d)) {
                                    projectile.setActive(false);
                                    break;
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

        /// <summary>
        /// Updates the list of NPCs, removing dead ones and walking static AIPaths or reacting to the player,
        /// if they are of the right state
        /// </summary>
        /// <param name="time">The GameTime instance with which to make decisions regarding NPC behavior</param>
        public void updateNpcs(GameTime time) {
            for (int i = 0; i < npcs.Count; i++) {
                Npc npc = npcs[i];
                if (npc != null) {
                    if (npc.isDead()) {
                        npcs.RemoveAt(i);
                        i--;
                    } else {
                        npc.update(time);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the level's map, player, NPCs, and objects currently on screen
        /// </summary>
        /// <param name="batch">The SpriteBatch to perform the drawing</param>
        public void draw(SpriteBatch batch) {
            if (index == 1)
                batch.Draw(map, new Vector2(-300, -200), Color.White);
            else if (index == 2)
                batch.Draw(map, new Vector2(0, 0), Color.White);


            foreach (Projectile p in projectiles) {
                if (p != null) {
                    p.draw(batch);
                    if (debug) {
                        game.outline(batch, p.getBounds());
                    }
                }
            }
            foreach (GameObject o in objects) {
                if (o != null && o.isOnScreen(game)) {
                    o.draw(batch, mode);
                    if (debug) {
                        game.outline(batch, o.getBounds());
                    }
                }
            }
            foreach (Npc e in npcs) {
                if (e != null && e.isOnScreen(game)) {
                    e.draw(batch);
                    if (debug) {
                        game.outline(batch, e.getBounds());
                        game.outline(batch, e.getLineOfSight());
                    }
                }
            }
            batch.Draw(player.getTexture(), player.getBounds(), Color.White);
            if (debug) {
                game.outline(batch, player.getBounds());
            }
            foreach (DisplayBar d in displayBars) {
                if (d != null) {
                    d.draw(batch);
                }
            }

            foreach (Token t in Tokens) {
                if (t != null) {
                    t.draw(batch);
                    if (debug) {
                        game.outline(batch, t.getBounds());
                    }
                }
            }

            foreach (Door door in Doors) {
                if (door != null) {
                    door.draw(batch);
                    if (debug) {
                        game.outline(batch, door.getBounds());
                    }
                }
            }

            foreach (Wall wall in walls) {
                if (wall != null) {
                    wall.draw(batch);
                    if (debug) {
                        game.outline(batch, wall.getBounds());
                    }
                }
            }

            foreach (ThoughtBubble thought in thoughts) {
                if (thought != null) {
                    thought.draw(batch);
                }
            }
        }
    }
}

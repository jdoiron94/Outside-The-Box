﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;

namespace OutsideTheBox {

    /// <summary>
    /// Class which holds all information for a level
    /// </summary>

    public class Level {

        private int mode;
        private bool active;

        private readonly Game1 game;
        private readonly Player player;
        private readonly Texture2D map;
        private Song song;
        private Song song2;

        private Vector2 dropLocation;
        private Vector2 playerOrigin;
        private Vector2 playerReentryPoint;
        private GameObject selectedObject;
        private InputManager inputManager;
        private CollisionManager collisionManager;
        private PlayerManager playerManager;

        private List<Npc> npcs;
        private List<Npc> deadNpcs;
        private List<GameObject> objects;

        private Screen[] screens;
        private List<Token> tokens;
        private List<Key> keys;
        private List<Collectible> takenCollectibles;

        private List<Door> doors;
        private List<Wall> walls;
        private List<Cubicle> cubicles;
        private List<Projectile> projectiles;
        private List<PressButton> pressButtons;
        private List<Barrier> barriers;
        private List<Pit> pits;

        private bool debug;
        private bool looped;
        private readonly int index;
        private int textTicks;
        private string text;
        private string oldText;

        public Level(Game1 game, Player player, Texture2D map, Npc[] npcs, GameObject[] objects, int index) {
            this.game = game;
            this.player = player;
            this.map = map;
            this.npcs = new List<Npc>(npcs.Length);
            this.npcs.AddRange(npcs);
            this.deadNpcs = new List<Npc>(npcs.Length);
            this.objects = new List<GameObject>(objects.Length);
            this.objects.AddRange(objects);
            this.tokens = new List<Token>();
            this.doors = new List<Door>();
            this.keys = new List<Key>();
            this.barriers = new List<Barrier>();
            this.walls = new List<Wall>();
            this.pressButtons = new List<PressButton>();
            this.pits = new List<Pit>();
            this.sortObjects();
            this.cubicles = new List<Cubicle>();
            this.active = true;
            this.projectiles = new List<Projectile>();
            this.index = index;
            this.takenCollectibles = new List<Collectible>();
        }

        /// <summary>
        /// Sets the level's song
        /// </summary>
        /// <param name="song">The song to set</param>
        /// <param name="song2">The trimmed song to set</param>
        public void setSongs(Song song, Song song2) {
            this.song = song;
            this.song2 = song2;
        }

        /// <summary>
        /// Adds to the level's song ticks
        /// </summary>
        /// <param name="ticks">The amount of ticks to add</param>
        public void setLooped(bool looped) {
            this.looped = looped;
        }

        /// <summary>
        /// Returns whether or not we should play the trimmed song
        /// </summary>
        /// <returns>Returns true if we should play the trimmed song; otherwise, false</returns>
        public bool shouldLoop() {
            return looped;
        }

        /// <summary>
        /// Returns the level's index
        /// </summary>
        /// <returns>Returns the level's index</returns>
        public int getIndex() {
            return index;
        }

        /// <summary>
        /// Returns the level's song
        /// </summary>
        /// <returns>Returns the level's song</returns>
        public Song getSong() {
            return song;
        }

        /// <summary>
        /// Returns the level's trimmed song
        /// </summary>
        /// <returns>Returns the level's trimmed song</returns>
        public Song getSong2() {
            return song2;
        }

        /// <summary>
        /// Sets the game screens
        /// </summary>
        /// <param name="screens">The screens to set</param>
        public void setScreens(Screen[] screens) {
            this.screens = screens;
        }

        /// <summary>
        /// Returns the screen with the specified name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <returns>Returns the screen with the specified name</returns>
        public Screen getScreen(string name) {
            foreach (Screen s in screens) {
                if (s.getName() == name) {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Sorts the objects added to the level
        /// </summary>
        public void sortObjects() {
            List<GameObject> newObjects = new List<GameObject>();
            foreach (GameObject o in objects) {
                if (o is Token) {
                    tokens.Add((Token) o);
                } else if (o is Door) {
                    doors.Add((Door) o);
                } else if (o is Wall) {
                    walls.Add((Wall) o);
                } else if (o is PressButton) {
                    pressButtons.Add((PressButton) o);
                } else if (o is Key) {
                    keys.Add((Key) o);
                } else if (o is Barrier) {
                    barriers.Add((Barrier) o);
                } else if (o is Pit) {
                    pits.Add((Pit) o);
                } else {
                    newObjects.Add(o);
                }
            }
            objects = newObjects;
        }

        /// <summary>
        /// Handles adding of cubicles to the level
        /// </summary>
        /// <param name="cube">The cubicle to add to the level</param>
        public void addCubicle(Cubicle cube) {
            cubicles.Add(cube);
            foreach (GameObject go in cube.getObjects()) {
                if (go is Token) {
                    Token t = (Token) go;
                    tokens.Add(t);
                } else {
                    objects.Add(go);
                }
            }
            walls.AddRange(cube.getWalls());
        }

        /// <summary>
        /// Returns an instance of the player
        /// </summary>
        /// <returns>Returns an instance of the player</returns>
        public Player getPlayer() {
            return player;
        }

        /// <summary>
        /// Returns all of the level's NPCs
        /// </summary>
        /// <returns>Returns a list of all of the NPCs in the level</returns>
        public List<Npc> getNpcs() {
            return npcs;
        }

        /// <summary>
        /// Returns all of the level's game objects
        /// </summary>
        /// <returns>Returns a list of all of the game objects in the level</returns>
        public List<GameObject> getObjects() {
            return objects;
        }

        /// <summary>
        /// Returns the list of objects and keys in the level
        /// </summary>
        /// <returns>Returns the list of objects and keys in the level</returns>
        public List<GameObject> getObjectsAndKeys() {
            List<GameObject> list = new List<GameObject>();
            foreach (GameObject o in objects) {
                list.Add(o);
            }
            foreach (Key k in keys) {
                list.Add(k);
            }
            return list;
        }

        /// <summary>
        /// Resets the level's projectile list
        /// </summary>
        public void resetProjectiles() {
            projectiles = new List<Projectile>();
        }

        /// <summary>
        /// Returns all of the level's game tokens
        /// </summary>
        /// <returns>Returns a list of all of the game objects in the level</returns>
        public List<Token> getTokens() {
            return tokens;
        }

        /// <summary>
        /// Returns all of the level's game tokens
        /// </summary>
        /// <returns>Returns a list of all of the game objects in the level</returns>
        public List<Key> getKeys() {
            return keys;
        }

        /// <summary>
        /// Returns the level's door list
        /// </summary>
        /// <returns>Returns the door list</returns>
        public List<Door> getDoors() {
            return doors;
        }

        /// <summary>
        /// Returns a list of the level's pits
        /// </summary>
        /// <returns>Returns a list of the level's pits</returns>
        public List<Pit> getPits() {
            return pits;
        }

        /// <summary>
        /// Returns the level's pushButton list
        /// </summary>
        /// <returns>Returns the door list</returns>
        public List<PressButton> getPressButtons() {
            return pressButtons;
        }

        /// <summary>
        /// Returns the level's wall list
        /// </summary>
        /// <returns>Returns the wall list</returns>
        public List<Wall> getWalls() {
            return walls;
        }

        /// <summary>
        /// Returns a list of the level's barriers
        /// </summary>
        /// <returns>Returns a list of the level's barriers</returns>
        public List<Barrier> getBarriers() {
            return barriers;
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
        public int getMode() {
            return mode;
        }

        /// <summary>
        /// Returns whether or not the level is active
        /// </summary>
        /// <returns>Returns true if the level is active; otherwise, false</returns>
        public bool isActive() {
            return active;
        }

        /// <summary>
        /// Returns the collision manager
        /// </summary>
        /// <returns>Returns the collsion manager</returns>
        public CollisionManager getCollisionManager() {
            return collisionManager;
        }

        /// <summary>
        /// Sets the level's active status
        /// </summary>
        /// <param name="active">Active status bool to set</param>
        public void setActive(bool active) {
            this.active = active;
        }

        /// <summary>
        /// Resets the level's npcs
        /// </summary>
        public void resetNpcs() {
            foreach (Npc n in deadNpcs) {
                npcs.Add(n);
            }
            foreach (Npc n in npcs) {
                n.reset();
            }
            deadNpcs = new List<Npc>(npcs.Count);
        }

        /// <summary>
        /// Resets the level's objects
        /// </summary>
        public void resetObjects() {
            foreach (GameObject g in objects) {
                g.reset();
            }
        }

        /// <summary>
        /// Resets the level's doors
        /// </summary>
        public void resetDoors() {
            foreach (Door d in doors) {
                d.reset();
            }
        }

        /// <summary>
        /// Sets the collectible as collected
        /// </summary>
        /// <param name="c"></param>
        public void takeCollectible(Collectible c) {
            c.setCollected(true);
            takenCollectibles.Add(c);
            if (c is Token) {
                Token t = (Token) c;
                tokens.Remove(t);
            } else {
                Key k = (Key) c;
                keys.Remove(k);
            }
        }

        /// <summary>
        /// Resets the level's tokens
        /// </summary>
        public void resetCollectibles() {
            foreach (Collectible c in takenCollectibles) {
                c.setCollected(false);
                if (c is Token) {
                    Token t = (Token) c;
                    tokens.Add(t);
                } else {
                    Key k = (Key) c;
                    k.setUnlocked(false);
                    keys.Add(k);
                }
            }
            takenCollectibles = new List<Collectible>();
        }

        /// <summary>
        /// Sets the telekinesis mode
        /// </summary>
        /// <param name="mode">The telekinesis mode to be set</param>
        public void setMode(int mode) {
            this.mode = mode;
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
        /// Returns the level's player origin
        /// </summary>
        /// <returns>Returns the level's player origin</returns>
        public Vector2 getPlayerOrigin() {
            return playerOrigin;
        }

        /// <summary>
        /// Sets the level's player origin
        /// </summary>
        /// <param name="origin">The origin to set</param>
        public void setPlayerOrigin(Vector2 origin) {
            playerOrigin = origin;
        }

        /// <summary>
        /// Returns the player's level reentry point
        /// </summary>
        /// <returns>Returns the player's level reentry point</returns>
        public Vector2 getPlayerReentryPoint() {
            return playerReentryPoint;
        }

        /// <summary>
        /// Sets the player's level reentry point
        /// </summary>
        /// <param name="point">The reentry point to set</param>
        public void setPlayerReentryPoint(Vector2 point) {
            playerReentryPoint = point;
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
            SoundEffect effect = projectile.getSound();
            if (effect != null) {
                effect.Play();
            }
        }

        /// <summary>
        /// Updates the list of active projectiles, checking for collisions with objects, NPCs, and the player,
        /// while also removing inactive projectiles
        /// </summary>
        public void updateProjectiles() {
            for (int i = 0; i < projectiles.Count; i++) {
                Projectile projectile = projectiles[i];
                projectile.update(game, projectile.getOwner());
                if (projectile.isActive()) {
                    foreach (Npc n in npcs) {
                        if (projectile.getOwner() == n) {
                            if (collisionManager.collides(projectile, player)) {
                                projectile.setActive(false);
                                playerManager.damagePlayer(projectile.getDamage());
                                player.getHitsplat().playEffect();
                            }
                            break;
                        } else if (collisionManager.collides(projectile, n)) {
                            projectile.setActive(false);
                            n.deriveHealth(-projectile.getDamage());
                            n.setHit(true);
                            n.resetCombatTicks();
                            n.getDisplayBar().update(n.getCurrentHealth(), n.getMaxHealth());
                            n.getHitsplat().setHit("" + projectile.getDamage());
                            n.getHitsplat().playEffect();
                            int exp = 4 * projectile.getDamage();
                            playerManager.incrementExperience(exp);
                            PauseMenu pause = (PauseMenu) getScreen("Pause");
                            pause.setExperience(pause.getExperience() + exp);
                            inputManager.setDropText("+ " + exp + " EXP");
                            break;
                        }
                    }
                    if (projectile.isActive()) {
                        foreach (GameObject e in objects) {
                            if (collisionManager.collides(projectile, e)) {
                                projectile.setActive(false);
                                break;
                            }
                        }
                    }
                    if (projectile.isActive()) {
                        foreach (Wall w in walls) {
                            if (collisionManager.collides(projectile, w)) {
                                projectile.setActive(false);
                                break;
                            }
                        }
                    }
                    if (projectile.isActive()) {
                        foreach (Door d in doors) {
                            if (collisionManager.collides(projectile, d)) {
                                projectile.setActive(false);
                                break;
                            }
                        }
                    }
                    if (projectile.isActive()) {
                        foreach (Barrier b in barriers) {
                            if (!b.isOpen() && collisionManager.collides(projectile, b)) {
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

        /// <summary>
        /// Updates the list of NPCs, removing dead ones and walking static AIPaths or reacting to the player,
        /// if they are of the right state
        /// </summary>
        /// <param name="time">The GameTime instance with which to make decisions regarding NPC behavior</param>
        public void updateNpcs(GameTime time) {
            for (int i = 0; i < npcs.Count; i++) {
                Npc npc = npcs[i];
                if (npc.isDead()) {
                    npc.playEffect();
                    deadNpcs.Add(npc);
                    npcs.RemoveAt(i);
                    i--;
                } else {
                    npc.update(time);
                }
            }
        }

        /// <summary>
        /// Unlocks all doors
        /// </summary>
        public void unlockDoors() {
            foreach (Door d in doors) {
                d.setUnlocked(true);
            }
        }

        /// <summary>
        /// Draws the level's map, player, NPCs, and objects currently on screen
        /// </summary>
        /// <param name="batch">The SpriteBatch to perform the drawing</param>
        public void draw(SpriteBatch batch) {
            batch.Draw(map, Vector2.Zero, Color.White);
            foreach (Pit pit in pits) {
                pit.draw(batch);
                if (debug) {
                    game.outline(batch, pit.getBounds());
                }
            }
            foreach (PressButton p in pressButtons) {
                p.draw(batch);
                if (debug) {
                    game.outline(batch, p.getBounds());
                }
            }
            foreach (Projectile p in projectiles) {
                p.draw(batch);
                if (debug) {
                    game.outline(batch, p.getBounds());
                }
            }
            foreach (GameObject o in objects) {
                if (o.isOnScreen(game)) {
                    o.draw(batch, mode);
                }
                if (debug) {
                    game.outline(batch, o.getBounds());
                }
            }
            foreach (Cubicle c in cubicles) {
                c.draw(batch, debug);
            }
            foreach (Token t in tokens) {
                t.draw(batch, mode);
                if (debug && !t.isCollected()) {
                    game.outline(batch, t.getBounds());
                }
            }
            foreach (Key k in keys) {
                k.draw(batch, mode);
                if (debug && !k.isCollected()) {
                    game.outline(batch, k.getBounds());
                }
            }
            foreach (Door d in doors) {
                batch.Draw(d.getTexture(), d.getBounds(), Color.White);
                if (debug) {
                    game.outline(batch, d.getBounds());
                }
            }
            foreach (Wall w in walls) {
                batch.Draw(w.getTexture(), w.getBounds(), Color.White);
                if (debug) {
                    game.outline(batch, w.getBounds());
                }
            }
            foreach (Barrier b in barriers) {
                b.draw(batch);
                if (debug) {
                    game.outline(batch, b.getBounds());
                }
            }
            foreach (Npc n in npcs) {
                n.draw(batch);
                n.getDefinition().drawThought(batch);
                if (n.wasHit() && n.getCombatTicks() < 250) {
                    n.getDisplayBar().draw(batch);
                    n.getHitsplat().draw(batch);
                }
                if (debug) {
                    game.outline(batch, n.getBounds());
                    game.outline(batch, n.getLineOfSight());
                }
            }
            batch.Draw(player.getTexture(), player.getBounds(), Color.White);
            if (debug) {
                game.outline(batch, player.getBounds());
            }
            playerManager.getKeyBox().draw(batch);
            playerManager.getHealthBar().draw(batch);
            playerManager.getManaBar().draw(batch);
            playerManager.getPowerBar().draw(batch);
            oldText = text;
            text = inputManager.getDropText();
            if (text != null) {
                if (textTicks < 300) {
                    if (text != oldText) {
                        textTicks = 0;
                    }
                    Vector2 size = game.getDropFont().MeasureString(text);
                    dropLocation = new Vector2(game.getWidth() / 2.0F - (size.X / 2.0F), game.getHeight() / 2.0F - (size.Y / 2.0F));
                    batch.DrawString(game.getDropFont(), text, dropLocation, Color.DodgerBlue);
                    textTicks++;
                } else {
                    text = null;
                    textTicks = 0;
                    inputManager.setDropText(null);
                }
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;

namespace KineticCamp {

    public class InputManager {

        /*
         * Class which handles all input given to the game. To be handled according to the ScreenManager's current screen state,
         * in the future.
         */

        private readonly Game1 game;
        private readonly Player player;
        private readonly Level level;
        private readonly PlayerManager playerManager;
        private readonly CollisionManager collisionManager;
        private readonly ScreenManager screenManager;

        private GameObject selectedObject;

        private ButtonState lastState;
        private ButtonState state;

        private readonly int velocity;
        private readonly int midX;
        private readonly int midY;

        private KeyboardState lastKeyState;
        private KeyboardState currentKeyState;

        public InputManager(Game1 game, Player player, Level level, PlayerManager playerManager, Screen[] screens) {
            this.game = game;
            this.player = player;
            this.level = level;
            this.playerManager = playerManager; 
            collisionManager = new CollisionManager(player, level);
            screenManager = new ScreenManager(screens[1], screens);
            selectedObject = null;
            velocity = player.getVelocity();
            midX = game.getMidX();
            midY = game.getMidY();
            lastKeyState = new KeyboardState();
            currentKeyState = new KeyboardState();
        }

        /*
         * Returns the game instance
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
         * Returns the level instance
         */
        public Level getLevel() {
            return level;
        }

        /*
        * Returns the Player Manager
        */
        public PlayerManager getPlayerManager() {
            return playerManager;
        }

        /*
         * Handles all user input to the game, depending on the keyboard/mouse states and the screen's state.
         */
        public void update(GameTime time) {
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            Screen active = screenManager.getActiveScreen();
            if (active.getSong() != null && !active.isSongPlaying()) {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(active.getSong());
                active.setSongPlaying(true);
            }
            if (screenManager.getActiveScreen().getName() == "Normal") {
                if (playerManager.getHealthCooldown() == 35) {
                    playerManager.regenerateHealth();
                    playerManager.regenerateMana();
                }
                if (currentKeyState.IsKeyDown(Keys.Escape)) {
                    game.Exit();
                } else if (currentKeyState.IsKeyDown(Keys.W)) {
                    player.setDirection(Direction.NORTH);
                    player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y - velocity));
                    if (player.getDestination().Y > 0 && collisionManager.isValid(player)) {
                        player.deriveY(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.S)) {
                    player.setDirection(Direction.SOUTH);
                    player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y + velocity));
                    if (player.getDestination().Y < midY * 2 && collisionManager.isValid(player)) {
                        player.deriveY(velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.A)) {
                    player.setDirection(Direction.WEST);
                    player.setDestination(new Vector2(player.getLocation().X - velocity, player.getLocation().Y));
                    if (player.getDestination().X > 0 && collisionManager.isValid(player)) {
                        player.deriveX(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.D)) {
                    player.setDirection(Direction.EAST);
                    player.setDestination(new Vector2(player.getLocation().X + velocity, player.getLocation().Y));
                    if (player.getDestination().X < midX * 2 && collisionManager.isValid(player)) {
                        player.deriveX(velocity);
                    }
                } else {
                    player.setDestination(player.getLocation());
                }
                if (currentKeyState.IsKeyDown(Keys.Space)) {
                    double totalMilliseconds = time.TotalGameTime.TotalMilliseconds;
                    if ((player.getLastFired() == -1 || totalMilliseconds - player.getLastFired() >= player.getProjectile().getCooldown()) && playerManager.getMana() >= 5) {
                        level.addProjectile(player.createProjectile(totalMilliseconds));
                        playerManager.depleteMana(5);
                    }
                } 
                if (lastKeyState.IsKeyDown(Keys.X) && currentKeyState.IsKeyUp(Keys.X)) {
                    level.setMode(1);
                    screenManager.setActiveScreen(2);
                    Console.WriteLine("Entered telekinesis mode!");
                }
                if (currentKeyState.IsKeyDown(Keys.P)) {
                    playerManager.damagePlayer(2);
                }
            } else if (screenManager.getActiveScreen().getName() == "Telekinesis-Select") {
                lastState = state;
                state = Mouse.GetState().LeftButton;
                playerManager.setManaDrainRate(5);
                if (currentKeyState.IsKeyDown(Keys.Escape)) {
                    game.Exit();
                } else if (lastState == ButtonState.Pressed && state == ButtonState.Released) {
                    foreach (GameObject obj in level.getObjects()) {
                        if (obj != null && obj.isLiftable()) {
                            Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                            if (p != null && obj.getBounds().Contains(p)) {
                                obj.setSelected(true);
                                selectedObject = obj;
                                level.setMode(2);
                                screenManager.setActiveScreen(3);
                            }
                        }
                    }
                } else if (lastKeyState.IsKeyDown(Keys.X) && currentKeyState.IsKeyUp(Keys.X)) {
                    level.setMode(0);
                    screenManager.setActiveScreen(1);
                    Console.WriteLine("Exited telekinesis mode.");
                }
            } else if (screenManager.getActiveScreen().getName() == "Telekinesis-Move") {
                playerManager.updateManaDrainRate();
                if (playerManager.getManaDrainRate() == 5) {
                    playerManager.depleteMana(1);
                }
                if (currentKeyState.IsKeyDown(Keys.Escape)) {
                    game.Exit();
                } else if (currentKeyState.IsKeyDown(Keys.W)) {
                    selectedObject.setDirection(Direction.NORTH);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y - velocity));
                    if (selectedObject.getDestination().Y > 0 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveY(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.S)) {
                    selectedObject.setDirection(Direction.SOUTH);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y + velocity));
                    if (selectedObject.getDestination().Y < midY * 2 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveY(velocity);
                        if (playerManager.getManaDrainRate() == 5) {
                            playerManager.depleteMana(2);
                        }
                    }
                } else if (currentKeyState.IsKeyDown(Keys.A)) {
                    selectedObject.setDirection(Direction.WEST);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X - velocity, selectedObject.getLocation().Y));
                    if (selectedObject.getDestination().X > 0 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveX(-velocity);
                        if (playerManager.getManaDrainRate() == 5) {
                            playerManager.depleteMana(2);
                        }

                    }
                } else if (currentKeyState.IsKeyDown(Keys.D)) {
                    selectedObject.setDirection(Direction.EAST);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X + velocity, selectedObject.getLocation().Y));
                    if (selectedObject.getDestination().X < midX * 2 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveX(velocity);
                        if (playerManager.getManaDrainRate() == 5) {
                            playerManager.depleteMana(2);
                        }
                    }
                } else {
                    selectedObject.setDestination(selectedObject.getLocation()); //if no movement keys are pressed, destination is same as location
                }
                if (currentKeyState.IsKeyDown(Keys.Space)) {
                    //throw object
                } 
                if ((lastKeyState.IsKeyDown(Keys.X) && currentKeyState.IsKeyUp(Keys.X)) || playerManager.getMana() == 0) {
                    selectedObject.setSelected(false);
                    selectedObject = null;
                    level.setMode(0);
                    screenManager.setActiveScreen(1);
                    Console.WriteLine("Exited telekinesis mode.");
                }
            }
        }
    }
}
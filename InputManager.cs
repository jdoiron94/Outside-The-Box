using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            /* Normal gameplay (player has control of character's movement*/
            if (screenManager.getActiveScreen().getName() == "Normal") {
                if (playerManager.getHealthCooldown() == 35) {
                    playerManager.regenerateHealth();
                }
                if (currentKeyState.IsKeyDown(Keys.Escape)) {
                    game.Exit();
                } else if (currentKeyState.IsKeyDown(Keys.W)) {
                    player.setDirection(Direction.NORTH);
                    player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y - velocity));
                    if (player.getLocation().Y + velocity > 0 && collisionManager.isValid(player)) {
                        player.deriveY(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.S)) {
                    player.setDirection(Direction.SOUTH);
                    player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y + velocity));
                    if (player.getLocation().Y + velocity < midY * 2 && collisionManager.isValid(player)) {
                        player.deriveY(velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.A)) {
                    player.setDirection(Direction.WEST);
                    player.setDestination(new Vector2(player.getLocation().X - velocity, player.getLocation().Y));
                    if (player.getLocation().X + velocity > 0 && collisionManager.isValid(player)) {
                        player.deriveX(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.D)) {
                    player.setDirection(Direction.EAST);
                    player.setDestination(new Vector2(player.getLocation().X + velocity, player.getLocation().Y));
                    if (player.getLocation().X + velocity < midX * 2 && collisionManager.isValid(player)) {
                        player.deriveX(velocity);
                    }
                } else {
                    player.setDestination(player.getLocation()); //if no movement keys are pressed, destination is same as location
                }
                if (currentKeyState.IsKeyDown(Keys.Space)) {
                    double totalMilliseconds = time.TotalGameTime.TotalMilliseconds;
                    if (player.getLastFired() == -1 || totalMilliseconds - player.getLastFired() >= player.getProjectile().getCooldown()) {
                        level.addProjectile(player.createProjectile(totalMilliseconds));
                    }
                } 
                if (lastKeyState.IsKeyDown(Keys.X) && currentKeyState.IsKeyUp(Keys.X)) {
                    //switch to telekinesis-select mode (player clicks a liftable object to select it)
                    screenManager.setActiveScreen(2);
                    Console.WriteLine("Entered telekinesis mode!");
                }
                if (currentKeyState.IsKeyDown(Keys.P)) {
                    playerManager.damagePlayer(2);
                }
            }
            /* Just entered telekinesis mode (player uses mouse to select a liftable object)*/
            else if (screenManager.getActiveScreen().getName() == "Telekinesis-Select") {
                level.setMode(1); 
                lastState = state;
                state = Mouse.GetState().LeftButton;
                if (lastState == ButtonState.Pressed && state == ButtonState.Released) {
                    foreach (GameObject obj in level.getObjects()) {
                        if (obj != null && obj.isLiftable()) {
                            Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                            if (p != null && obj.getBounds().Contains(p)) {
                                obj.setSelected(true);
                                selectedObject = obj;
                                screenManager.setActiveScreen(3);
                            }
                        }
                    }
                } else if (lastKeyState.IsKeyDown(Keys.X) && currentKeyState.IsKeyUp(Keys.X)) {
                    // player is exiting telekinesis mode
                    screenManager.setActiveScreen(1);
                    level.setMode(0);
                    Console.WriteLine("Exited telekinesis mode.");
                }
            }
            /* Telekinetic lifting mode (player controls the selected object's movement)*/
            else if (screenManager.getActiveScreen().getName() == "Telekinesis-Move") {
                level.setMode(2);
                if (currentKeyState.IsKeyDown(Keys.Escape)) {
                    game.Exit();
                } else if (currentKeyState.IsKeyDown(Keys.W)) {
                    selectedObject.setDirection(Direction.NORTH);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y - velocity));
                    if (selectedObject.getLocation().Y + velocity > 0 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveY(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.S)) {
                    selectedObject.setDirection(Direction.SOUTH);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y + velocity));
                    if (selectedObject.getLocation().Y - velocity < midY * 2 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveY(velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.A)) {
                    selectedObject.setDirection(Direction.WEST);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X - velocity, selectedObject.getLocation().Y));
                    if (selectedObject.getLocation().X + velocity > 0 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveX(-velocity);
                    }
                } else if (currentKeyState.IsKeyDown(Keys.D)) {
                    selectedObject.setDirection(Direction.EAST);
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X + velocity, selectedObject.getLocation().Y));
                    if (selectedObject.getLocation().X - velocity < midX * 2 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveX(velocity);
                    }
                } else {
                    selectedObject.setDestination(selectedObject.getLocation()); //if no movement keys are pressed, destination is same as location
                }
                if (currentKeyState.IsKeyDown(Keys.Space)) {
                    //throw object
                } 
                if (lastKeyState.IsKeyDown(Keys.X) && currentKeyState.IsKeyUp(Keys.X)) {
                    //deselect object
                    selectedObject.setSelected(false);
                    selectedObject = null;
                    //switch back to normal screen state (control over character)
                    screenManager.setActiveScreen(1);
                    level.setMode(0);
                    Console.WriteLine("Exited telekinesis mode.");
                }
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;

namespace KineticCamp {

    public class InputManager {

        /*
         * Class which handles all input given to the game. To be handled according to the ScreenManager's current screen state,
         * in the future.
         */

        private readonly Game1 game;
        private readonly Player player;
        private Level level;
        private readonly Menu pauseMenu;
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

        private int ticks;
        private bool stagnant;

        private const byte WAIT = 0x4;

        public InputManager(Game1 game, Player player, Level level, Menu pauseMenu, PlayerManager playerManager, Screen[] screens) {
            this.game = game;
            this.player = player;
            this.level = level;
            this.pauseMenu = pauseMenu;
            this.playerManager = playerManager; 
            collisionManager = new CollisionManager(player, level);
            screenManager = new ScreenManager(screens[1], screens);
            selectedObject = null;
            velocity = player.getVelocity();
            midX = game.getMidX();
            midY = game.getMidY();
            lastKeyState = new KeyboardState();
            currentKeyState = new KeyboardState();
            ticks = 0;
            stagnant = false;
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
        /// Returns an instance of the level
        /// </summary>
        /// <returns>Returns an instance of the level</returns>
        public Level getLevel() {
            return level;
        }

        public Menu getMenu()
        {
            return pauseMenu;
        }

        /// <summary>
        /// Returns an instance of the player manager
        /// </summary>
        /// <returns>Returns an instance of the player manager</returns>
        public PlayerManager getPlayerManager() {
            return playerManager;
        }

        public CollisionManager getCollisionManager() {
            return collisionManager;
        }

        public ScreenManager getScreenManager()
        {
            return screenManager;
        }

        /// <summary>
        /// Controls updating of the game based on the current screen state and mouse/keyboard input
        /// </summary>
        /// <param name="time">The GameTime to update with respect to</param>
        public void update(GameTime time) {
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            Screen active = screenManager.getActiveScreen();
            if (active.getSong() != null && !active.isSongPlaying()) {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(active.getSong());
                active.setSongPlaying(true);
            }

            if (lastKeyState.IsKeyDown(Keys.F1) && currentKeyState.IsKeyUp(Keys.F1)) {
                level.toggleDebug();
            }

            if (active.getName() == "Normal") {
                if (playerManager.getHealthCooldown() == 35) {
                    playerManager.regenerateHealth();
                    playerManager.regenerateMana();
                }

                List<Token> temp = level.getTokens();
                for (int i = 0; i < temp.Count; i++)
                {
                    if (collisionManager.collides(player, temp[i]))
                    {
                        playerManager.incrementExperience(temp[i].getExp());
                        level.removeToken(temp[i]);
                    }
                }

                List<Door> doors = level.getDoors(); 
                for (int i = 0; i < doors.Count; i++)
                {
                    if (collisionManager.collides(player, doors[i]))
                    {
                        if(doors[i].getNext()==true)
                        {
                            level.setActive(false);
                            game.setLevelIndex(game.getLevelIndex() + 1);
                            level = game.getLevelByIndex(game.getLevelIndex());
                        }
                        else
                        {
                           level.setActive(false);
                           game.setLevelIndex(game.getLevelIndex() - 1);
                           level = game.getLevelByIndex(game.getLevelIndex());  
                        }

                        level.setActive(true);
                        level.setInputManager(this);
                        game.getLevel().setActive(false);
                        game.setLevel(level);
                        collisionManager.getLevel().setActive(false);
                        collisionManager.setLevel(level);

                        if (doors[i].getDirection() == Direction.EAST)
                            player.setX(30);
                        else if (doors[i].getDirection() == Direction.WEST)
                            player.setX(700);
                        else if (doors[i].getDirection() == Direction.NORTH)
                            player.setY(30);
                        else
                            player.setY(400);
                    }
                }

                if (lastKeyState.IsKeyDown(Keys.H) && currentKeyState.IsKeyUp(Keys.H))
                {
                    player.setX(0);
                    level.setActive(false);
                    level = game.getLevelByIndex(1);
                    level.setActive(true);
                    level.setInputManager(this);
                    game.getLevel().setActive(false);
                    game.setLevel(level);
                    collisionManager.getLevel().setActive(false);
                    collisionManager.setLevel(level);
                }

                if (lastKeyState.IsKeyDown(Keys.G) && currentKeyState.IsKeyUp(Keys.G))
                {
                    level.setActive(false);
                    level = game.getLevelByIndex(0);
                    level.setActive(true);
                    level.setInputManager(this);
                    game.getLevel().setActive(false);
                    game.setLevel(level);
                    collisionManager.getLevel().setActive(false);
                    collisionManager.setLevel(level);
                    
                }

                if (currentKeyState.IsKeyDown(Keys.Escape)) {
                    game.Exit();
                } else if (currentKeyState.IsKeyDown(Keys.W)) {
                    player.setDirection(Direction.NORTH);
                    player.updateMovement();
                    player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y - velocity));
                    if (player.getDestination().Y >= 0 && collisionManager.isValid(player)) {
                        player.deriveY(-velocity);
                    } else {
                        Console.WriteLine("can't walk to " + player.getDestination());
                    }
                } else if (lastKeyState.IsKeyDown(Keys.W) && currentKeyState.IsKeyUp(Keys.W)) {
                    stagnant = true;
                } else if (currentKeyState.IsKeyDown(Keys.S)) {
                    player.setDirection(Direction.SOUTH);
                    player.updateMovement();
                    player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y + velocity));
                    if (player.getDestination().Y <= midY * 2 && collisionManager.isValid(player)) {
                        player.deriveY(velocity);
                    }
                } else if (lastKeyState.IsKeyDown(Keys.S) && currentKeyState.IsKeyUp(Keys.S)) {
                    stagnant = true;
                } else if (currentKeyState.IsKeyDown(Keys.A)) {
                    player.setDirection(Direction.WEST);
                    player.updateMovement();
                    player.setDestination(new Vector2(player.getLocation().X - velocity, player.getLocation().Y));
                    if (player.getDestination().X >= 0 && collisionManager.isValid(player)) {
                        player.deriveX(-velocity);
                    }
                } else if (lastKeyState.IsKeyDown(Keys.A) && currentKeyState.IsKeyUp(Keys.A)) {
                    stagnant = true;
                } else if (currentKeyState.IsKeyDown(Keys.D)) {
                    player.setDirection(Direction.EAST);
                    player.updateMovement();
                    player.setDestination(new Vector2(player.getLocation().X + velocity, player.getLocation().Y));
                    if (player.getDestination().X <= midX * 2 && collisionManager.isValid(player)) {
                        player.deriveX(velocity);
                    }
                } else if (lastKeyState.IsKeyDown(Keys.D) && currentKeyState.IsKeyUp(Keys.D)) {
                    stagnant = true;
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
                if (stagnant) {
                    if (ticks >= WAIT) {
                        player.updateStill();
                        ticks = 0;
                        stagnant = false;
                    } else {
                        ticks++;
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
                if (lastKeyState.IsKeyDown(Keys.M) && currentKeyState.IsKeyUp(Keys.M))
                {
                    screenManager.setActiveScreen(0);
                    level.setActive(false);
                    pauseMenu.setActive(true);
                    Console.WriteLine("Entered menu.");
                }

            } else if (active.getName() == "Telekinesis-Select") {
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

            } else if (active.getName() == "Telekinesis-Move") {
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
                    selectedObject.setDestination(selectedObject.getLocation());
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

            } else if (active.getName() == "Menu")
            {
                lastState = state;
                state = Mouse.GetState().LeftButton;

                if (lastState == ButtonState.Pressed && state == ButtonState.Released)
                {
                    pauseMenu.reactToMouseClick();
                }


                if (lastKeyState.IsKeyDown(Keys.M) && currentKeyState.IsKeyUp(Keys.M))
                {
                    screenManager.setActiveScreen(1);
                    pauseMenu.setActive(false);
                    level.setActive(true);
                    Console.WriteLine("Exited menu.");
                }
            }
        }
    }
}

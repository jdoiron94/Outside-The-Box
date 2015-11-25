using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace OutsideTheBox {

    /// <summary>
    /// Class which handles all input given to the game
    /// </summary>

    public class InputManager {

        private readonly Game1 game;
        private readonly Player player;
        private readonly PlayerManager playerManager;
        private readonly CollisionManager collisionManager;
        private readonly Target target;
        private readonly SpriteFont font;

        private Level level;
        private MindRead mindRead;
        private GameObject selectedObject;
        private DeathManager deathManager;
        private KeyboardState lastKeyState;
        private KeyboardState currentKeyState;

        private GameState gameState;
        private ButtonState lastState;
        private ButtonState state;

        private readonly int velocity;
        private readonly int width;
        private readonly int height;

        private int ticks;
        private bool stagnant;
        private bool moving;
        private bool powerReveal;
        private bool menuShown;
        private string dropText;

        private const byte WAIT = 0x4;

        public InputManager(Game1 game, Player player, Level level, Target target, PlayerManager playerManager, Screen[] screens, MindRead mindRead) {
            this.game = game;
            this.player = player;
            this.level = level;
            this.target = target;
            this.playerManager = playerManager;
            this.mindRead = mindRead;
            powerReveal = false;
            collisionManager = new CollisionManager(player, level);
            selectedObject = null;
            velocity = player.getVelocity();
            width = game.getWidth();
            height = game.getHeight() - 40;
            lastKeyState = new KeyboardState();
            currentKeyState = new KeyboardState();
            ticks = 0;
            stagnant = false;
            moving = false;
            font = game.getDropFont();
            this.menuShown = false;
            this.gameState = GameState.Normal;
        }

        /// <summary>
        /// Returns the drop text
        /// </summary>
        /// <returns>Returns the drop text</returns>
        public string getDropText() {
            return dropText;
        }

        /// <summary>
        /// Sets the drop text
        /// </summary>
        /// <param name="dropText">The text to set</param>
        public void setDropText(string dropText) {
            this.dropText = dropText;
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

        /// <summary>
        /// Returns an instance of the player manager
        /// </summary>
        /// <returns>Returns an instance of the player manager</returns>
        public PlayerManager getPlayerManager() {
            return playerManager;
        }

        /// <summary>
        /// Returns the collision manager
        /// </summary>
        /// <returns>Returns the collision manager</returns>
        public CollisionManager getCollisionManager() {
            return collisionManager;
        }

        /// <summary>
        /// Returns the power reveal bool
        /// </summary>
        /// <returns>Returns the power reveal bool</returns>
        public bool getPowerReveal() {
            return powerReveal;
        }

        /// <summary>
        /// Sets the power reveal bool
        /// </summary>
        /// <param name="reveal">Sets the power reveal bool</param>
        public void setPowerReveal(bool reveal) {
            powerReveal = reveal;
        }

        /// <summary>
        /// Sets the death manager
        /// </summary>
        /// <param name="deathManager">Sets the death manager</param>
        public void setDeathManager(DeathManager deathManager) {
            this.deathManager = deathManager;
        }

        /// <summary>
        /// Controls updating of the game based on the current screen state and mouse/keyboard input
        /// </summary>
        /// <param name="time">The GameTime to update with respect to</param>
        public void update(GameTime time) {
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            collisionManager.updatePressButtons(player);
            if (currentKeyState.IsKeyDown(Keys.Escape)) {
                game.Exit();
            }
            if (playerManager.getHealth() <= 0) {
                deathManager.resetGame();
            }
            if (collisionManager.playerSpotted(level)) {
                //playerManager.setHealth(0);
            }
            if (lastKeyState.IsKeyDown(Keys.F1) && currentKeyState.IsKeyUp(Keys.F1)) {
                foreach (Level l in game.getLevels()) {
                    l.toggleDebug();
                }
            }
            if (gameState == GameState.Normal) {
                updateNormal(time);
            } else if (gameState == GameState.TelekinesisSelect) {
                updateSelect(time);
            } else if (gameState == GameState.TelekinesisMovement) {
                updateTelekinesisMove(time);
            } else if (gameState == GameState.PauseMenu) {
                if (menuShown) {
                    gameState = GameState.Normal;
                    menuShown = false;
                } else {
                    menuShown = true;
                    level.getScreens()[1].setActive(true);
                }
            }
        }

        private void updateNormal(GameTime time) {
            if (!(collisionManager.getObjectCollision(player, true) is PlayerLimitationField)) {
                playerManager.setManaLimit(true);
                playerManager.setHealthLimit(true);
            }
            if (playerManager.getHealthCooldown() == 35 && playerManager.getHealthLimit() && playerManager.getManaLimit()) {
                playerManager.regenerateHealth();
                playerManager.regenerateMana();
            }
            foreach (ThoughtBubble tb in level.getThoughts()) {
                tb.reveal(mindRead.isActivated());
                tb.updateLocation();
                if (tb.isRevealed() && tb.isKey()) {
                    playerManager.getKeyBox().setUnlocked(true);
                    level.unlockDoors();
                }
            }
            foreach (PressButton pb in level.getPressButtons()) {
                pb.update();
            }
            GameObject gCollision = collisionManager.getObjectCollision(player, true);
            if (gCollision != null && gCollision is Token) {
                Token t = (Token) gCollision;
                t.setCollected(true);
                playerManager.incrementExperience(t.getExp());
                playerManager.levelMana(t.getManaIncrementationValue());
                PauseMenu pause = (PauseMenu) level.getScreens()[1];
                pause.setExperience(pause.getExperience() + t.getExp());
                dropText = "+ " + t.getExp() + " EXP";
                gCollision = null;
            } else if (gCollision != null && gCollision is Key) {
                Key k = (Key) gCollision;
                k.setCollected(true);
                k.setUnlocked(true);
                level.unlockDoors();
            } else if (gCollision != null && gCollision is Door) {
                Door d = (Door) gCollision;
                if (d.isUnlocked()) {
                    level.eliminateCollectibles();
                    int index = (game.getLevelIndex()) + (d.getNext() ? 1 : -1);
                    Song current = level.getSong();
                    level.setActive(false);
                    game.setLevel(index);
                    level = game.getLevel(index);
                    //
                    PauseMenu pause = (PauseMenu) level.getScreens()[1];
                    pause.setLevel(index);
                    if (current != level.getSong()) {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(level.getSong());
                    }
                    //
                    game.setLevel(level);
                    deathManager = new DeathManager(this);
                    setDeathManager(deathManager);
                    collisionManager.getLevel().setActive(false);
                    level.setInputManager(this);
                    collisionManager.setLevel(level);
                    level.setActive(true);
                    if (d.getNext())
                        player.setLocation(level.getPlayerOrigin());
                    else
                        player.setLocation(level.getPlayerReentryPoint());
                    playerManager.getKeyBox().update(this);
                }
            } else if (gCollision != null && gCollision is Pit) {
                Pit p = (Pit) gCollision;
                p.update(this);
                if (p is PlayerLimitationField) {
                    PlayerLimitationField plf = (PlayerLimitationField) p;
                    plf.update(this);
                }
                if (gCollision is LavaPit) {
                    p.playEffect();
                }
            }
            if (lastKeyState.IsKeyDown(Keys.M) && currentKeyState.IsKeyUp(Keys.M)) {
                if (gameState == GameState.Normal) {
                    gameState = GameState.PauseMenu;
                    level.setActive(false);
                } else {
                    gameState = GameState.Normal;
                    level.setActive(true);
                }
            } else if (lastKeyState.IsKeyDown(Keys.E) && currentKeyState.IsKeyUp(Keys.E)) {
                if (mindRead.validate()) {
                    playerManager.depleteMana(mindRead.getManaCost());
                    foreach (ThoughtBubble th in level.getThoughts()) {
                        th.updateThought();
                    }
                }
            }
            if (playerManager.getManaLimit()) {
                mindRead.activate(level);
                SlowTime slowmo = (SlowTime) playerManager.getPowers()[0];
                if (lastKeyState.IsKeyDown(Keys.A) && currentKeyState.IsKeyUp(Keys.A)) {
                    if (slowmo.validate()) {
                        playerManager.depleteMana(slowmo.getManaCost());
                    }
                }
                slowmo.activate(level);
                Dash dash = (Dash) playerManager.getPowers()[1];
                if (lastKeyState.IsKeyDown(Keys.W) && currentKeyState.IsKeyUp(Keys.W)) {
                    if (dash.validate()) {
                        playerManager.depleteMana(dash.getManaCost());
                    }
                }
                dash.activate(level);
                Confuse confuse = (Confuse) playerManager.getPowers()[2];
                if (lastKeyState.IsKeyDown(Keys.S) && currentKeyState.IsKeyUp(Keys.S)) {
                    if (confuse.validate()) {
                        playerManager.depleteMana(confuse.getManaCost());
                    }
                }
                confuse.activate(level);
            }
            if (currentKeyState.IsKeyDown(Keys.Up)) {
                player.setDirection(Direction.North);
                player.updateMovement();
                player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y - velocity));
                if (player.getDestination().Y >= 0 && collisionManager.isValid(player, true)) {
                    player.deriveY(-velocity);
                }
            } else if (lastKeyState.IsKeyDown(Keys.Up) && currentKeyState.IsKeyUp(Keys.Up)) {
                stagnant = true;
            } else if (currentKeyState.IsKeyDown(Keys.Down)) {
                player.setDirection(Direction.South);
                player.updateMovement();
                player.setDestination(new Vector2(player.getLocation().X, player.getLocation().Y + velocity));
                if (player.getDestination().Y <= height - player.getTexture().Height && collisionManager.isValid(player, true)) {
                    player.deriveY(velocity);
                }
            } else if (lastKeyState.IsKeyDown(Keys.Down) && currentKeyState.IsKeyUp(Keys.Down)) {
                stagnant = true;
            } else if (currentKeyState.IsKeyDown(Keys.Left)) {
                player.setDirection(Direction.West);
                player.updateMovement();
                player.setDestination(new Vector2(player.getLocation().X - velocity, player.getLocation().Y));
                if (player.getDestination().X >= 0 && collisionManager.isValid(player, true)) {
                    player.deriveX(-velocity);
                }
            } else if (lastKeyState.IsKeyDown(Keys.Left) && currentKeyState.IsKeyUp(Keys.Left)) {
                stagnant = true;
            } else if (currentKeyState.IsKeyDown(Keys.Right)) {
                player.setDirection(Direction.East);
                player.updateMovement();
                player.setDestination(new Vector2(player.getLocation().X + velocity, player.getLocation().Y));
                if (player.getDestination().X <= width - 64 && collisionManager.isValid(player, true)) {
                    player.deriveX(velocity);
                }
            } else if (lastKeyState.IsKeyDown(Keys.Right) && currentKeyState.IsKeyUp(Keys.Right)) {
                stagnant = true;
            } else {
                player.setDestination(player.getLocation());
            }
            if (currentKeyState.IsKeyDown(Keys.Space) && playerManager.getManaLimit()) {
                double ms = time.TotalGameTime.TotalMilliseconds;
                if ((player.getLastFired() == -1 || ms - player.getLastFired() >= player.getProjectile().getCooldown()) && playerManager.getMana() >= 5) {
                    level.addProjectile(player.createProjectile(ms));
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
            if (lastKeyState.IsKeyDown(Keys.Q) && currentKeyState.IsKeyUp(Keys.Q)) {
                gameState = GameState.TelekinesisSelect;
                level.setMode(1);
                target.setActive(true);
            } else if (currentKeyState.IsKeyDown(Keys.P)) {
                playerManager.damagePlayer(2);
            }
        }

        private void updateSelect(GameTime time) {
            lastState = state;
            state = Mouse.GetState().LeftButton;
            playerManager.setManaDrainRate(5);
            if (lastState == ButtonState.Pressed && state == ButtonState.Released) {
                foreach (GameObject obj in level.getObjectsAndKeys()) {
                    if (obj.isLiftable()) {
                        if (obj.getBounds().Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) {
                            obj.setSelected(true);
                            selectedObject = obj;
                            gameState = GameState.TelekinesisMovement;
                            level.setMode(2);
                        }
                    }
                }
            } else if (lastKeyState.IsKeyDown(Keys.Q) && currentKeyState.IsKeyUp(Keys.Q)) {
                gameState = GameState.Normal;
                level.setMode(0);
            }
        }

        private void updateTelekinesisMove(GameTime time) {
            playerManager.updateManaDrainRate();
            if (currentKeyState.IsKeyDown(Keys.Up)) {
                selectedObject.setDirection(Direction.North);
                selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y - velocity));
                if (selectedObject.getDestination().Y > 0 && collisionManager.isValid(selectedObject)) {
                    selectedObject.deriveY(-velocity);
                    if (playerManager.getManaDrainRate() == 5) {
                        playerManager.depleteMana(2);
                    }
                }
            } else if (currentKeyState.IsKeyDown(Keys.Down)) {
                selectedObject.setDirection(Direction.South);
                selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y + velocity));
                if (selectedObject.getDestination().Y < height - selectedObject.getTexture().Height && collisionManager.isValid(selectedObject)) {
                    selectedObject.deriveY(velocity);
                    if (playerManager.getManaDrainRate() == 5) {
                        playerManager.depleteMana(2);
                    }
                }
            } else if (currentKeyState.IsKeyDown(Keys.Left)) {
                selectedObject.setDirection(Direction.West);
                selectedObject.setDestination(new Vector2(selectedObject.getLocation().X - velocity, selectedObject.getLocation().Y));
                if (selectedObject.getDestination().X > 0 && collisionManager.isValid(selectedObject)) {
                    selectedObject.deriveX(-velocity);
                    if (playerManager.getManaDrainRate() == 5) {
                        playerManager.depleteMana(2);
                    }
                }
            } else if (currentKeyState.IsKeyDown(Keys.Right)) {
                selectedObject.setDirection(Direction.East);
                selectedObject.setDestination(new Vector2(selectedObject.getLocation().X + velocity, selectedObject.getLocation().Y));
                if (selectedObject.getDestination().X < width && collisionManager.isValid(selectedObject)) {
                    selectedObject.deriveX(velocity);
                    if (playerManager.getManaDrainRate() == 5) {
                        playerManager.depleteMana(2);
                    }
                }
            } else {
                selectedObject.setDestination(selectedObject.getLocation());
            }
            if ((moving || (lastKeyState.IsKeyDown(Keys.Space) && currentKeyState.IsKeyDown(Keys.Space))) && playerManager.getMana() > 0) {
                moving = true;
                if (selectedObject.getDirection() == Direction.North) {
                    selectedObject.setDestination(new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y - velocity));
                    if (selectedObject.getDestination().Y > 0 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveY(-velocity);
                    } else {
                        moving = false;
                    }
                } else if (selectedObject.getDirection() == Direction.South) {
                    if (selectedObject.getDestination().Y < height - selectedObject.getTexture().Height && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveY(velocity);
                    } else {
                        moving = false;
                    }
                } else if (selectedObject.getDirection() == Direction.West) {
                    if (selectedObject.getDestination().X > 0 && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveX(-velocity);
                    } else {
                        moving = false;
                    }
                } else {
                    if (selectedObject.getDestination().X < width && collisionManager.isValid(selectedObject)) {
                        selectedObject.deriveX(velocity);
                    } else {
                        moving = false;
                    }
                }
                if (playerManager.getManaDrainRate() == 5) {
                    playerManager.depleteMana(1);
                }
                if (!moving) {
                    gameState = GameState.Normal;
                    selectedObject.setSelected(false);
                    selectedObject = null;
                    level.setMode(0);
                }
            } else if ((lastKeyState.IsKeyDown(Keys.Q) && currentKeyState.IsKeyUp(Keys.Q)) || playerManager.getMana() == 0) {
                gameState = GameState.Normal;
                selectedObject.setSelected(false);
                selectedObject = null;
                level.setMode(0);
            }
        }
    }
}

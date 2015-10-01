using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;

namespace KineticCamp {

    public class InputManager {

        /*
         * Class which handles all input given to the game. To be handled according to the ScreenManager's current screen state,
         * in the future.
         */

        private Game1 game;
        private Entity player;
        private Level level;
        private CollisionManager collisionManager;

        private ButtonState lastState;
        private ButtonState state;

        private int stepSize;
        private int midX;
        private int midY;

        public InputManager(Game1 game, Entity player, Level level) {
            this.game = game;
            this.player = player;
            this.level = level;
            collisionManager = new CollisionManager(player, level);
            stepSize = game.getStepSize();
            midX = game.getMidX();
            midY = game.getMidY();
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
        public Entity getPlayer() {
            return player;
        }

        /*
         * Returns the level instance
         */
        public Level getLevel() {
            return level;
        }

        /*
         * Handles all user input to the game, depending on the keyboard/mouse states and the screen's state.
         */
        public void update(GameTime time) {
            KeyboardState kbs = Keyboard.GetState();
            Vector2 destination;
            if (kbs.IsKeyDown(Keys.Escape)) {
                game.Exit();
            } else if (kbs.IsKeyDown(Keys.W)) {
                Console.WriteLine("player: " + player.getLocation().ToString());
                player.setDirection(Direction.NORTH);
                destination = new Vector2(player.getLocation().X, player.getLocation().Y + stepSize);
                if (player.getLocation().Y + stepSize < midY/* && collisionManager.isValid(destination)*/) {
                    level.deriveY(stepSize);
                } else {
                    Console.WriteLine("u wanna go north? nty");
                }
            } else if (kbs.IsKeyDown(Keys.S)) {
                Console.WriteLine("player: " + player.getLocation().ToString());
                player.setDirection(Direction.SOUTH);
                destination = new Vector2(player.getLocation().X, player.getLocation().Y - stepSize);
                if (player.getLocation().Y - stepSize > -(level.getMap().Height - midY - player.getTexture().Height)/* && collisionManager.isValid(destination)*/) {
                    level.deriveY(-stepSize);
                } else {
                    Console.WriteLine("u wanna go south? nty");
                }
            } else if (kbs.IsKeyDown(Keys.A)) {
                Console.WriteLine("player: " + player.getLocation().ToString());
                player.setDirection(Direction.WEST);
                destination = new Vector2(player.getLocation().X + stepSize, player.getLocation().Y);
                if (player.getLocation().X + stepSize < midX/* && collisionManager.isValid(destination)*/) {
                    level.deriveX(stepSize);
                } else {
                    Console.WriteLine("u wanna go west? nty");
                }
            } else if (kbs.IsKeyDown(Keys.D)) {
                Console.WriteLine("player: " + player.getLocation().ToString());
                player.setDirection(Direction.EAST);
                destination = new Vector2(player.getLocation().X - stepSize, player.getLocation().Y);
                Console.WriteLine("dest: " + destination.ToString());
                if (player.getLocation().X - stepSize > -(level.getMap().Width - midX - player.getTexture().Width)/* && collisionManager.isValid(destination)*/) {
                    level.deriveX(-stepSize);
                } else {
                    Console.WriteLine("u wanna go east? nty");
                }
            }
            if (kbs.IsKeyDown(Keys.Space)) {
                double totalMilliseconds = time.TotalGameTime.TotalMilliseconds;
                if (player.getLastFired() == -1 || totalMilliseconds - player.getLastFired() >= player.getProjectile().getCooldown()) {
                    level.addProjectile(player.createProjectile(totalMilliseconds));
                }
                /*foreach (Entity e in level.getNpcs()) {
                    if (e != null) {
                        npc.deriveHealth(-50);
                        // handle projectile interaction with npcs
                        // if hit, derive npc health by -1 * skilltree power
                        Console.WriteLine("Health: " + npc.getHealth());
                    }
                }*/
            }
            lastState = state;
            state = Mouse.GetState().LeftButton;
            if (lastState.Equals(ButtonState.Pressed) && state.Equals(ButtonState.Released)) {
                foreach (GameObject obj in level.getObjects()) {
                    if (obj != null) {
                        Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                        if (p != null && obj.getBounds().Contains(p)) { // and is object is throwable. make instance variable in GameObject for bool throwable and a getter for it.
                            obj.setLocation(game.getMouse().X, game.getMouse().Y); // will need to play around with mouse states and what current one is/last one is for accurately
                            Console.WriteLine("obj contains mouse!");
                        }
                    }
                }
                Console.WriteLine("Mouse pressed");
            }
        }
    }
}

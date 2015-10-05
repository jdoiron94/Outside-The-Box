using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace KineticCamp {

    public class InputManager {

        /*
         * Class which handles all input given to the game. To be handled according to the ScreenManager's current screen state,
         * in the future.
         */

        private Game1 game;
        private Entity player;
        private GameObject selectedObject;
        private Level level;
        private CollisionManager collisionManager;
        private ScreenManager screenManager;

        private ButtonState lastState;
        private ButtonState state;

        private int stepSize;
        private int midX;
        private int midY;

        public InputManager(Game1 game, Entity player, Level level, List<Screen> screens) {
            this.game = game;
            this.player = player;
            this.selectedObject = null;
            this.level = level;
            collisionManager = new CollisionManager(player, level);
            screenManager = new ScreenManager(screens[1], screens); 
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

            /* Normal gameplay (player has control of character's movement*/
            if (screenManager.getActiveScreen().getName() == "Normal")
            {
                if (kbs.IsKeyDown(Keys.Escape))
                {
                    game.Exit();
                }
                else if (kbs.IsKeyDown(Keys.W))
                {
                    Console.WriteLine("player: " + player.getLocation().ToString());
                    player.setDirection(Direction.NORTH);
                    destination = new Vector2(player.getLocation().X, player.getLocation().Y - stepSize);
                    if (player.getLocation().Y + stepSize > 0 && collisionManager.isValid(destination, player))
                    {
                        player.deriveY(-stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go north? nty");
                    }
                }
                else if (kbs.IsKeyDown(Keys.S))
                {
                    Console.WriteLine("player: " + player.getLocation().ToString());
                    player.setDirection(Direction.SOUTH);
                    destination = new Vector2(player.getLocation().X, player.getLocation().Y + stepSize);
                    if (player.getLocation().Y + stepSize < midY * 2 && collisionManager.isValid(destination, player))
                    {
                        player.deriveY(stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go south? nty");
                    }
                }
                else if (kbs.IsKeyDown(Keys.A))
                {
                    Console.WriteLine("player: " + player.getLocation().ToString());
                    player.setDirection(Direction.WEST);
                    destination = new Vector2(player.getLocation().X - stepSize, player.getLocation().Y);
                    if (player.getLocation().X + stepSize > 0 && collisionManager.isValid(destination, player))
                    {
                        player.deriveX(-stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go west? nty");
                    }
                }
                else if (kbs.IsKeyDown(Keys.D))
                {
                    Console.WriteLine("player: " + player.getLocation().ToString());
                    player.setDirection(Direction.EAST);
                    destination = new Vector2(player.getLocation().X + stepSize, player.getLocation().Y);
                    if (player.getLocation().X + stepSize < midX * 2 && collisionManager.isValid(destination, player))
                    {
                        player.deriveX(stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go east? nty");
                    }
                }
                if (kbs.IsKeyDown(Keys.Space))
                {
                    double totalMilliseconds = time.TotalGameTime.TotalMilliseconds;
                    if (player.getLastFired() == -1 || totalMilliseconds - player.getLastFired() >= player.getProjectile().getCooldown())
                    {
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
                else if (kbs.IsKeyDown(Keys.X))
                {
                    //switch to telekinesis-select mode (player clicks a liftable object to select it)
                    screenManager.setActiveScreen(2);
                    Console.WriteLine("Entered telekinesis mode!");
                }
            }

            /* Just entered telekinesis mode (player uses mouse to select a liftable object)*/
            else if (screenManager.getActiveScreen().getName() == "Telekinesis-Select")
            {
                lastState = state;
                state = Mouse.GetState().LeftButton;
                if (lastState.Equals(ButtonState.Pressed) && state.Equals(ButtonState.Released))
                {
                    foreach (GameObject obj in level.getObjects())
                    {
                        if (obj != null && obj.isLiftable())
                        {
                            Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                            if (p != null && obj.getBounds().Contains(p))
                            { 
                                //select object
                                obj.setSelected(true); 
                                selectedObject = obj;
                                //switch screen state to telekinesis-move (control over selected object)
                                screenManager.setActiveScreen(3); 
                                Console.WriteLine("obj contains mouse!"); 
                            }
                        }
                    }
                    Console.WriteLine("Mouse pressed");
                }
                else if (kbs.IsKeyDown(Keys.X))
                {
                    //switch to telekinesis-select mode (player clicks a liftable object to select it)
                    screenManager.setActiveScreen(1);
                    Console.WriteLine("Entered telekinesis mode!");
                }
            }

            /* Telekinetic lifting mode (player controls the selected object's movement)*/
            else if (screenManager.getActiveScreen().getName() == "Telekinesis-Move")
            {
                if (kbs.IsKeyDown(Keys.Escape))
                {
                    game.Exit();
                }
                else if (kbs.IsKeyDown(Keys.W))
                {
                    Console.WriteLine("selectedObject: " + selectedObject.getLocation().ToString());
                    selectedObject.setDirection(Direction.NORTH);
                    //destination = new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y + stepSize);
                    if (selectedObject.getLocation().Y + stepSize > 0/* && collisionManager.isValid(destination)*/)
                    {
                        selectedObject.deriveY(-stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go north? nty");
                    }
                }
                else if (kbs.IsKeyDown(Keys.S))
                {
                    Console.WriteLine("selectedObject: " + selectedObject.getLocation().ToString());
                    selectedObject.setDirection(Direction.SOUTH);
                    //destination = new Vector2(selectedObject.getLocation().X, selectedObject.getLocation().Y - stepSize);
                    if (selectedObject.getLocation().Y - stepSize < midY * 2/* && collisionManager.isValid(destination)*/)
                    {
                        selectedObject.deriveY(stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go south? nty");
                    }
                }
                else if (kbs.IsKeyDown(Keys.A))
                {
                    Console.WriteLine("selectedObject: " + selectedObject.getLocation().ToString());
                    selectedObject.setDirection(Direction.WEST);
                    //destination = new Vector2(selectedObject.getLocation().X + stepSize, selectedObject.getLocation().Y);
                    if (selectedObject.getLocation().X + stepSize > 0/* && collisionManager.isValid(destination)*/)
                    {
                        selectedObject.deriveX(-stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go west? nty");
                    }
                }
                else if (kbs.IsKeyDown(Keys.D))
                {
                    Console.WriteLine("selectedObject: " + selectedObject.getLocation().ToString());
                    selectedObject.setDirection(Direction.EAST);
                    //destination = new Vector2(selectedObject.getLocation().X - stepSize, selectedObject.getLocation().Y);
                    //Console.WriteLine("dest: " + destination.ToString());
                    if (selectedObject.getLocation().X - stepSize < midX * 2/* && collisionManager.isValid(destination)*/)
                    {
                        selectedObject.deriveX(stepSize);
                    }
                    else
                    {
                        Console.WriteLine("u wanna go east? nty");
                    }
                }
                if (kbs.IsKeyDown(Keys.Space))
                {
                   
                }
                else if (kbs.IsKeyDown(Keys.X))
                {
                    //deselect object
                    selectedObject.setSelected(false); 
                    selectedObject = null; 
                    //switch back to normal screen state (control over character)
                    screenManager.setActiveScreen(1);
                    Console.WriteLine("Exited telekinesis mode.");
                }
            }

        }
    }
}

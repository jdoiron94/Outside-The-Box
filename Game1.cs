using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace KineticCamp {

    /* CHANGELOG
     * Version 0.0.0.3
     *
     * 0.0.0.1:
     *      Added a few sprites to test functionality, centered player on screen, locked player to the map's bounds
     * 0.0.0.2:
     *      Created data structures for relevant information, handling entities on/off screen
     * 0.0.0.3:
     *      Direction handling, basic projectile support, changed rendering order
     * 0.0.0.4:
     *      Projectile rotation, npc hotfix
     */

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        
        // TODO: content handler, input handler, collision handler

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playerTexture;
        Entity player;
        Entity npc;
        Level level;

        int midX;
        int midY;

        private const int step = 4;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        //
        // TODO: set viewport bounds to be a factor of sprite dimensions
        protected override void Initialize() {
            //graphics.PreferredBackBufferWidth = x;
            //graphics.PreferredBackBufferHeight = y;
            //graphics.ApplyChanges();
            Window.Title = "Kinetic Camp";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("player");
            midX = (graphics.PreferredBackBufferWidth - playerTexture.Width) / 2;
            midY = (graphics.PreferredBackBufferHeight - playerTexture.Height) / 2;
            player = new Entity(playerTexture, new Projectile(Content.Load<Texture2D>("bullet"), new Vector2(midX, midY), 2, 250, 0.5f), new Vector2(midX, midY), Direction.EAST, GraphicsDevice.Viewport.Bounds, 50, 5);
            npc = new Entity(Content.Load<Texture2D>("npc"), null, new Vector2(midX + 150, midY + 150), Direction.EAST, GraphicsDevice.Viewport.Bounds, 50, 5);
            level = new Level(player, Content.Load<Texture2D>("map"), new Entity[] { npc });
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.Escape)) {
                Exit();
            } else if (kbs.IsKeyDown(Keys.W)) {
                player.setDirection(Direction.NORTH);
                if (player.getLocation().Y + step < midY) {
                    level.deriveY(step);
                }
            } else if (kbs.IsKeyDown(Keys.S)) {
                player.setDirection(Direction.SOUTH);
                if (player.getLocation().Y - step > -(level.getMap().Height - midY - player.getTexture().Height)) {
                    level.deriveY(-step);
                }
            } else if (kbs.IsKeyDown(Keys.A)) {
                player.setDirection(Direction.WEST);
                if (player.getLocation().X + step < midX) {
                    level.deriveX(step);
                }
            } else if (kbs.IsKeyDown(Keys.D)) {
                player.setDirection(Direction.EAST);
                if (player.getLocation().X - step > -((level.getMap().Width - midX - player.getTexture().Width))) {
                    level.deriveX(-step);
                }
            }
            if (kbs.IsKeyDown(Keys.Space)) {
                double totalMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
                if (player.getLastFired() == -1 || totalMilliseconds - player.getLastFired() >= player.getProjectile().getCooldown()) {
                    level.addProjectile(player.createProjectile(totalMilliseconds));
                }
                foreach (Entity e in level.getNpcs()) {
                    if (e != null) {
                        npc.deriveHealth(-50);
                        // handle projectile interaction with npcs
                        // if hit, derive npc health by -1 * skilltree power
                        Console.WriteLine("Health: " + npc.getHealth());
                    }
                }
            }
            level.updateProjectiles();
            level.updateNpcs();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            level.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

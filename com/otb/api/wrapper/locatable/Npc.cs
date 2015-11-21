using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace OutsideTheBox {

    /// <summary>
    /// Class which represents a non-playing character
    /// </summary>

    public class Npc : Entity {

        // TODO: Have it react with reactTime

        private readonly Game1 game;
        private readonly NpcDefinition def;
        private readonly Texture2D sight;
        private readonly Vector2 origin;
        private Vector2 losBegin;

        private readonly int[] offsets;
        private readonly int radius;
        private readonly int reactTime;
        private readonly bool wander;

        private int reactTicks;
        private bool hit;

        private float angle = 0;

        private AIPath path;
        private Rectangle lineOfSight;

        public Npc(Game1 game, Texture2D texture, Texture2D sight, Vector2 location, Direction direction, NpcDefinition def, int[] offsets, int maxHealth, int velocity, int radius, int reactTime, bool wander) :
            base(texture, location, direction, maxHealth, velocity) {
            this.game = game;
            this.def = def;
            this.offsets = offsets;
            this.radius = radius;
            this.reactTime = reactTime;
            this.wander = wander;
            reactTicks = 0;
            hit = false;
            this.sight = sight;
            origin = new Vector2(texture.Width / 2F, texture.Height / 2F);
        }

        public Npc(Game1 game, Texture2D texture, Texture2D sight, Vector2 location, Direction direction, NpcDefinition def, int[] offsets, int radius, byte reactTime, bool wander) :
            this(game, texture, sight, location, direction, def, offsets, 100, 3, radius, 10, wander) {
        }

        public Npc(Game1 game, Texture2D texture, Texture2D sight, Vector2 location, Direction direction, NpcDefinition def, int radius, byte reactTime) :
            this(game, texture, sight, location, direction, def, new int[0], radius, 10, false) {
        }

        /// <summary>
        /// Returns whether or not the npc has been hit
        /// </summary>
        /// <returns>Returns true if the npc has been hit; otherwise, false</returns>
        public bool wasHit() {
            return hit;
        }

        /// <summary>
        /// Sets the npc's hit status
        /// </summary>
        /// <param name="hit">The boolean to set</param>
        public void setHit(bool hit) {
            this.hit = hit;
        }

        /// <summary>
        /// Returns an instance of the game
        /// </summary>
        /// <returns></returns>
        public Game getGame() {
            return game;
        }

        /// <summary>
        /// Returns an instance of the npc's definition
        /// </summary>
        /// <returns>Returns the npc's definition</returns>
        public NpcDefinition getDefinition() {
            return def;
        }

        /// <summary>
        /// Returns the npc's wanderable offsets
        /// </summary>
        /// <returns>Returns an integer array of the npc's wanderable offsets from its origin</returns>
        public int[] getOffsets() {
            return offsets;
        }

        /// <summary>
        /// Returns the acting radius of the npc
        /// </summary>
        /// <returns>Returns the radius by which the npc responds to player presence</returns>
        public int getRadius() {
            return radius;
        }

        /// <summary>
        /// Returns the npc's reaction time
        /// </summary>
        /// <returns>Returns the npc's reaction time</returns>
        public int getReactTime() {
            return reactTime;
        }

        /// <summary>
        /// Returns the npc's static ai pathing
        /// </summary>
        /// <returns>Returns the npc's static ai pathing</returns>
        public AIPath getPath() {
            return path;
        }

        /// <summary>
        /// Returns whether or not the npc is allowed to wander around
        /// </summary>
        /// <returns>Returns true if the npc is allowed to wander; otherwise, false</returns>
        public bool isWanderer() {
            return wander;
        }

        /// <summary>
        /// Sets the npc's static ai path
        /// </summary>
        /// <param name="path">The path to be set</param>
        public void setPath(AIPath path) {
            this.path = path;
        }

        /// <summary>
        /// Sets the npc's line of sight bounds
        /// </summary>
        /// <param name="lineOfSight">The bounds to set for los</param>
        public void setLineOfSight(Rectangle lineOfSight) {
            this.lineOfSight = lineOfSight;
        }

        /// <summary>
        /// Returns the npc's line of sight
        /// </summary>
        /// <returns>Returns the npc's line of sight bounds</returns>
        public Rectangle getLineOfSight() {
            return lineOfSight;
        }

        /// <summary>
        /// Returns whether or not the npc is within reaction distance of the player
        /// </summary>
        /// <param name="player">The player to check against</param>
        /// <returns>Returns true if the player is within reaction distance; otherwise, false</returns>
        public bool isWithin(Player player) {
            return Math.Abs(player.getLocation().Y - location.Y) <= radius && Math.Abs(location.X - player.getLocation().X) <= radius;
        }

        /// <summary>
        /// Handles the npc's reaction to the player
        /// </summary>
        /// <param name="time">The game time to respect</param>
        /// <param name="player">The player to react to</param>
        public void react(GameTime time, Player player, bool face) {
            if (projectile == null /*|| reactTicks < reactTime */|| (!isFacing(player, 1.75F) && !face)) {
                reactTicks++;
                return;
            }
            if (face) {
                setFacing(player);
            }
            if (getDistance(player) <= 100) {
            foreach (Npc n in game.getLevel().getNpcs()) {
                if (n != null && n != this) {
                    if (isFacing(n, 1.75F) && (getHDistance(n) <= n.getTexture().Width * 2 || getVDistance(n) <= n.getTexture().Height * 2)) {
                        return;
                    }
                }
            }
            double ms = time.TotalGameTime.TotalMilliseconds;
            if (lastFired == -1 || ms - lastFired >= projectile.getCooldown()) {
                game.addProjectile(createProjectile(ms));
                reactTicks = 0;
            }
            }
        }

        /// <summary>
        /// Updates the npc's line of sight bounds depending on its direction
        /// </summary>
        public void updateLineOfSight() {
            if (direction == Direction.North) {
                lineOfSight = new Rectangle((int) location.X, (int) location.Y + 10 - texture.Height * 3, texture.Width, (texture.Height * 3));
                angle = MathHelper.ToRadians(0F);
                losBegin = new Vector2(location.X, location.Y - lineOfSight.Height + 10F);
            } else if (direction == Direction.South) {
                lineOfSight = new Rectangle((int) location.X, (int) location.Y + texture.Height, texture.Width, texture.Height * 3);
                angle = MathHelper.ToRadians(180F);
                losBegin = new Vector2(location.X, location.Y + (texture.Height * 3F));
            } else if (direction == Direction.West) {
                lineOfSight = new Rectangle((int) location.X + 15 - texture.Width * 3, (int) location.Y, texture.Width * 3, texture.Height);
                angle = MathHelper.ToRadians(-90F);
                losBegin = new Vector2(location.X - lineOfSight.Width + 15F, location.Y);
            } else {
                lineOfSight = new Rectangle((int) location.X - 15 + texture.Width, (int) location.Y, texture.Width * 3, texture.Height);
                angle = MathHelper.ToRadians(90F);
                losBegin = new Vector2(location.X + (texture.Width * 3F) - 15F, location.Y);
            }
        }

        /// <summary>
        /// Updates the npc's movements and reactions
        /// </summary>
        /// <param name="time">The game time to respect</param>
        public void update(GameTime time) {
            addCombatTicks();
            if (path != null) {
                path.update();
                updateLineOfSight();
                react(time, game.getPlayer(), false);
                return;
            }
            react(time, game.getPlayer(), true);
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(texture, location, Color.White);
            batch.Draw(sight, Vector2.Add(losBegin, origin), null, Color.White, angle, origin, 1F, SpriteEffects.None, 0F);
        }
    }
}

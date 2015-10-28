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

        private readonly int[] offsets;
        private readonly int radius;
        private readonly byte reactTime;
        private readonly bool wander;

        private int ticks;

        private AIPath path;
        private Rectangle lineOfSight;

        public Npc(Game1 game, Texture2D texture, Vector2 location, Direction direction, NpcDefinition def, int[] offsets, int maxHealth, int velocity, int radius, byte reactTime, bool wander) :
            base(texture, location, direction, maxHealth, velocity) {
            this.game = game;
            this.def = def;
            this.offsets = offsets;
            this.radius = radius;
            this.reactTime = reactTime;
            this.wander = wander;
            ticks = 0;
        }

        public Npc(Game1 game, Texture2D texture, Vector2 location, Direction direction, NpcDefinition def, int[] offsets, int radius, byte reactTime, bool wander) :
            this(game, texture, location, direction, def, offsets, 100, 3, radius, reactTime, wander) {
        }

        public Npc(Game1 game, Texture2D texture, Vector2 location, Direction direction, NpcDefinition def, int radius, byte reactTime) :
            this(game, texture, location, direction, def, new int[0], radius, reactTime, false) {
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
        public byte getReactTime() {
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

        public void dodge() {
        }

        /// <summary>
        /// Handles the npc's reaction to the player
        /// </summary>
        /// <param name="time">The game time to respect</param>
        /// <param name="player">The player to react to</param>
        public void react(GameTime time, Player player) {
            setFacing(player);
            if (getDistance(player) <= 100) {
                foreach (Npc n in game.getLevel().getNpcs()) {
                    if (n != null && n != this && n.isOnScreen(game)) {
                        if (isFacing(n, 1.75F) && (getHDistance(n) <= n.getTexture().Width * 2 || getVDistance(n) <= n.getTexture().Height * 2)) {
                            return;
                        }
                    }
                }
                double totalMilliseconds = time.TotalGameTime.TotalMilliseconds;
                if (getLastFired() == -1 || totalMilliseconds - getLastFired() >= getProjectile().getCooldown()) {
                    game.addProjectile(createProjectile(totalMilliseconds));
                }
            }
        }

        /// <summary>
        /// Updates the npc's line of sight bounds depending on its direction
        /// </summary>
        public void updateLineOfSight() {
            switch (getDirection()) {
                case Direction.North:
                    lineOfSight = new Rectangle((int) location.X, (int) location.Y - texture.Height * 3, texture.Width, texture.Height * 3);
                    break;
                case Direction.South:
                    lineOfSight = new Rectangle((int) location.X, (int) location.Y + texture.Height, texture.Width, texture.Height * 3);
                    break;
                case Direction.West:
                    lineOfSight = new Rectangle((int) location.X - texture.Width * 3, (int) location.Y, texture.Width * 3, texture.Height);
                    break;
                case Direction.East:
                    lineOfSight = new Rectangle((int) location.X + texture.Width, (int) location.Y, texture.Width * 3, texture.Height);
                    break;
                case Direction.None:
                    lineOfSight = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
                    break;
                default:
                    lineOfSight = new Rectangle((int) location.X, (int) location.Y, texture.Width, texture.Height);
                    break;
            }
        }

        /// <summary>
        /// Updates the npc's movements and reactions
        /// </summary>
        /// <param name="time">The game time to respect</param>
        public void update(GameTime time) {
            if (path != null) {
                path.update();
                updateLineOfSight();
            } else if (isWithin(game.getPlayer())) {
                react(time, game.getPlayer());
            }
        }
    }
}
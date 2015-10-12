using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace KineticCamp {

    public class Npc : Entity {

        /*
         * Class which represents a non-playing character
         */

        // TODO: Have it react with reactTime, minimize chances of friendly fire, minimize pre-fire

        private readonly Game1 game;
        private readonly NpcDefinition def;

        private readonly int radius;
        private readonly byte reactTime;

        private AIPath path;

        public Npc(Game1 game, Texture2D texture, Vector2 location, Direction direction, NpcDefinition def, int maxHealth, int velocity, int radius, byte reactTime) :
            base(texture, location, direction, maxHealth, velocity) {
            this.game = game;
            this.def = def;
            this.radius = radius;
            this.reactTime = reactTime;
        }

        /*
         * Returns the game instance
         */
        public Game getGame() {
            return game;
        }

        /*
         * Returns the npc's definition
         */
        public NpcDefinition getDefinition() {
            return def;
        }

        /*
         * Returns the npc's react radius
         */
        public int getRadius() {
            return radius;
        }

        /*
         * Returns the npc's reaction time, in terms of number of frames skipped between interaction
         */
        public byte getReactTime() {
            return reactTime;
        }

        /*
         * Returns the entity's static AI path
         */
        public AIPath getPath() {
            return path;
        }

        /*
         * Sets the npc's static pathing
         */
        public void setPath(AIPath path) {
            this.path = path;
        }

        /*
         * Returns true if the player is within react distance of the npc; otherwise, false
         */
        public bool isWithin(Player player) {
            return Math.Abs(player.getLocation().Y - location.Y) <= radius && Math.Abs(location.X - player.getLocation().X) <= radius;
        }

        /*
         * Handles the npc's reaction to the player's presence
         */
        public void react(GameTime time, Player player) {
            if (isWithin(player)) {
                if (Math.Abs(player.getLocation().X - location.X) <= texture.Width) {
                    setDirection(player.getLocation().Y >= location.Y ? Direction.SOUTH : Direction.NORTH);
                } else if (player.getLocation().X < location.X) {
                    setDirection(Direction.WEST);
                } else {
                    setDirection(Direction.EAST);
                }
            }
            double totalMilliseconds = time.TotalGameTime.TotalMilliseconds;
            if (getLastFired() == -1 || totalMilliseconds - getLastFired() >= getProjectile().getCooldown()) {
                game.addProjectile(createProjectile(totalMilliseconds));
            }
        }

        public virtual void update(GameTime time) {
        }
    }
}

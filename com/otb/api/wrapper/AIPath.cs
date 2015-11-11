using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    /// <summary>
    /// Class which contains information to create a path for an artificially "intelligent"
    /// npc to follow a static path through the game.
    /// </summary>

    public class AIPath {

        // TODO: npc animations

        private int state;
        private int ticks;
        private int wait;
        private bool stagnant;

        private readonly int[] path;
        private readonly int[] delays;

        private const byte SKIPPED_FRAMES = 0x4;

        private readonly Npc npc;
        private readonly Player player;
        private readonly CollisionManager collisionManager;
        private readonly Direction[] directions;

        public AIPath(Npc npc, Game1 game, int[] path, int[] delays, Direction[] directions) {
            this.npc = npc;
            player = game.getPlayer();
            collisionManager = game.getInputManager().getCollisionManager();
            this.path = path;
            this.delays = delays;
            this.directions = directions;
            state = 0;
            ticks = 0;
            wait = 0;
            stagnant = false;
        }

        /// <summary>
        /// Returns an instance of the npc
        /// </summary>
        /// <returns>Returns an instance of the npc</returns>
        public Npc getNpc() {
            return npc;
        }

        /// <summary>
        /// Returns the path followed by the npc
        /// </summary>
        /// <returns>Returns the integer array path followed by the npc</returns>
        public int[] getPath() {
            return path;
        }

        /// <summary>
        /// Returns the delays followed by the npc
        /// </summary>
        /// <returns>Returns the integer array of delays between directions, followed by the npc</returns>
        public int[] getDelays() {
            return delays;
        }

        /// <summary>
        /// Returns the directions followed by the npc
        /// </summary>
        /// <returns>Returns the direction array followed by the npc</returns>
        public Direction[] getDirections() {
            return directions;
        }

        /// <summary>
        /// Returns the number of frames skipped between movements
        /// </summary>
        /// <returns>Returns 4</returns>
        public byte getSkippedFrames() {
            return SKIPPED_FRAMES;
        }

        /// <summary>
        /// Updates the npc's direction and movement, if it has been sufficient time between interactions
        /// </summary>
        public void update() {
            npc.setDirection(directions[state]);
            if (stagnant && wait < delays[state]) {
                wait++;
                return;
            } else if (stagnant) {
                stagnant = false;
                wait = 0;
                state = (state + 1) % path.Length;
                npc.setDirection(directions[state]);
            }
            switch (npc.getDirection()) {
                case Direction.North:
                    if (npc.getLocation().Y > path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.setDestination(new Vector2(npc.getLocation().X, npc.getLocation().Y - npc.getVelocity()));
                            if (collisionManager.isValid(npc)) {
                                npc.deriveY(-npc.getVelocity());
                            }
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    if (npc.getLocation().Y <= path[state]) {
                        stagnant = true;
                    }
                    break;
                case Direction.South:
                    if (npc.getLocation().Y < path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.setDestination(new Vector2(npc.getLocation().X, npc.getLocation().Y + npc.getVelocity()));
                            if (collisionManager.isValid(npc)) {
                                npc.deriveY(npc.getVelocity());
                            }
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    if (npc.getLocation().Y >= path[state]) {
                        stagnant = true;
                    }
                    break;
                case Direction.West:
                    if (npc.getLocation().X > path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.setDestination(new Vector2(npc.getLocation().X - npc.getVelocity(), npc.getLocation().Y));
                            if (collisionManager.isValid(npc)) {
                                npc.deriveX(-npc.getVelocity());
                            }
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    if (npc.getLocation().X <= path[state]) {
                        stagnant = true;
                    }
                    break;
                case Direction.East:
                    if (npc.getLocation().X < path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.setDestination(new Vector2(npc.getLocation().X + npc.getVelocity(), npc.getLocation().Y));
                            if (collisionManager.isValid(npc)) {
                                npc.deriveX(npc.getVelocity());
                            }
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    if (npc.getLocation().X >= path[state]) {
                        stagnant = true;
                    }
                    break;
            }
        }
    }
}

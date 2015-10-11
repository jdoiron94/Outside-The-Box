namespace KineticCamp {

    public class AIPath {

        /*
         * Class which contains information to create a path for an artificially intelligent
         * npc to follow a static path through the game.
         */

        // TODO: Support delays between moving directions and animations

        private int state;
        private byte ticks;

        private readonly int[] path;
        private readonly int[] delays;

        private const byte SKIPPED_FRAMES = 0x4;

        private readonly Npc npc;
        private readonly Direction[] directions;
        
        public AIPath(Npc npc, int[] path, int[] delays, Direction[] directions) {
            this.npc = npc;
            this.path = path;
            this.delays = delays;
            this.directions = directions;
            state = 0;
            ticks = 0;
        }

        /*
         * Returns the npc instance
         */
        public Npc getNpc() {
            return npc;
        }

        /*
         * Returns the given path
         */
        public int[] getPath() {
            return path;
        }

        /*
         * Returns the delays between finishing the path in each direction
         */
        public int[] getDelays() {
            return delays;
        }

        /*
         * Returns the directions used in the path
         */
        public Direction[] getDirections() {
            return directions;
        }

        /*
         * Returns the number of frames skipped between walking
         */
        public byte getSkippedFrames() {
            return SKIPPED_FRAMES;
        }

        /*
         * Controls the walking of the path
         */
        public void update() {
            npc.setDirection(directions[state]);
            switch (npc.getDirection()) {
                case Direction.NORTH:
                    if (npc.getLocation().Y > path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.deriveY(-npc.getVelocity());
                            ticks = 0;
                        } else {
                            ticks++;
                        }

                    }
                    state = npc.getLocation().Y <= path[state] ? (state + 1) % path.Length : state;
                    break;
                case Direction.SOUTH:
                    if (npc.getLocation().Y < path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.deriveY(npc.getVelocity());
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    state = npc.getLocation().Y >= path[state] ? (state + 1) % path.Length : state;
                    break;
                case Direction.WEST:
                    if (npc.getLocation().X > path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.deriveX(-npc.getVelocity());
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    state = npc.getLocation().X <= path[state] ? (state + 1) % path.Length : state;
                    break;
                case Direction.EAST:
                    if (npc.getLocation().X < path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.deriveX(npc.getVelocity());
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    state = npc.getLocation().X >= path[state] ? (state + 1) % path.Length : state;
                    break;
            }
        }
    }
}

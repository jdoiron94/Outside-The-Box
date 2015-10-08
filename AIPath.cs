namespace KineticCamp {

    public class AIPath {

        /*
         * Class which contains information to create a path for an artificially intelligent
         * entity to move through the game.
         */

        // TODO: Support delays between moving directions and animations

        private int state;
        private byte ticks;
        private int[] path;
        private int[] delays;

        private const byte SKIPPED_FRAMES = 0x4;

        private Entity entity;
        private Direction[] directions;
        
        public AIPath(Entity entity, int[] path, int[] delays, Direction[] directions) {
            this.entity = entity;
            this.path = path;
            this.delays = delays;
            this.directions = directions;
            state = 0;
            ticks = 0;
        }

        /*
         * Returns the entity instance
         */
        public Entity getEntity() {
            return entity;
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
            entity.setDirection(directions[state]);
            switch (entity.getDirection()) {
                case Direction.NORTH:
                        if (entity.getLocation().Y > path[state]) {
                            if (ticks >= SKIPPED_FRAMES) {
                                entity.deriveY(-entity.getStepSize());
                                ticks = 0;
                            } else {
                                ticks++;
                            }

                        }
                        state = entity.getLocation().Y <= path[state] ? (state + 1) % path.Length : state;
                    break;
                case Direction.SOUTH:
                        if (entity.getLocation().Y < path[state]) {
                            if (ticks >= SKIPPED_FRAMES) {
                                entity.deriveY(entity.getStepSize());
                                ticks = 0;
                            } else {
                                ticks++;
                            }
                        }
                        state = entity.getLocation().Y >= path[state] ? (state + 1) % path.Length : state;
                    break;
                case Direction.WEST:
                        if (entity.getLocation().X > path[state]) {
                            if (ticks >= SKIPPED_FRAMES) {
                                entity.deriveX(-entity.getStepSize());
                                ticks = 0;
                            } else {
                                ticks++;
                            }
                        }
                        state = entity.getLocation().X <= path[state] ? (state + 1) % path.Length : state;
                    break;
                case Direction.EAST:
                    if (entity.getLocation().X < path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            entity.deriveX(entity.getStepSize());
                            ticks = 0;
                        } else {
                            ticks++;
                        }
                    }
                    state = entity.getLocation().X >= path[state] ? (state + 1) % path.Length : state;
                    break;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using System;

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
        private int stuckFrames;
        private bool stagnant;

        private readonly int[] path;
        private readonly int[] delays;

        private const int SKIPPED_FRAMES = 4;
        private const int STILL_FRAMES = 2;

        private readonly Npc npc;
        private readonly Player player;
        private readonly CollisionManager collisionManager;
        private readonly Direction[] directions;

        public AIPath(Npc npc, Game1 game, int[] path, int[] delays, Direction[] directions) {
            this.npc = npc;
            this.player = game.getPlayer();
            this.collisionManager = game.getInputManager().getCollisionManager();
            this.path = path;
            this.delays = delays;
            this.directions = directions;
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
            if (stuckFrames >= STILL_FRAMES) {
                npc.updateStill();
                stuckFrames = 0;
            }
            switch (npc.getDirection()) {
                case Direction.North:
                    if (npc.getLocation().Y > path[state]) {
                        if (ticks >= SKIPPED_FRAMES) {
                            npc.setDestination(new Vector2(npc.getLocation().X, npc.getLocation().Y - npc.getVelocity()));
                            if (collisionManager.isValid(npc, false)) {
                                stuckFrames = 0;
                                npc.deriveY(-npc.getVelocity());
                                npc.updateMovement();
                            } else {
                                stuckFrames++;
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
                            if (collisionManager.isValid(npc, false)) {
                                stuckFrames = 0;
                                npc.deriveY(npc.getVelocity());
                                npc.updateMovement();
                            } else {
                                stuckFrames++;
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
                            if (collisionManager.isValid(npc, false)) {
                                stuckFrames = 0;
                                npc.deriveX(-npc.getVelocity());
                                npc.updateMovement();
                            } else {
                                stuckFrames++;
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
                            if (collisionManager.isValid(npc, false)) {
                                stuckFrames = 0;
                                npc.deriveX(npc.getVelocity());
                                npc.updateMovement();
                            } else {
                                stuckFrames++;
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

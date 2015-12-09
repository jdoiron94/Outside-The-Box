using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace OutsideTheBox
{

    /// <summary>
    /// Class which represents a non-playing character
    /// </summary>

    public class Npc : Entity
    {

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

        private readonly Vector2 origLoc;
        private readonly Direction origDir;

        private readonly int origVel;

        public Npc(Game1 game, Texture2D texture, Texture2D sight, Vector2 location, SoundEffectInstance effect, Direction direction, NpcDefinition def, int[] offsets, int maxHealth, int velocity, int radius, int reactTime, bool wander) :
            base(texture, location, effect, direction, maxHealth, velocity)
        {
            this.game = game;
            this.def = def;
            this.offsets = offsets;
            this.radius = radius;
            this.reactTime = reactTime;
            this.wander = wander;
            this.sight = sight;
            this.origin = new Vector2(texture.Width / 2F, texture.Height / 2F);
            this.origLoc = new Vector2((int)location.X, (int)location.Y);
            this.origDir = direction;
            this.origVel = velocity;
        }

        public Npc(Game1 game, Texture2D texture, Texture2D sight, Vector2 location, SoundEffectInstance effect, Direction direction, NpcDefinition def, int[] offsets, int radius, int reactTime, bool wander) :
            this(game, texture, sight, location, effect, direction, def, offsets, 100, 3, radius, 10, wander)
        {
        }

        public Npc(Game1 game, Texture2D texture, Texture2D sight, Vector2 location, SoundEffectInstance effect, Direction direction, NpcDefinition def, int radius, int reactTime) :
            this(game, texture, sight, location, effect, direction, def, new int[0], radius, 10, false)
        {
        }

        /// <summary>
        /// Resets the npc to as it was once the level started
        /// </summary>
        public void reset()
        {
            resetHealth();
            setLocation(origLoc);
            setDestination(origLoc);
            getDisplayBar().reset();
            getHitsplat().reset();
            if (path != null) {
                path.reset();
            }
            reactTicks = 0;
            hit = false;
            setVelocity(origVel);
            direction = origDir;
            updateStill();
            def.resetThought();
        }

        /// <summary>
        /// Derives the npc and thought bubble by x
        /// </summary>
        /// <param name="x">The x amount</param>
        public override void deriveX(int x)
        {
            base.deriveX(x);
            def.deriveX(x);
        }

        /// <summary>
        /// Derives the npc and thought bubble by y
        /// </summary>
        /// <param name="y">The y amount</param>
        public override void deriveY(int y)
        {
            base.deriveY(y);
            def.deriveY(y);
        }

        /// <summary>
        /// Returns whether or not the npc has been hit
        /// </summary>
        /// <returns>Returns true if the npc has been hit; otherwise, false</returns>
        public bool wasHit()
        {
            return hit;
        }

        /// <summary>
        /// Sets the npc's hit status
        /// </summary>
        /// <param name="hit">The boolean to set</param>
        public void setHit(bool hit)
        {
            this.hit = hit;
        }

        /// <summary>
        /// Returns an instance of the game
        /// </summary>
        /// <returns></returns>
        public Game getGame()
        {
            return game;
        }

        /// <summary>
        /// Returns an instance of the npc's definition
        /// </summary>
        /// <returns>Returns the npc's definition</returns>
        public NpcDefinition getDefinition()
        {
            return def;
        }

        /// <summary>
        /// Returns the npc's wanderable offsets
        /// </summary>
        /// <returns>Returns an integer array of the npc's wanderable offsets from its origin</returns>
        public int[] getOffsets()
        {
            return offsets;
        }

        /// <summary>
        /// Returns the acting radius of the npc
        /// </summary>
        /// <returns>Returns the radius by which the npc responds to player presence</returns>
        public int getRadius()
        {
            return radius;
        }

        /// <summary>
        /// Returns the npc's reaction time
        /// </summary>
        /// <returns>Returns the npc's reaction time</returns>
        public int getReactTime()
        {
            return reactTime;
        }

        public void setReactTicks(int reactTicks)
        {
            this.reactTicks = reactTicks;
        }

        /// <summary>
        /// Returns the npc's static ai pathing
        /// </summary>
        /// <returns>Returns the npc's static ai pathing</returns>
        public AIPath getPath()
        {
            return path;
        }

        /// <summary>
        /// Returns whether or not the npc is allowed to wander around
        /// </summary>
        /// <returns>Returns true if the npc is allowed to wander; otherwise, false</returns>
        public bool isWanderer()
        {
            return wander;
        }

        /// <summary>
        /// Sets the npc's static ai path
        /// </summary>
        /// <param name="path">The path to be set</param>
        public void setPath(AIPath path)
        {
            this.path = path;
        }

        /// <summary>
        /// Sets the npc's line of sight bounds
        /// </summary>
        /// <param name="lineOfSight">The bounds to set for los</param>
        public void setLineOfSight(Rectangle lineOfSight)
        {
            this.lineOfSight = lineOfSight;
        }

        /// <summary>
        /// Returns the npc's line of sight
        /// </summary>
        /// <returns>Returns the npc's line of sight bounds</returns>
        public Rectangle getLineOfSight()
        {
            return lineOfSight;
        }

        public int getDefaultVelocity()
        {
            return origVel;
        }

        /// <summary>
        /// Returns whether or not the npc is within reaction distance of the player
        /// </summary>
        /// <param name="player">The player to check against</param>
        /// <returns>Returns true if the player is within reaction distance; otherwise, false</returns>
        public bool isWithin(Player player)
        {
            return Math.Abs(player.getLocation().Y - location.Y) <= radius && Math.Abs(location.X - player.getLocation().X) <= radius;
        }

        /// <summary>
        /// Handles the npc's reaction to the player
        /// </summary>
        /// <param name="time">The game time to respect</param>
        /// <param name="player">The player to react to</param>
        public void react(GameTime time, Player player, bool face)
        {
            if (projectile == null || reactTicks < reactTime || (!isFacing(player, 1.75F) && !face))
            {
                reactTicks++;
                return;
            }
            if (face)
            {
                setFacing(player);
            }
            if (getDistance(player) <= 100)
            {
                foreach (Npc n in game.getLevel().getNpcs())
                {
                    if (n != null && n != this)
                    {
                        if (isFacing(n, 1.75F) && (getHDistance(n) <= n.getTexture().Width * 2 || getVDistance(n) <= n.getTexture().Height * 2))
                        {
                            return;
                        }
                    }
                }
                double ms = time.TotalGameTime.TotalMilliseconds;
                if (lastFired == -1 || ms - lastFired >= projectile.getCooldown())
                {
                    game.addProjectile(createProjectile(ms));
                    reactTicks = 0;
                }
            }
        }

        /// <summary>
        /// Updates the npc's line of sight bounds depending on its direction
        /// </summary>
        public void updateLineOfSight()
        {
            if (direction == Direction.North)
            {
                lineOfSight = new Rectangle((int)location.X, (int)location.Y + 10 - texture.Height * 3, texture.Width, (texture.Height * 3));
                angle = MathHelper.ToRadians(0.0F);
                losBegin = new Vector2(location.X, location.Y - lineOfSight.Height + 10.0F);
            }
            else if (direction == Direction.South)
            {
                lineOfSight = new Rectangle((int)location.X, (int)location.Y + texture.Height, texture.Width, texture.Height * 3);
                angle = MathHelper.ToRadians(180.0F);
                losBegin = new Vector2(location.X, location.Y + (texture.Height * 3.0F));
            }
            else if (direction == Direction.West)
            {
                lineOfSight = new Rectangle((int)location.X + 15 - texture.Width * 3, (int)location.Y, texture.Width * 3, texture.Height);
                angle = MathHelper.ToRadians(-90.0F);
                losBegin = new Vector2(location.X - lineOfSight.Width + 15.0F, location.Y);
            }
            else
            {
                lineOfSight = new Rectangle((int)location.X - 15 + texture.Width, (int)location.Y, texture.Width * 3, texture.Height);
                angle = MathHelper.ToRadians(90.0F);
                losBegin = new Vector2(location.X + (texture.Width * 3.0F) - 15.0F, location.Y);
            }
        }

        /// <summary>
        /// Updates the npc's movements and reactions
        /// </summary>
        /// <param name="time">The game time to respect</param>
        public void update(GameTime time)
        {
            addCombatTicks();
            if (path != null)
            {
                path.update();
                updateLineOfSight();
                react(time, game.getPlayer(), false);
                return;
            }
            react(time, game.getPlayer(), true);
        }

        /// <summary>
        /// Updates the drawing of the npc
        /// </summary>
        /// <param name="batch">The SpriteBatch to draw with</param>
        public override void draw(SpriteBatch batch)
        {
            batch.Draw(texture, location, Color.White);
            if (sight != null)
            {
                batch.Draw(sight, Vector2.Add(losBegin, origin), null, Color.White, angle, origin, 1.0F, SpriteEffects.None, 0.0F);
            }
        }
    }
}

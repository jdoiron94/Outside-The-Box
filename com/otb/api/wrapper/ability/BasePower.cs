using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    /// <summary>
    /// Interface which holds relevant information to serve as a base for all powers
    /// </summary>

    public interface BasePower {

        int getManaCost();
        int getExpCost();
        void unlockPower(bool unlock);
        void activatePower(bool activate);
        bool isActivated();
        bool isUnlocked();
        void behavior(GameTime gameTime);
    }
}

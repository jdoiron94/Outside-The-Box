using Microsoft.Xna.Framework;

namespace OutsideTheBox {

    public interface BasePower {

        int getManaCost();
        int getExpCost();
        void unlockPower(bool unlock);
        void activatePower(bool activate);
        bool isActivated();
        bool isUnlocked();
        void behavior(GameTime gametime);
    }
}

namespace OutsideTheBox {

    public interface DrainPower {

        int getDrainRate();
        int getDrainCooldown();
        void updateDrainCooldown();
    }
}

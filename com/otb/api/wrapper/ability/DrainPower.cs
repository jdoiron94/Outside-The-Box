namespace OutsideTheBox {

    /// <summary>
    /// Interface which maintains methods for draining abilities
    /// </summary>

    public interface DrainPower {

        int getDrainRate();
        int getDrainCooldown();
        void updateDrainCooldown();
    }
}

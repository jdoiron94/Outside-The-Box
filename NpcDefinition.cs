namespace KineticCamp {

    public class NpcDefinition {

        /*
         * Class which can define an npc's name and helpful hints
         */

        private readonly string name;
        private readonly string[] hints;

        private readonly int[] hintLevels;

        public NpcDefinition(string name, string[] hints, int[] hintLevels) {
            this.name = name;
            this.hints = hints;
            this.hintLevels = hintLevels;
        }

        /*
         * Returns the definition's name
         */
        public string getName() {
            return name;
        }

        /*
         * Returns the definition's hints
         */
        public string[] getHints() {
            return hints;
        }

        /*
         * Returns the levels needed to view the hints
         */
        public int[] getHintLevels() {
            return hintLevels;
        }
    }
}

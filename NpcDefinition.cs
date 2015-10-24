namespace OutsideTheBox {

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

        /// <summary>
        /// Returns the npc's name
        /// </summary>
        /// <returns>Returns the npc's name</returns>
        public string getName() {
            return name;
        }

        /// <summary>
        /// Returns the hints from the definition
        /// </summary>
        /// <returns>Returns a string array of helpful hints</returns>
        public string[] getHints() {
            return hints;
        }

        /// <summary>
        /// Returns the levels required to view the hints
        /// </summary>
        /// <returns>Returns an integer array of required levels to view the corresponding level hints</returns>
        public int[] getHintLevels() {
            return hintLevels;
        }
    }
}

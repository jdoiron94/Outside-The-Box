namespace KineticCamp {

    public class NpcDefinition {

        private readonly string name;
        private readonly string[] hints;

        private readonly int[] hintLevels;

        public NpcDefinition(string name, string[] hints, int[] hintLevels) {
            this.name = name;
            this.hints = hints;
            this.hintLevels = hintLevels;
        }

        public string getName() {
            return name;
        }

        public string[] getHints() {
            return hints;
        }

        public int[] getHintLevels() {
            return hintLevels;
        }
    }
}

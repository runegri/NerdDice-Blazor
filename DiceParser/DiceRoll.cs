using System.Linq;

namespace no.Rag.DiceParser
{
    public class DiceRoll
    {

        public DiceRoll(int[] rolls)
        {
            DiceRolls = rolls;
            Value = rolls.Sum();
        }

        public int[] DiceRolls { get; private set; }
        public int Value { get; set; }

        public static DiceRoll Empty
        {
            get { return new DiceRoll(new int[0]); }
        }

        public override string ToString()
        {
            var rolls = string.Join(" + ", DiceRolls);
            return $"{rolls} = {Value}";
        }
    }
}

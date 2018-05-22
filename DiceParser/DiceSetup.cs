using System.Linq;

namespace no.Rag.DiceParser
{
    public class DiceSetup
    {
        public DiceSet[] DiceSets { get; internal set; }

        public DiceRoll Roll()
        {
            var rolls = DiceSets.SelectMany(s => s.Roll()).ToArray();
            return new DiceRoll(rolls);
        }

        public int MinThrow { get { return DiceSets.Sum(s => s.MinThrow); } }
        public int MaxThrow { get { return DiceSets.Sum(s => s.MaxThrow); } }
        public double AverageThrow { get { return DiceSets.Sum(s => s.AverageThrow); } }

        public override string ToString()
        {
            return string.Join("+", DiceSets.Select(d => d.ToString()));
        }

    }

    public class DiceSet
    {
        public int NumberOfDice { get; private set; }
        public int DiceType { get; private set; }

        public int MinThrow
        {
            get
            {
                if (NumberOfDice >= 0)
                {
                    return NumberOfDice;
                }
                // Min value for negative dice
                return NumberOfDice * DiceType;
            }
        }
        public int MaxThrow
        {
            get
            {
                if (NumberOfDice >= 0)
                {
                    return NumberOfDice * DiceType;
                }
                // Max value for negative dice
                return NumberOfDice;
            }
        }
        public double AverageThrow { get { return (double)(MinThrow + MaxThrow) / 2; } }

        public DiceSet(int numberOfDice, int diceType)
        {
            NumberOfDice = numberOfDice;
            DiceType = diceType;
        }

        public DiceSet(string dice)
        {
            var diceSetup = new DiceParser().Parse(dice);
            if(diceSetup.DiceSets.Length != 1)
            {
                throw new DiceParserException("Dice set can consist of only one roll");
            }

            var set = diceSetup.DiceSets[0];

            NumberOfDice = set.NumberOfDice;
            DiceType = set.DiceType;
        }

        public virtual int[] Roll()
        {
            return RandomDice.Roll(DiceType, NumberOfDice);
        }

        public override string ToString()
        {
            return $"{NumberOfDice}d{DiceType}";
        }

        public string TextualRepresentation
        {
            get { return ToString(); }
        }
    }

    public class ConstantDiceSet : DiceSet
    {
        public ConstantDiceSet(int constant)
            : base(constant, 1)
        { }

        public override int[] Roll()
        {
            return new[] { NumberOfDice };
        }
    }
}

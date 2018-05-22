using System;
using System.Collections.Generic;
using System.Linq;

namespace no.Rag.DiceParser
{
    public class DiceParser
    {

        public const int MaxNumberOfDice = 100;
        public const int MaxDiceValue = 1000;

        public DiceSetup Parse(string dice)
        {
            try
            {
                dice = dice.Replace(" " , "");
                var diceSets = SplitStringIntoSets(dice).ToArray();

                if(!diceSets.Any())
                {
                    throw new DiceParserException(dice);
                }

                return new DiceSetup {DiceSets = diceSets};
            }
            catch(Exception exception)
            {
                throw new DiceParserException(dice, exception);
            }
        }

        private IEnumerable<DiceSet> SplitStringIntoSets(string dice)
        {
            var setStart = 0;
            var setLength = 0;
            var diceSet = "";
            for(var i=0; i<dice.Length;i++)
            {
                var currentChar = dice[i];
                if(currentChar == '+' || currentChar == '-' && setLength > 1)
                {
                    diceSet = dice.Substring(setStart, setLength);
                    yield return ParseDiceSet(diceSet);
                    setStart = i ;
                    setLength = 1;
                }
                else
                {
                    setLength++;
                }
            }
            diceSet = dice.Substring(setStart, setLength);
            yield return ParseDiceSet(diceSet);

        }

        private DiceSet ParseDiceSet(string diceSet)
        {
            var indexOfD = diceSet.IndexOf("d", StringComparison.OrdinalIgnoreCase);

            if(indexOfD == -1)
            {
                // No d in the string, assume constant
                var constantValue = LimitToMaxValue(Convert.ToInt32(diceSet), MaxNumberOfDice);                
                return new ConstantDiceSet(constantValue);
            }

            var diceType = LimitToMaxValue(Convert.ToInt32(diceSet.Substring(indexOfD + 1, diceSet.Length - indexOfD - 1)), MaxDiceValue);

            var number = LimitToMaxValue(Convert.ToInt32(diceSet.Substring(0, indexOfD)), MaxNumberOfDice);

            return new DiceSet(number, diceType);
        }

        private int LimitToMaxValue(int number, int maxValue)
        {
            if (number > maxValue)
            {
                return maxValue;
            }

            if (number < -maxValue)
            {
                return -maxValue;
            }

            return number;
        }
    }
}

using System;
using System.Collections.Generic;

namespace no.Rag.DiceParser
{
    internal static class RandomDice
    {
        private static readonly Random Random = new Random();

        public static int[] Roll(int diceType, int numberOfDice)
        {
            var rolls = new List<int>();
            for (var roll = 0; roll < Math.Abs(numberOfDice); roll++)
            {
                if (numberOfDice > 0)
                {
                    rolls.Add(Random.Next(diceType) + 1);
                }
                else
                {
                     rolls.Add(-(Random.Next(diceType) + 1));
                }
            }
            return rolls.ToArray();
        }

    }
}

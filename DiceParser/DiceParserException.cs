using System;

namespace no.Rag.DiceParser
{
    public class DiceParserException : Exception
    {
        public string DiceText { get; private set; }

        public DiceParserException(string diceText, Exception innerException)
            : base("Cannot parse the text " + diceText, innerException)
        {
            DiceText = diceText;
        }

        public DiceParserException(string diceText)
            : base("Cannot parse the text " + diceText)
        {
            DiceText = diceText;
        }
    }
}

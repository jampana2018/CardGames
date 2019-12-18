using System;

namespace CardGames
{
    public class Card
    {

        public string Me { get; set; }

        public string Face { get; set; }

        public Symbol Symbol { get; set; }

        public Card(string str)
        {
            Me = str;
            bool IsSuccess = Enum.TryParse(str.Substring(str.Length - 1), out Symbol s);
            Symbol = s;
            Face = str.Substring(0, str.Length - 1);
        }

        public override string ToString()
        {
            bool IsSuccess = Enum.TryParse(Face, out CardFace face);
            string strFace = IsSuccess ? face.GetDescription() : Face;

            return $"Card {strFace} of {Symbol.GetDescription()}";
        }
    }
}

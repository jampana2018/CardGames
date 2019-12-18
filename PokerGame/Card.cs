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
			Enum.TryParse(str.Substring(str.Length - 1), out Symbol s);
			Symbol = s;

            Face = str.Substring(0, str.Length - 1);
        }

        public override string ToString()
        {
            return $"Card {Face} {Symbol.GetDescription()}";
        }
    }
}

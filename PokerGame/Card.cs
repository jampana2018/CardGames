using System;
namespace CardGames
{
    public class Card
    {
        public Card(string str)
        {
			Me = str;
            Symbol s;
            Enum.TryParse(str.Substring(str.Length - 1), out s);
            symbol = s;

            Face = str.Substring(0, str.Length - 1);
        }

		public string Me { get; set; }

        public string Face { get; set; }
        public Symbol symbol { get; set; }

        public override string ToString()
        {
            return $"Card {Face} {symbol.GetDescription()}";
        }
    }
}

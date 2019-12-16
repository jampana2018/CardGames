using System.Collections.Generic;
using System.Linq;
using System;

namespace CardGames
{
    public class CardPlayer
    {
        public List<Card> Cards { get; set; }

        public Stakes stake { get; set; }

        public int Rank { get; set; }

        public int ID { get; set; }

        public int BigCard
        {
            get
            {
                return Cards.Max(o => (int)Enum.Parse(typeof(CardFace), o.Face));
            }
        }

        public string Faces
        {
            get
            {
                return String.Join("", Cards.OrderBy(o => (int)Enum.Parse(typeof(CardFace), o.Face)).Select(card => card.Face));
            }
        }

        public override string ToString()
        {
            return $"Player {ID} with '{stake.ToString()}' ( {String.Join(" ", Cards.Select(card => card.Me))} )";
        }
        public CardPlayer(int playerIndex, List<Card> cards)
        {
            ID = playerIndex;
            Cards = cards;
        }
    }
}

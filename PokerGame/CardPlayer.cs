using System.Collections.Generic;
using System.Linq;
using System;

namespace CardGames
{
    public class CardPlayer
    {
        public List<Card> Cards { get; set; }

        public int Rank { get; set; }

        public int ID { get; set; }

        public List<int> Kickers { get; set; }

        public Stakes Stake
        {
            get
            {
                return (Stakes)Enum.Parse(typeof(Stakes), Rank.ToString());
            }
        }


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

        public void ShowCards()
        {
            Console.WriteLine($"Player {ID} hand having:");
            foreach (var card in Cards)
                Console.WriteLine(card);
        }

        public override string ToString()
        {
            return $"{Environment.NewLine} Player {ID} with '{Stake}' ( {String.Join(" ", Cards.Select(card => card.Me))} )";
        }


        public CardPlayer(int playerIndex, List<Card> cards)
        {
            ID = playerIndex;
            Cards = cards;
        }
    }
}

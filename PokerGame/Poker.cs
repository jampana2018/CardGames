using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public class Poker : IGame
    {
        private static IList<CardPlayer> hands;

        private const int CARD_COUNT = 5;

        public Poker()
        {
            hands = new List<CardPlayer>();
        }

        string IGame.Name
        {
            get { return "Texas Hold'em"; }
        }


        void IGame.Shuffle(IEnumerable<Card> cards)
        {

            if (cards.Count() % CARD_COUNT != 0)
                throw new Exception("Invalid Set of cards for Poker");

            for (int player = 0; player * CARD_COUNT < cards.Count();)
            {
                var playerCards = cards?.Skip(player * CARD_COUNT)?.Take(CARD_COUNT)?.ToList();

                if (playerCards != null)
                {
                    //CardPlayer hand = new CardPlayer(playerCards.OrderBy(x => x.Face).ToList());
                    CardPlayer hand = new CardPlayer(player + 1, playerCards);
                    hand.Rank = GetRank(hand);

                    //Console.WriteLine(hand.Rank);
                    hands.Add(hand);
                }
                player++;
            }

            //Console.WriteLine($"No of hands: {this.HandCount}");
        }

        string IGame.ShowWinner()
        {
            return String.Concat(WhoisWinner.Select(x => x.ToString()));
        }
         private int GetRank(CardPlayer player)
        {
            Stakes playerStake = Stakes.BigCard;

            string sequence = "12345678910JQKA";
            string Royals = "10, J, Q, K, A";

            var groupSymbol = from card in player.Cards
                              group card by card.symbol into newGroup
                              select new { key = newGroup.Key, cnt = newGroup.Count() };

            var pairs = from card in player.Cards
                        group card by card.Face into pair
                        select new { key = pair.Key, cnt = pair.Count() };

            bool IsSequence = sequence.IndexOf(player.Faces, StringComparison.OrdinalIgnoreCase) != -1;

            // foreach (var item in pairs)
            //     Console.WriteLine($"{item.key}- {item.cnt}");

            if (groupSymbol.Count() == 1)
            {
                //Console.WriteLine(player.Faces);
                // Royal Flush  - 100
                //Same Symbol with 10, J, Q, K, A

                //player.Cards.All((x) => {  Console.WriteLine(x.Face); return true;});
                if (player.Cards.All(x => Royals.Contains(x.Face)))
                    playerStake = Stakes.RoyalFlush;

                //straight flush - 90
                else if (IsSequence)
                    playerStake = Stakes.Flush;
            }
            //Four of A Kind - 80
            else if (pairs.Any(x => x.cnt == 4))
                playerStake = Stakes.FourOfKind;

            //Full House - 70
            else if (pairs.Any(x => x.cnt == 3) && pairs.Any(x => x.cnt == 2))
                playerStake = Stakes.FullHouse;

            //Flush - 60
            else if (groupSymbol.Count() == 1)
                playerStake = Stakes.Flush;

            //Straight - 50
            else if (IsSequence)
                playerStake = Stakes.Straight;

            //Three of Kind - 40
            else if (pairs.Any(x => x.cnt == 3))
                playerStake = Stakes.ThreeOfKind;

            //Two Pair  - 30
            else if (pairs.Count(x => x.cnt == 2) == 2)
                playerStake = Stakes.TwoPair;

            //One Pair - 20
            else if (pairs.Count(x => x.cnt == 2) == 1)
                playerStake = Stakes.OnePair;

            player.stake = playerStake;

            Console.WriteLine($"{player.ToString()}");

            //Big Card - get big card
            return playerStake == Stakes.BigCard ? player.BigCard : (int)playerStake;

        }

        private IEnumerable<CardPlayer> WhoisWinner
        {
            get
            {
                var bigHands =  hands.Where(x => x.Rank == hands.Max(y => y.Rank));

				if(bigHands.Count() > 1){
					//Tie Break if possible.
				}

				return bigHands;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public class Poker : IGame
    {
        const string sequence = "12345678910JQKA";
        const string Royals = "10, J, Q, K, A";
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
                    var hand = new CardPlayer(player + 1, playerCards);
                    hand.Rank = GetRank(hand);
                    hands.Add(hand);

                    Console.WriteLine();
                    hand.ShowCards();
                }
                player++;
            }
        }

        string IGame.ShowWinner()
        {
            return string.Concat(WhoisWinner.Select(x => x.ToString()));
        }

        private int GetRank(CardPlayer player)
        {
            Stakes playerStake = Stakes.BigCard;
            var groupSymbol = from card in player.Cards
                              group card by card.Symbol into newGroup
                              select new { key = newGroup.Key, cnt = newGroup.Count() };

            var pairs = from card in player.Cards
                        group card by card.Face into pair
                        select new { key = pair.Key, cnt = pair.Count() };

            bool IsSequence = sequence.IndexOf(player.Faces, StringComparison.OrdinalIgnoreCase) != -1;

            List<int> kickers = pairs.OrderByDescending(x => x.cnt)
                                        .Select(x => (int)Enum.Parse(typeof(CardFace), x.key)).ToList();

            if (groupSymbol.Count() == 1)
            {
                // Royal Flush  - 100
                //Same Symbol with 10, J, Q, K, A

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

            //Add kicker cards to player for tie break
            player.Kickers = kickers;
            return (int)playerStake;
        }

        private string GetRankCard(IEnumerable<dynamic> pairs, int cnt)
        {
            return pairs.First(x => x.cnt == cnt).key;
        }

        /// <summary>
        /// Find Who Big Stakes have
        /// </summary>
        private IEnumerable<CardPlayer> WhoisWinner
        {
            get
            {
                int winnerStake = hands.Max(y => y.Rank);

                var bigHands = hands.Where(x => x.Rank == hands.Max(y => y.Rank));

                if (bigHands.Count() > 1 && winnerStake != (int)Stakes.RoyalFlush)
                {

                    int elementAt = 0;
                    int kickerCount = bigHands.First().Kickers.Count;

                    //Add hands to tie break, then remove one by one out of the game
                    List<int> tieBreakers = bigHands.Select(x => x.ID).ToList();

                    do
                    {
                        var maxstake = hands.Max(item => item.Kickers.ElementAt(elementAt));
                        foreach (var hand in bigHands)
                        {
                            if (hand.Kickers.ElementAt(elementAt) != maxstake)
                                tieBreakers.Remove(hand.ID);
                        }

                        ++elementAt;
                    } while (elementAt < kickerCount && tieBreakers.Count() > 1);


                    if (tieBreakers.Count() > 1)
                        Console.WriteLine("Split the Pot/Draw with below players");

                    return bigHands.Where(x => tieBreakers.Contains(x.ID));
                }

                return bigHands;
            }
        }
    }
}

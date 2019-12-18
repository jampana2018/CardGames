using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Poker Game");

            //Open Game
            GameFactory factory = new GameFactory();
            IGame myGame = factory.StartGame("Poker");

            try
            {
                Console.WriteLine("Distribute cards to players seperate with space");

                string inputCards = Console.ReadLine();

                if (string.IsNullOrEmpty(inputCards))
                    throw new Exception("Not valid input");

                //Accept Cards
                IEnumerable<string> inputStrings = inputCards.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                IList<Card> cards = new List<Card>();

                foreach (var str in inputStrings)
                {
                    bool IsSuccess = Enum.TryParse(str.Substring(str.Length - 1), out Symbol s);
                    if (IsSuccess)
                    {
                        var FaceValue = str.Substring(0, str.Length - 1);
                        Enum.TryParse(FaceValue, out CardFace face);

                        if ((int)face < 0 || (int)face > 14)
                            throw new Exception("Invalid Card");

                        cards.Add(new Card(str));
                    }
                }
                //Shuffle Cards to players
                myGame.Shuffle(cards);

                Console.WriteLine();

                //Show Winner
                Console.WriteLine($"Winner(s) for {myGame.Name} ! " + myGame.ShowWinner());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Game terminated, Reason :  {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter any key to exit");
            Console.ReadLine();
        }
    }
}

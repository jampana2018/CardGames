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
            GameFactory factory = new GameFactory();
            IGame myGame = factory.StartGame("Poker");

            try
            {
                Console.WriteLine("Distribute cards to players seperate with space");

                string inputCards = Console.ReadLine();

                if (string.IsNullOrEmpty(inputCards))
                    throw new Exception("Not valid input");

                IEnumerable<Card> cards = inputCards.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select((x) => new Card(x));

                myGame.Shuffle(cards);

                Console.WriteLine();
				Console.WriteLine();

                Console.WriteLine($"Winner for {myGame.Name} ! " + myGame.ShowWinner());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Interruption in the Game due to:  {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter any key to exit");
            Console.ReadLine();
        }
    }

}

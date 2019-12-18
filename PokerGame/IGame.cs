using System.Collections.Generic;

namespace CardGames
{
    public interface IGame
    {
        string Name { get; }

        void Shuffle(IEnumerable<Card> cards);

		string ShowWinner();
    }
}

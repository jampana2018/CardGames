
namespace CardGames
{
    public class GameFactory
    {
        public IGame StartGame(string name)
        {
            switch (name)
            {
                case "Poker":
                    return new Poker();
                default: return null;
            }
        }
    }
}

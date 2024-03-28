
namespace GenSpil
{
    public class Inventory
    {
        private string filePath = @"C:\Users\Gusta\source\repos\GenSpil\GenSpil\GameDatabase.txt";

        public Inventory()
        {
            // Ensure the file exists when the Inventory is instantiated
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void AddGame(Game game)
        {
            var gameData = $"{game.Name};{game.Genre};{game.NumberOfPlayers};{game.Condition};{game.Price}";
            File.AppendAllLines(filePath, new[] { gameData });
        }

        public List<Game> LoadGames()
        {
            var games = new List<Game>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 5)
                {
                    games.Add(new Game
                    {
                        Name = parts[0],
                        Genre = parts[1],
                        NumberOfPlayers = int.Parse(parts[2]),
                        Condition = parts[3],
                        Price = double.Parse(parts[4])
                    });
                }
            }

            return games;
        }


        public List<Game> SearchGames(SearchCriteria criteria)
        {
            var games = LoadGames();
            return games.Where(g => criteria.Matches(g)).ToList();
        }

        public void PrintInventoryReport(SortCriteria sortBy)
        {
            var games = LoadGames();
            IEnumerable<Game> sortedGames = sortBy switch
            {
                SortCriteria.Name => games.OrderBy(g => g.Name),
                SortCriteria.Genre => games.OrderBy(g => g.Genre),
                _ => games,
            };

            foreach (var game in sortedGames)
            {
                Console.WriteLine(game);
            }
        }
    }
}

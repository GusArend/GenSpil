
namespace GenSpil
{
    public class DataHandler
    {
        private string filePath = @"C:\Users\Gusta\source\repos\GenSpil\GenSpil\GameDatabase.txt";
        private string userFilePath = @"C:\Users\Gusta\source\repos\GenSpil\GenSpil\Users.txt";

        public DataHandler()
        {
            // Ensure the file exists when the Inventory is instantiated
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            if (!File.Exists(userFilePath))
            {
                File.Create(userFilePath).Close();
            }
        }

        public void AddGame(Game game)
        {
            string gameData = $"{game.Name};{game.Genre};{game.NumberOfPlayers};{game.Condition};{game.Price}";
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

        public void AddUser(User user)
        {
            string userData = $"{user.Name};{user.Id};{user.Password}";
            File.AppendAllLines(userFilePath, new[] { userData });
        }

        public List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            var lines = File.ReadAllLines(userFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 3)
                {
                    users.Add(new User
                    {
                        Name = parts[0],
                        Id = parts[1],
                        Password = parts[2],


                    });
                }
            }

            return users;
        }

        public bool Login(UserSearchCriteria criteria)
        {
            var users = LoadUsers();
            List<User> user = users.Where(u => criteria.Matches(u)).ToList();

            if ((user.Count > 0) && (criteria.Name == user[0].Name) && (criteria.Password == user[0].Password))
            {
                return true;
            }
            else
            {
                return false;
            }
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

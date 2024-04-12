
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
            try
            {
                string gameData = $"{game.Name};{game.Genre};{game.MinPlayers};{game.MaxPlayers};{game.Condition};{game.Price};{game.HasInqury}{(game.Customer != null ? ";" + game.CustomerInfoToString(game.Customer) : null)}";
                File.AppendAllLines(filePath, new[] { gameData });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong! {e}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
           
        }

        

        public List<Game> LoadGames()
        {
            var games = new List<Game>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 7)
                {
                    games.Add(new Game
                    {
                        Name = parts[0],
                        Genre = parts[1],
                        MinPlayers = int.Parse(parts[2]),
                        MaxPlayers = int.Parse(parts[3]),
                        Condition = parts[4],
                        Price = double.Parse(parts[5]),
                        HasInqury = bool.Parse(parts[6])
                    });
                }
            }
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 8)
                {
                    games.Add(new Game
                    {
                        Name = parts[0],
                        Genre = parts[1],
                        MinPlayers = int.Parse(parts[2]),
                        MaxPlayers = int.Parse(parts[3]),
                        Condition = parts[4],
                        Price = double.Parse(parts[5]),
                        HasInqury = bool.Parse(parts[6]),
                        Customer = new Customer(parts[7].Split(", ")[0], parts[7].Split(", ")[1], parts[7].Split(", ")[2], parts[7].Split(", ")[3].Split(", ") )
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

            bool userExists = false;
            
                try
                {
                    // Læser alle linjer fra brugerfilen og gemmer dem i en List<string>
                    List<string> lines = File.ReadAllLines(userFilePath).ToList();

                    // Gennemgå hver linje og kontroller om brugernavnet eksisterer.
                    foreach (string line in lines)
                    {
                        // Splitter linjen og gemmer substrings i en List<string>
                        List<string> userList = line.Split(';').ToList();

                        //Hvis der er match returner metoden true
                        if (userList.Count > 0 && userList[0] == user.Name)
                        {
                            userExists = true; // Brugeren eksisterer allerede
                        } else
                        {
                        //Hvis der ikke er match mellem valgt brugernavn og brugernavne i databasen returner vi false
                            userExists = false; // Brugeren eksisterer ikke
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Håndtering af fejl under læsning fra filen
                    Console.WriteLine($"Fejl ved læsning fra filen: {ex.Message}");
                }
           
            if ( !userExists ) {
                string userData = $"{user.Name};{user.Password}";
                File.AppendAllLines(userFilePath, new[] { userData });

                Console.WriteLine("User added successfully.");
            } else
            {
                Console.WriteLine("User already exists in the system!");
            }

            Console.ReadLine();
            
        }

        public List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            var lines = File.ReadAllLines(userFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 2)
                {
                    users.Add(new User
                    {
                        Name = parts[0],
                      
                        Password = parts[1],


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


       
    }
}

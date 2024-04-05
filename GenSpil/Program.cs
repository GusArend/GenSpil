using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace GenSpil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataHandler dataHandler = new DataHandler();
            bool exit = false;
            bool isLogedIn = false;

            while (!isLogedIn)
            {
                Console.WriteLine("Please Login");
                isLogedIn = LoginToInventory(dataHandler);
                Console.Clear();
            }




            while (!exit && isLogedIn)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a game to the inventory");
                Console.WriteLine("2. Search for games");
                Console.WriteLine("3. Print the inventory report");
                Console.WriteLine("4. Add a User to the Program");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AddGameToInventory(dataHandler);
                        break;
                    case "2":
                        Console.Clear();
                        SearchGames(dataHandler);
                        break;
                    case "3":
                        Console.Clear();
                        PrintInventoryReport(dataHandler);
                        break;
                    case "4":
                        Console.Clear();
                        AddUserToInventory(dataHandler);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option, please choose again.");
                        break;
                }

            }

            static bool LoginToInventory(DataHandler dataHandler)
            {

                UserSearchCriteria criteria = new UserSearchCriteria();

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = ReadPassword();

                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(password))
                {
                    criteria.Name = name;
                    criteria.Password = password;
                }

                return dataHandler.Login(criteria);
            }



            static void AddUserToInventory(DataHandler dataHandler)
            {
                Console.Write("Enter Username: ");
                string userName = Console.ReadLine();
                string password = "";
                string repeatPassword = "";

                while (password == "" || repeatPassword == "")
                {
                    Console.Write("Enter Password: ");
                    password = ReadPassword();

                    Console.Write("Repeat Password: ");
                    repeatPassword = ReadPassword();

                    if (password != repeatPassword)
                    {
                        Console.Write("Password do not match! Try again.");
                        password = "";
                        repeatPassword = "";
                    }
                    else
                    {
                        User user = new User
                        {
                            Name = userName,
                            Password = password,
                            Id = Guid.NewGuid().ToString()
                        };

                        dataHandler.AddUser(user);
                        Console.WriteLine("User added successfully.");
                    }

                }


            }

            static void AddGameToInventory(DataHandler dataHandler)
            {
                Console.Write("Enter game name: ");
                string name = Console.ReadLine();

                Console.Write("Enter game genre: ");
                string genre = Console.ReadLine();

                Console.Write("Enter number of players: ");
                int numberOfPlayers;
                while (!int.TryParse(Console.ReadLine(), out numberOfPlayers))
                {
                    Console.Write("Please enter a valid number for players: ");
                }

                Console.Write("Enter game condition (New/Used): ");
                string condition = Console.ReadLine();

                Console.Write("Enter game price: ");
                double price;
                while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out price))
                {
                    Console.Write("Please enter a valid price: ");
                }

                Game game = new Game
                {
                    Name = name,
                    Genre = genre,
                    NumberOfPlayers = numberOfPlayers,
                    Condition = condition,
                    Price = price
                };

                dataHandler.AddGame(game);
                Console.WriteLine("Game added successfully.");
            }

            static void SearchGames(DataHandler dataHandler)
            {
                var criteria = new SearchCriteria();

                Console.Write("Enter a genre to search for (or leave blank to skip): ");
                var genre = Console.ReadLine();
                if (!string.IsNullOrEmpty(genre))
                {
                    criteria.Genre = genre;
                }

                var results = dataHandler.SearchGames(criteria);
                if (results.Count > 0)
                {
                    foreach (var game in results)
                    {
                        Console.WriteLine(game);
                    }
                }
                else
                {
                    Console.WriteLine("No games found matching the criteria.");
                }
            }

            static void PrintInventoryReport(DataHandler dataHandler)
            {
                Console.WriteLine("Choose the sort criteria for the report:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Genre");
                Console.Write("Enter your choice (1-2): ");

                var choice = Console.ReadLine();
                SortCriteria sortCriteria = SortCriteria.Name;
                if (choice == "2")
                {
                    sortCriteria = SortCriteria.Genre;
                }

                dataHandler.PrintInventoryReport(sortCriteria);
            }

            static string ReadPassword()
            {
                string password = "";
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    // Ignore any key that is not a character or Backspace
                    if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        // If Backspace is pressed, remove the last character from the password
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b"); // Move the cursor back and erase the last character
                    }
                } while (key.Key != ConsoleKey.Enter);

                Console.WriteLine(); // Move to the next line after pressing Enter
                return password;
            }
        }
    }
}

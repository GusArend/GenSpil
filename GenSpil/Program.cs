using System.Globalization;

namespace GenSpil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a game to the inventory");
                Console.WriteLine("2. Search for games");
                Console.WriteLine("3. Print the inventory report");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice (1-4): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddGameToInventory(inventory);
                        break;
                    case "2":
                        SearchGames(inventory);
                        break;
                    case "3":
                        PrintInventoryReport(inventory);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please choose again.");
                        break;
                }
            }
        }

        static void AddGameToInventory(Inventory inventory)
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

            inventory.AddGame(game);
            Console.WriteLine("Game added successfully.");
        }

        static void SearchGames(Inventory inventory)
        {
            var criteria = new SearchCriteria();

            Console.Write("Enter a genre to search for (or leave blank to skip): ");
            var genre = Console.ReadLine();
            if (!string.IsNullOrEmpty(genre))
            {
                criteria.Genre = genre;
            }

            var results = inventory.SearchGames(criteria);
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

        static void PrintInventoryReport(Inventory inventory)
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

            inventory.PrintInventoryReport(sortCriteria);
        }
    }
}

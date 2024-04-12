

namespace GenSpil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataHandler dataHandler = new DataHandler();
            Game game = new Game();
            InventoryReport inventoryReport = new InventoryReport();
            User user = new User();
            bool exit = false;
            bool isLogedIn = false;
            int selectedIndex = 0;
            string[] options = {
            "Add a game to the inventory",
            "Search for games",
            "Print the inventory report",
            "Add a User to the Program",
            "Exit"
            };

            while (!isLogedIn)
            {
                Console.WriteLine("Please Login");
                isLogedIn = user.LoginToInventory(dataHandler);
                Console.Clear();
            }


            while (!exit && isLogedIn)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{i + 1}. {options[i]}");

                    Console.ResetColor();
                }

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0) selectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedIndex < options.Length - 1) selectedIndex++;
                        break;
                    case ConsoleKey.Enter:
                        PerformAction(selectedIndex, ref exit, ref game, ref dataHandler, ref inventoryReport, ref user);
                        break;
                }

               


            }

            static void PerformAction(int selectedIndex, ref bool exit, ref Game game, ref DataHandler dataHandler, ref InventoryReport inventoryReport, ref User user)
            {
                switch (selectedIndex)
                {
                    case 0:

                        game.AddGameToDatabase(dataHandler);
                        break;
                    case 1:

                        game.SearchGames(dataHandler);
                        break;
                    case 2:

                        List<Game> sortedGames = inventoryReport.PrintInventoryMenu(dataHandler.LoadGames());
                        inventoryReport.PrintInventory(sortedGames);
                        break;
                    case 3:

                        user.AddUserToDatabase(dataHandler);
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:

                        Console.WriteLine("Invalid option, please choose again.");
                        break;
                }
            }
        }
    }
}

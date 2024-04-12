

namespace GenSpil

{
  
    public class InventoryReport
    {
        public List<Game> PrintInventoryMenu(List<Game> games)
        {

            bool invalidChoice = true;
            while (invalidChoice)
            {
                //Inventory printing menu
                Console.WriteLine("How would you like to sort the report?");
                Console.WriteLine("1. By name?");
                Console.WriteLine("2. By genre?");
                Console.Write("Enther choice: ");
                string sortChoice = Console.ReadLine();

                switch (sortChoice)
                {
                    case "1":
                        invalidChoice = false; //valid choice, exit loop
                        return printInventoryByName(games);
                    case "2":
                        invalidChoice = false; //valid choice, exit loop
                        return printInventoryByGenre(games);
                    default:
                        Console.WriteLine("Invalid choice, please select a valid option");
                        break;
                }
            }
            return games;
        }


        //method to print, sorting by name
        private List<Game> printInventoryByName(List<Game> games)
        {
            Console.WriteLine("Inventory list sorted by name");
            return games.OrderBy(g => g.Name).ToList();
        }


        //method to print, sorting by genre
        private List<Game> printInventoryByGenre(List<Game> games)
        {
            Console.WriteLine("Inventory list sorted by genre");
            return games.OrderBy(g => g.Genre).ThenBy(g => g.Name).ToList();
        }

        //method for printing the list
        public void PrintInventory(List<Game> games)
        {
            Console.WriteLine("Inventory report:");
            
            Console.WriteLine("┌──────────────────────────────────┬────────────────┬────────────┬───────────┬──────────┐");
            Console.WriteLine($"│{"Name",-34}│{"Players",-16}│{"Genre",-12}│{"Condition",-11}│{"Price",-10}│");
            foreach (var game in games)
            {
                
                Console.WriteLine("├──────────────────────────────────┼────────────────┼────────────┼───────────┼──────────┤");
                Console.WriteLine($"│{game.Name,-34}│{game.MinPlayers + "-" + game.MaxPlayers,-16}│{game.Genre,-12}│{game.Condition,-11}│{game.Price,-10}│");
                
            }
            Console.WriteLine("└──────────────────────────────────┴────────────────┴────────────┴───────────┴──────────┘");
            Console.WriteLine();

            Console.ReadLine();
        }
    }

}


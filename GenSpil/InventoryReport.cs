

namespace GenSpil

{
    public enum SortCriteria
    {
        Name,
        Genre
    }
    public class InventoryReport
    {
        private List<Game> games;
        private SortCriteria sortBy;

        public void CreateInventoryReport(List<Game> games, SortCriteria sortBy)
        {
            this.games = games;
            this.sortBy = sortBy;
        }

        public void GenerateReport()
        {
            var sortedGames = sortBy == SortCriteria.Name ?
                              games.OrderBy(g => g.Name) :
                              games.OrderBy(g => g.Genre);

            foreach (var game in sortedGames)
            {
                Console.WriteLine(game);
            }
        }

        public void PrintInventoryReport(DataHandler dataHandler)
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

    }
}

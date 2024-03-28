

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

        public InventoryReport(List<Game> games, SortCriteria sortBy)
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

    }
}

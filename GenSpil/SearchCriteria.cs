
namespace GenSpil
{
    public class SearchCriteria
    {
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public int? Players { get; set; }
        public string? Condition { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool? HasInqury { get; set; }

        public bool Matches(Game game)
        {
            return (string.IsNullOrEmpty(Name) || game.Name.ToLower().Contains(Name.ToLower())) &&
                   (string.IsNullOrEmpty(Genre) || game.Genre.ToLower() == Genre.ToLower()) &&
                   (!Players.HasValue || (game.MinPlayers <= Players && game.MaxPlayers >= Players)) &&
                   (string.IsNullOrEmpty(Condition) || game.Condition.ToLower() == Condition.ToLower()) &&
                   (!MinPrice.HasValue || game.Price >= MinPrice) &&
                   (!MaxPrice.HasValue || game.Price <= MaxPrice) &&
                   (!HasInqury.HasValue || game.HasInqury == true);
        }
    }
}

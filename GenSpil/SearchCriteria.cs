
namespace GenSpil
{
    public class SearchCriteria
    {
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public string? Condition { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool? HasInqury { get; set; }

        public bool Matches(Game game)
        {
            return (string.IsNullOrEmpty(Name) || game.Name.ToLower().Contains(Name.ToLower())) &&
                   (string.IsNullOrEmpty(Genre) || game.Genre.ToLower() == Genre.ToLower()) &&
                   (!MinPlayers.HasValue || game.NumberOfPlayers >= MinPlayers) &&
                   (!MaxPlayers.HasValue || game.NumberOfPlayers <= MaxPlayers) &&
                   (string.IsNullOrEmpty(Condition) || game.Condition.ToLower() == Condition.ToLower()) &&
                   (!MinPrice.HasValue || game.Price >= MinPrice) &&
                   (!MaxPrice.HasValue || game.Price <= MaxPrice) &&
                   (!HasInqury.HasValue || game.HasInqury == true);
        }
    }
}

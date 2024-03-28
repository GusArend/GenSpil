
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

        public bool Matches(Game game)
        {
            return (string.IsNullOrEmpty(Name) || game.Name.Contains(Name)) &&
                   (string.IsNullOrEmpty(Genre) || game.Genre == Genre) &&
                   (!MinPlayers.HasValue || game.NumberOfPlayers >= MinPlayers) &&
                   (!MaxPlayers.HasValue || game.NumberOfPlayers <= MaxPlayers) &&
                   (string.IsNullOrEmpty(Condition) || game.Condition == Condition) &&
                   (!MinPrice.HasValue || game.Price >= MinPrice) &&
                   (!MaxPrice.HasValue || game.Price <= MaxPrice);
        }
    }
}

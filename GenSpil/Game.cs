
namespace GenSpil
{
    public class Game
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int NumberOfPlayers { get; set; }
        public string Condition { get; set; }
        public double Price { get; set; }

        private Inquiry[]? inquiries;

        public override string ToString()
        {
            return $"{Name} ({Genre}) - {Condition}, Players: {NumberOfPlayers}, Price: {Price:C}";
        }
    }
}


using System.Globalization;

namespace GenSpil
{
    public class Game
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int NumberOfPlayers { get; set; }
        public string Condition { get; set; }
        public double Price { get; set; }
        public bool HasInqury { get; set; }

        public Customer? Customer;

        public override string ToString()
        {
            return $"{Name} ({Genre}) - {Condition}, Players: {NumberOfPlayers}, Price: {Price:C}, hasInqury: {HasInqury}" +
                $"{(HasInqury ? (", " + CustomerInfoToString(Customer!)) : "")}";
        }

        public void AddGameToDatabase(DataHandler dataHandler)
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

            Console.Write("Does the game have an inquiry? (y/n): ");
            string inquiryResponse = Console.ReadLine()?.Trim().ToLower();

            bool hasInquiry = (inquiryResponse == "y" ? true : false);

            Customer? customer = null;
            if (hasInquiry)
            {
                customer = AddCustomerToGame();
            }

            Game game = new Game
            {
                Name = name,
                Genre = genre,
                NumberOfPlayers = numberOfPlayers,
                Condition = condition,
                Price = price,
                HasInqury = hasInquiry,
                Customer = customer
            };

            dataHandler.AddGame(game);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Game added successfully.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SearchGames(DataHandler dataHandler)
        {
            var criteria = new SearchCriteria();

            Console.WriteLine("Search by: ");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Genre");
            Console.WriteLine("3. Number Of Players");
            Console.WriteLine("4. Condition");
            Console.WriteLine("5. Max Price");
            Console.WriteLine("6. Has Inquery");
            string searchKey = "";

            while (string.IsNullOrEmpty(searchKey))
            {
                int choice = int.Parse(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        Console.Write("Enter a Name to search for: ");
                        searchKey = Console.ReadLine();
                        criteria.Name = searchKey;
                        break;
                    case 2:
                        Console.Write("Enter a Genre to search for: ");
                        searchKey = Console.ReadLine();
                        criteria.Genre = searchKey;
                        break;
                    case 3:
                        Console.Write("Enter Max number of players: ");
                        searchKey = Console.ReadLine();
                        criteria.MaxPlayers = int.Parse(searchKey);
                        break;
                    case 4:
                        Console.Write("Enter a Condition (used/new): ");
                        searchKey = Console.ReadLine();
                        criteria.Condition = searchKey;
                        break;
                    case 5:
                        Console.Write("Enter max price: ");
                        searchKey = Console.ReadLine();
                        criteria.MaxPrice = int.Parse(searchKey);
                        break;
                    case 6:
                        searchKey = "true";
                        criteria.HasInqury = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option.");
                        searchKey = "";
                        break;
                }
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

        static Customer AddCustomerToGame()
        {
            Console.Write("Enter customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer E-mail: ");
            string email = Console.ReadLine();

            Console.Write("Enter customer Phone number: ");
            string phone = Console.ReadLine();

            Console.Write("Enter customer Address (each line seperated by a comma and space): ");
            string addressInput = Console.ReadLine();

            string[] address = addressInput.Split(", ");

            Customer customer = new Customer(name, email, phone, address);

            return customer;

        }

        public string CustomerInfoToString(Customer customer)
        {
            if (customer != null)
            {
                string address = string.Join(":", customer.Address);

                return $"{customer.Name}, {customer.PhoneNum}, {customer.Email}, {address}";
            } else
            {
                return "";
            }
            
        }
    }
}

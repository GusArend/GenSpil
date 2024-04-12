

namespace GenSpil
{
    public class User
    {
        
        public string Name { get; set; }
        public string Password { get; set; }

        public void AddUserToDatabase(DataHandler dataHandler)
        {
            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();
            string password = "";
            string repeatPassword = "";

            while (password == "" || repeatPassword == "")
            {
                Console.Write("Enter Password: ");
                password = ReadPassword();

                Console.Write("Repeat Password: ");
                repeatPassword = ReadPassword();

                if (password != repeatPassword)
                {
                    Console.WriteLine("Password do not match! Try again.");
                    password = "";
                    repeatPassword = "";
                }
                else
                {
                    User user = new User
                    {
                        Name = userName,
                        Password = password,
                        
                    };

                    dataHandler.AddUser(user);
                    break;
                }
            }
        }

        public bool LoginToInventory(DataHandler dataHandler)
        {

            UserSearchCriteria criteria = new UserSearchCriteria();

            Console.Write("Enter Username: ");
            string name = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = ReadPassword();

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(password))
            {
                criteria.Name = name;
                criteria.Password = password;
            }

            return dataHandler.Login(criteria);
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Ignore any key that is not a character or Backspace
                if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // If Backspace is pressed, remove the last character from the password
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Move the cursor back and erase the last character
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after pressing Enter
            return password;
        }

       

    }
}

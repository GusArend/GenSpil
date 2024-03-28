
namespace GenSpil
{
    internal class User
    {
        private string filePath = @"C:\Users\Gusta\source\repos\GenSpil\GenSpil\Users.txt";
        private string name;
        private int id;
        private string password;


        public User(string name, int id, string password)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            this.name = name;
            this.id = id;
            this.password = password;
        }

        public void AddUser(User user)
        {
            var userData = $"{user.name};{user.id};{user.password}";
            File.AppendAllLines(filePath, new[] { userData });
        }

        //public bool Login(string name, string password)
        //{
        //    var users = LoadGames();
        //    return games.Where(g => criteria.Matches(g)).ToList()
        //    if (name == this.name && password == this.password)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}



namespace GenSpil
{
    public class UserSearchCriteria
    {
        public string Name { get; set; }
        public string Password { get; set; }
       

        public bool Matches(User user)
        {
            return (user.Name.Contains(Name)) &&
                   (user.Password == Password);

        }        
    }
}

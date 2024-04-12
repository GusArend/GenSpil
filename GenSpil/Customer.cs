
namespace GenSpil
{
    public class Customer
    {
        public string Name {  get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string[] Address {  get; set; } 

        // private Inquiry[] inquiries;


        public Customer(string name, string email, string phoneNum,
        string[] address )
        {
            Name = name;
            Email = email;
            PhoneNum = phoneNum;
            Address = address;
        }
        
    }
}

namespace EliteATMWebApp.Models
{
    public class Contact
    {
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Comments { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string getFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}

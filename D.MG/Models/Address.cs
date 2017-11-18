namespace Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string County { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
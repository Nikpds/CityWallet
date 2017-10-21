namespace Models
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string Country { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
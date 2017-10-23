namespace Models
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string County { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
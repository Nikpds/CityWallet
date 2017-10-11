namespace Models
{
    public class Address:EntityBase
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
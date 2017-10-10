namespace Models
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Vat { get; set; }
        public string Password { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
    }
}

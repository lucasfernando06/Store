namespace Store.Models.JWT
{
    public class Jwt
    {
        public string Secret { get; set; }
        public string ExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Valid { get; set; }     
    }
}

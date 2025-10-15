namespace BusinessLogic.DTOs
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public int LifetimeInMinutes { get; set; }
        public string Issuer { get; set; }
    }
}

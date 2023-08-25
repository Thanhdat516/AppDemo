namespace Business_Logic_Layer.Models
{
    public class AppSettingModel
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Subject { get; set; }
    }
}

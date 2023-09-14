namespace Business_Logic_Layer.Models
{
    public class ApiResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public TokenModel Data { get; set; }
    }
}

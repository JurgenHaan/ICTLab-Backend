namespace ICTLab_backend.DTO
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string sessionKey { get; set; }
    }
}
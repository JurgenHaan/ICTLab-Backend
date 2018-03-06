namespace ICTLab_backend.DTO {
    public class ActiveUser {
        public string username { get; set; }
        public string fullName { get; set; }
        public string sessionKey { get; set; }
        public bool valid { get; set; }

        public ActiveUser(string username, string fullName, string sessionKey) {
            this.username = username;
            this.fullName = fullName;
            this.sessionKey = sessionKey;
            valid = true;
        }

        public ActiveUser()
        {
            valid = false;
        }
    }
}
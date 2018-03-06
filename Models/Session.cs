using System;
using System.ComponentModel.DataAnnotations;

namespace ICTLab_backend.Models {
    public class Session {
        [Key]
        public string uuid { get; set; }
        public User user { get; set; }
        public DateTime expiration { get; set; }

        public Session(string uuid, User user, DateTime expiration) {
            this.uuid = uuid;
            this.user = user;
            this.expiration = expiration;
        }

        public Session()
        {
        }
    }
}
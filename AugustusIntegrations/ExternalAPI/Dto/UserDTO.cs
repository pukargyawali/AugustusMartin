using System;
using Newtonsoft.Json;

namespace AugustusIntegrations.ExternalAPI.Dto
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public string UserId { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("website")]
        public string WebSite { get; set; }

        public int PostCount { get; set; }

    }
}

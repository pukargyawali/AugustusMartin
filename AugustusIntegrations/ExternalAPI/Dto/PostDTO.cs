using System;
using Newtonsoft.Json;

namespace AugustusIntegrations.ExternalAPI.Dto
{
    public class PostDTO
    {
        [JsonProperty("id")]
        public string PostId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}

using Newtonsoft.Json;

namespace testWorkIntellectSoft.API.Models.DTO
{
    public class UserDTO
    {
        [JsonProperty("user_id")]
        public int ID { get; set; }
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }
        [JsonProperty("last_name")]
        public string? LastName { get; set; }
        [JsonProperty("birthyear")]
        public int Birthyear { get; set; }
        [JsonProperty("phones")]
        public PhoneDTO[]? Phones { get; set; }
    }
    public class UserAnswerDTO
    {
        [JsonProperty("total_page")]
        public int TotalPage { get; set; }
        [JsonProperty("current_page")]
        public int CurentPage { get; set; }
        [JsonProperty("users")]
        public UserDTO[]? Users { get; set; }
    }
    public class PhoneDTO
    {
        [JsonProperty("phone_id")]
        public int PhoneID { get; set; }
        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }
    }
}

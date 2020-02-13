using Newtonsoft.Json;

namespace ToDo.Model
{
    public class ToDoItem
    {
        [JsonProperty(PropertyName = "id")]
        public int Id{ get; set; }
        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "completed")]
        public bool IsCompleted { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}

using Newtonsoft.Json;

namespace TicTacToe.Controllers
{
    public class TicTacToeResult
    {
        [JsonProperty(PropertyName = "winner")]
        public string Winner { get; set; }
    }
}

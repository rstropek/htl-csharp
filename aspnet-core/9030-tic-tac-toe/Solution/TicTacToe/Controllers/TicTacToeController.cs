using Microsoft.AspNetCore.Mvc;
using TicTacToe.Logic;

namespace TicTacToe.Controllers
{
    [Route("api")]
    public class TicTacToeController : Controller
    {
        private IBoard boardLogic;

        public TicTacToeController(IBoard boardLogic)
        {
            this.boardLogic = boardLogic;
        }

        [HttpPost]
        [Route("getWinner")]
        public IActionResult Post([FromBody]string[] board)
        {
            if (board == null || board.Length != 9)
            {
                return BadRequest();
            }

            return Ok(new TicTacToeResult { Winner = boardLogic.GetWinner(board) });
        }

        [HttpGet]
        [Route("getWinner")]
        public IActionResult Get([FromQuery(Name = "board")]string boardString)
            => Post(boardString?.Split(','));
    }
}

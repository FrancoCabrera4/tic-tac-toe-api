using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tic_tac_toe_api.Models;

namespace tic_tac_toe_api.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController: ControllerBase
    {
        public ApplicationDbContext _applicationDbContext;

        public GameController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateGame()
        {
            var game = new Game();
            _applicationDbContext.Games.Add(game);  
            await _applicationDbContext.SaveChangesAsync();
            var gameId = game.Id;
            return Ok(gameId);
        }

        [HttpPost("join/{id:int}")]
        public async Task<ActionResult> JoinGame(int id)
        {
            var gameExists = await _applicationDbContext.Games.AnyAsync(game => game.Id == id);

            if(!gameExists) 
            { 
                return BadRequest(); 
            }
            var game = await _applicationDbContext.Games.FirstOrDefaultAsync(game => game.Id == id);
            var gameId = game.Id;

            return Ok(gameId);
        }

        [HttpPost("move")]
        public async Task<ActionResult> MakeMove(int gameId, int square)
        {
            if(square < 0 || square > 81)
            {
                return BadRequest();
            }

            var gameExists = await _applicationDbContext.Games.AnyAsync(game => game.Id == gameId);

            if (!gameExists)
            {
                return BadRequest();
            }
            var game = await _applicationDbContext.Games.Include(x => x.MovesData).FirstOrDefaultAsync(game => game.Id == gameId);     

            var moveData = new MoveData
            {
                GameId = gameId,
                SquareSelected = square
            };

            _applicationDbContext.MovesData.Add(moveData);
            await _applicationDbContext.SaveChangesAsync();

            var moveNumberN = game.MovesData.Count;
            
            return Ok(moveNumberN);
        }

        [HttpGet("move")]
        public async Task<ActionResult> GetMove(int gameId, int lastMoveNumber)
        {
            var gameExists = await _applicationDbContext.Games.AnyAsync(game => game.Id == gameId);

            if (!gameExists)
            {
                return BadRequest();
            }

            var game = await _applicationDbContext.Games.Include(x => x.MovesData).FirstOrDefaultAsync(game => game.Id == gameId);

            if(lastMoveNumber+1 == game.MovesData.Count)
            {
                var lastMove = game.MovesData.LastOrDefault().SquareSelected;

                return Ok(lastMove);
            }
            else
            {
                return Ok(-1);
            }
            
        }

    }
}

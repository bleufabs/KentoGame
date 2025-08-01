using Microsoft.AspNetCore.Mvc;
using KentoGame.Models;

namespace KentoGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private static GameState _state = new GameState();
        private static GameEngine _engine = new GameEngine(_state);

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            _state = new GameState();
            _engine = new GameEngine(_state);
            var scene = _engine.GetCurrentScene();
            return Ok(new
            {
                text = scene.Text,
                choices = scene.Choices.Select(c => new { id = c.Id, text = c.Text }),
                affection = _state.Affection
            });
        }

        [HttpGet("scene")]
        public IActionResult GetScene()
        {
            var scene = _engine.GetCurrentScene();
            return Ok(new
            {
                text = scene.Text,
                choices = scene.Choices.Select(c => new { id = c.Id, text = c.Text }),
                affection = _state.Affection,
                response = _engine.GetResponseText()
            });
        }

        [HttpPost("choice")]
        public IActionResult PostChoice([FromBody] int choiceId)
        {
            _engine.ApplyChoice(choiceId);
            return Ok(); // keep it simple — let the frontend call /scene afterward
        }

        [HttpPost("next")]
        public IActionResult NextScene()
        {
            _engine.MoveToNextScene();
            return Ok(); // same — let frontend call /scene after
        }

        [HttpGet("affection")]
        public IActionResult GetAffection()
        {
            return Ok(_state.Affection);
        }
    }
}

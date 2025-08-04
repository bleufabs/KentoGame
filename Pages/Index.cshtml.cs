using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KentoGame.Models;

namespace KentoGame.Pages
{
    public class IndexModel : PageModel
    {
        public string? SceneText { get; set; }
        public List<string> Choices { get; set; } = new();
        public int Affection { get; set; }

        private static GameState _state = new GameState();
        private static GameEngine _engine = new GameEngine(_state);

        public void OnGet()
        {
            var scene = _engine.GetCurrentScene();
            SceneText = scene.Text;
            Choices = scene.Choices.Select(c => c.Text).ToList();
            Affection = _engine.GetAffection();

        }

        public IActionResult OnPostChoice(int choiceIndex)
        {
            _engine.ApplyChoice(choiceIndex);
            var scene = _engine.GetCurrentScene();
            SceneText = scene.Text;
            Choices = scene.Choices.Select(c => c.Text).ToList();
            Affection = _engine.GetAffection();
            return new JsonResult(new { SceneText, Choices, Affection });

        }
    }
}


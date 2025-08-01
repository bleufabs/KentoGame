using System.Collections.Generic;
using System.Linq;

namespace KentoGame.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public int AffectionChange { get; set; }
        public string Response { get; set; } = "";
    }

    public class Scene
    {
        public string Text { get; set; } = "";
        public List<Choice> Choices { get; set; } = new();
    }

    public class GameEngine
    {
        private readonly GameState _state;

        public GameEngine(GameState state)
        {
            _state = state;
        }

        public Scene GetCurrentScene()
        {
            switch (_state.CurrentScene)
            {
                case 1:
                    return new Scene
                    {
                        Text = "<em>Rain clings to the overhang as you wait outside..." +
                               " Kento approaches, coat damp...</em><br><strong>Kento:</strong> \"You called me out here… this better not be a waste of time.\"",
                        Choices = new List<Choice>
                        {
                            new Choice { Id = 1, Text = "I wanted to see you — that’s all.", AffectionChange = 2, Response = "Kento blinks... \"…Then next time… don’t wait in the rain.\"" },
                            new Choice { Id = 2, Text = "Sorry. Maybe I shouldn’t have bothered you.", AffectionChange = -1, Response = "He exhales... \"Don’t make this weird.\"" },
                            new Choice { Id = 3, Text = "You seemed bored earlier. Figured I’d offer a distraction.", AffectionChange = 1, Response = "He tilts his head... \"…Hmph. You’re not wrong.\"" }
                        }
                    };

                case 2:
                    return new Scene
                    {
                        Text = "<em>The scent of coffee clings to the corridor...</em><br><strong>Kento:</strong> \"That… wasn’t terrible. You held your ground.\"",
                        Choices = new List<Choice>
                        {
                            new Choice { Id = 1, Text = "Was that a compliment?", AffectionChange = 1, Response = "He doesn’t answer... \"It was… an observation.\"" },
                            new Choice { Id = 2, Text = "Thanks. I couldn’t have done it without you.", AffectionChange = 2, Response = "He meets your gaze... \"Hm. At least you understand.\"" },
                            new Choice { Id = 3, Text = "I’ve had worse assignments.", AffectionChange = 0, Response = "He nods once... \"It’ll serve you.\"" }
                        }
                    };

                default:
                    return new Scene { Text = "<em>Scene coming soon...</em>", Choices = new List<Choice>() };
            }
        }

        public void ApplyChoice(int choiceId)
        {
            var scene = GetCurrentScene();
            var choice = scene.Choices.FirstOrDefault(c => c.Id == choiceId);
            if (choice != null)
            {
                _state.Affection += choice.AffectionChange;
                _state.LastResponse = choice.Response;
            }
        }

        public string GetResponseText()
        {
            return _state.LastResponse ?? "";
        }

        public void MoveToNextScene()
        {
            _state.CurrentScene++;
            _state.LastResponse = null;
        }

        public int GetAffection() => _state.Affection;
    }
}


namespace KentoGame.Models
{
    public class GameState
    {
        public int Affection { get; set; } = 0;
        public int CurrentScene { get; set; } = 1;
        public string? LastResponse { get; set; }
    }
}

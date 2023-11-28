namespace tic_tac_toe_api.Models
{
    public class Game
    {
        public int Id { get; set; }
        public List<MoveData> MovesData { get; set; }
    }
}

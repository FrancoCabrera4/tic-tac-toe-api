namespace tic_tac_toe_api.Models
{
    public class MoveData
    {
        public int Id { get; set; }
        public int SquareSelected { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}

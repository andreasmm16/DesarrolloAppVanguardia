namespace ChessRules.Core.Models
{
    public class ChessMove
    {
        public string Piece { get; set; }
        public string InColumn { get; set; }
        public int InRow { get; set; }
        public string ToColumn { get; set; }
        public int ToRow { get; set; }
    }
}

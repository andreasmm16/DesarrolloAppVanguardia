using ChessRules.Core.Models;

namespace ChessRules.Core.Interfaces
{
    public interface IChessboard
    {
        int[,] FillBoard(ChessMove[] pieces);
        int ColumnToNumber(string column);
    }
}

using ChessRules.Core.Models;

namespace ChessRules.Core.Interfaces
{
    public interface IMovementsRule
    {
        void validateMovements(ChessMove move, ILogger logger, IChessboard chessboard, int[,] board);
        bool isMatch(string pieceName);
    }
}

using ChessRules.Core.Interfaces;
using ChessRules.Core.Models;

namespace ChessRules.Core.Rules
{
    public class RookRule : IMovementsRule
    {
        public bool isMatch(string pieceName) => pieceName == "RW" || pieceName == "RB";

        public void validateMovements(ChessMove move, ILogger logger, IChessboard chessboardConvert, int[,] board)
        {

            if (board[8-move.ToRow, chessboardConvert.ColumnToNumber(move.ToColumn)] == 1)
            {
                logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is not valid, there is a piece in place");
                return;
            }
            if (move.InRow != move.ToRow && move.InColumn != move.ToColumn)
            {
                logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is not valid, the rook can only move in a straight line");
                return;
            }
            logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is valid");
        }
    }
}

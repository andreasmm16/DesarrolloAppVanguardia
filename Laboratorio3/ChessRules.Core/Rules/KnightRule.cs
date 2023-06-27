using ChessRules.Core.Interfaces;
using ChessRules.Core.Models;

namespace ChessRules.Core.Rules
{
    public class KnightRule : IMovementsRule
    {
        public bool isMatch(string pieceName) => pieceName == "KW" || pieceName == "KB";

        public void validateMovements(ChessMove move, ILogger logger, IChessboard chessboardConvert, int[,] board)
        {
            if (board[8 - move.ToRow, chessboardConvert.ColumnToNumber(move.ToColumn)] == 1)
            {
                logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is not valid, there is a piece in place");
                return;
            }

            if (move.InRow == move.ToRow || move.InColumn == move.ToColumn)
            {
                 logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is not valid, the knight can only move in L");
                return;
            }

            if(move.ToRow == (move.InRow-2) && chessboardConvert.ColumnToNumber(move.InColumn) == (chessboardConvert.ColumnToNumber(move.ToColumn) - 1))
            {
                logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is valid");
                return;
            }
            if (move.ToRow == (move.InRow - 1) && chessboardConvert.ColumnToNumber(move.InColumn) == (chessboardConvert.ColumnToNumber(move.ToColumn) - 2))
            {
                logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is valid");
                return;
            }
            logger.Log($"Movement {move.Piece} to {move.ToColumn}{move.ToRow} is not valid, the knight can only move in L");
        }
    }
}

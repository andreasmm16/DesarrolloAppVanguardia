using ChessRules.Core.Interfaces;
using ChessRules.Core.Models;

namespace ChessRules.Core
{
    public class ChessBoard:IChessboard
    {
        public int[,] FillBoard(ChessMove[] pieces)
        {
            int[,] board = new int[8, 8];
            for (int i = 0; i < pieces.Length; i++)
            {
                board[8 - pieces[i].InRow, ColumnToNumber(pieces[i].InColumn)] = 1;
            }
           return board;
        }

        public int ColumnToNumber(string column)
        {
            switch (column)
            {
                case "A": return 0;
                case "B": return 1;
                case "C": return 2;
                case "D": return 3;
                case "E": return 4;
                case "F": return 5;
                case "G": return 6;
                case "H": return 7;
                default: throw new Exception("Invalid column");
            }
        }
    }
}

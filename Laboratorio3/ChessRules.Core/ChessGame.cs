using ChessRules.Core.Interfaces;
using ChessRules.Core.Models;
using ChessRules.Core.Rules;

namespace ChessRules.Core
{
    public class ChessGame
    {
        private readonly ILogger logger;
        private readonly IMovementsReader reader;
        private readonly IMovementsDeserializer deserializer;
        private readonly IChessboard chessboard;

        public ChessGame(ILogger logger, IMovementsReader reader, IMovementsDeserializer deserializer, IChessboard chessboard)
        {
            this.logger = logger;
            this.reader = reader;
            this.deserializer = deserializer;
            this.chessboard = chessboard;
        }

        public void validateMovements()
        {
            string movementsJson = reader.ReadMovements();  
            var movements = deserializer.Deserialize(movementsJson);
            int[,] board = chessboard.FillBoard(movements);
            var builder = new MovementsRulesEngine.Builder();
            var engine = builder.PawnRule().RookRule().KnightRule().Build();
            foreach (var movement in movements)
            {
                engine.ApplyRules(movement, logger, chessboard, board);
            }
        }
    }
}

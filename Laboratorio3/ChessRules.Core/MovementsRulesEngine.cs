using ChessRules.Core.Interfaces;
using ChessRules.Core.Models;
using ChessRules.Core.Rules;

namespace ChessRules.Core
{
    public class MovementsRulesEngine
    {
       private readonly IEnumerable<IMovementsRule> movementsRules = new List<IMovementsRule>();

        public MovementsRulesEngine(IEnumerable<IMovementsRule> rules)
        {
            movementsRules= rules;
        }

        public void ApplyRules(ChessMove move, ILogger logger, IChessboard chessboardConvert, int[,] board)
        {
            foreach (var rule in movementsRules)
            {
                if (rule.isMatch(move.Piece))
                {
                    rule.validateMovements(move, logger, chessboardConvert, board);
                }
            }
        }

        public class Builder
        {
            private readonly List<IMovementsRule> rules = new List<IMovementsRule>();

            public Builder RookRule()
            {
                rules.Add(new RookRule());
                return this;
            }

            public Builder PawnRule()
            {
                rules.Add(new PawnRule());
                return this;
            }

            public Builder KnightRule()
            {
                rules.Add(new KnightRule());
                return this;
            }
            public MovementsRulesEngine Build()
            {
                return new MovementsRulesEngine(rules);
            }
        }
    }
}



using ChessRules.Infrastructure.Serializers;
using ChessRules.Infrastructure.Readers;
using ChessRules.Core;
using ChessRules.Infrastructure.Loggers;

internal class Program
{
    //Design patterns used:
    //1. Rules Engine Pattern
    //2. Builder Pattern
    private static void Main(string[] args)
    {
        var chessGame = new ChessGame(new ConsoleLogger(), new JsonFileReader(), new JsonMovementsDeserializer(), new ChessBoard());
        chessGame.validateMovements();
    }
}
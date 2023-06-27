using ChessRules.Core.Models;

namespace ChessRules.Core.Interfaces
{
    public interface IMovementsDeserializer
    {
        ChessMove[] Deserialize(string source); 
    }
}

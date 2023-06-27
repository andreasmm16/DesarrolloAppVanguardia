using ChessRules.Core.Interfaces;
using ChessRules.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ChessRules.Infrastructure.Serializers
{
    public class JsonMovementsDeserializer : IMovementsDeserializer
    {
        public ChessMove[]? Deserialize(string source) => JsonConvert.DeserializeObject<ChessMove[]>(source, new StringEnumConverter());

    }
}

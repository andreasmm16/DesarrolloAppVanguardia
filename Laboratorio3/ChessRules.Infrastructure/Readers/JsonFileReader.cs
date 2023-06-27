using ChessRules.Core.Interfaces;

namespace ChessRules.Infrastructure.Readers
{
    public class JsonFileReader : IMovementsReader
    {
        public string ReadMovements()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string solutionDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
            string jsonFilePath = Path.Combine(solutionDirectory, "movements.json");
            return File.ReadAllText(jsonFilePath);
        }
    }
}

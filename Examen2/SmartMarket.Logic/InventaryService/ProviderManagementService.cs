using System.Text.Json;
using SmartMarket.Logic.Entities;
using SmartMarket.Logic.Interfaces;

namespace SmartMarket.Logic.InventaryService;

public class ProviderManagementService : IDisposable, IProviderService
{
    private readonly HttpClient _client;
    public ProviderManagementService()
    {
        _client = new HttpClient();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _client.Dispose();
        }
    }

    public async Task<Provider?> GetFromApiByIdAsync(Guid id)
    {
        var response = await _client.GetAsync($"https://localhost:5001/api/providers/{id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var provider = JsonSerializer.Deserialize<Provider>(responseContent);
        return provider;
    }
}
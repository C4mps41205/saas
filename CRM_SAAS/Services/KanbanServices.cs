using System.Net.Http.Json;
using Application.Dto.Request;
using Application.Dto.Response;
using Microsoft.AspNetCore.SignalR.Client;

namespace CRM_SAAS.Services;

public class KanbanServices(HttpClient httpClient) : IKanbanRepository
{
    private HubConnection? _hubConnection;
    public event Action<bool>? OnCardsCreated;
    public event Action<bool>? OnCardsUpdated;
    public event Action<bool>? OnCardDeleted;
    
    #region --Hub

    public async Task InitializeConnectionHubEmployee()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5159/EmployeeHub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<bool>("EmployeeCreated", (e) =>
        {
            OnCardsCreated?.Invoke(e);
        });
        
        _hubConnection.On<bool>("EmployeeCreated", (e) =>
        {
            OnCardsUpdated?.Invoke(e);
        });
        
        _hubConnection.On<bool>("EmployeeDeleted", (client) =>
        {
            OnCardDeleted?.Invoke(client);
        });

        await _hubConnection.StartAsync();
    }

    public async Task DisconnectAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();
        }
    }

    #endregion

    #region --Queries

    public async Task<List<CardResponse>> GetKanban(GetCardRequest request)
    {
        return await httpClient.GetFromJsonAsync<List<CardResponse>>(
                   $"Kanban/GetAllKanban") ??
               new();
    }
    
    public async Task<CardResponse?> GetCardById(Guid id)
    {
        return await httpClient.GetFromJsonAsync<CardResponse>($"Kanban/GetCardById?id={id}");
    }

    #endregion

    #region --Actions

    public async Task<bool> CreateCard(CreateCardKanbanRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("Kanban/CreateCard", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCard(UpdateCardKanbanRequest request, Guid id)
    {
        var content = JsonContent.Create(request);
        var response = await httpClient.PatchAsync($"Kanban/UpdateCard?id={id}", content);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> ChangeStatusCard(ChangeCardStatusRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("Kanban/ChangeStatusCard", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCard(Guid id)
    {
        var response = await httpClient.DeleteAsync($"Kanban/DeleteCard?id={id}");
        return response.IsSuccessStatusCode;
    }

    #endregion
}
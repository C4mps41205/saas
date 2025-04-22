using System.Net.Http.Json;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace CRM_SAAS.Services;

public class ClientsServices(HttpClient httpClient) : IClientsRepository
{
    private HubConnection? _hubConnection;
    public event Action<CreateClientResponse>? OnClientsCreated;
    public event Action<bool>? OnClientDeleted;

    #region --Hub

    public async Task InitializeConnectionHubClient()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5159/ClientsHub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<CreateClientResponse>("ClientCreated", (client) =>
        {
            OnClientsCreated?.Invoke(client);
        });
        
        _hubConnection.On<bool>("ClientDeleted", (client) =>
        {
            OnClientDeleted?.Invoke(client);
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

    public async Task<PaginationDefault<GetClientResponse>> GetPaginatedClients(GetClientRequest request)
    {
        return await httpClient.GetFromJsonAsync<PaginationDefault<GetClientResponse>>(
                   $"Clients/GetPaginatedClients?PageNumber={request.PageNumber}&PageSize={request.PageSize}&Page={request.Page}") ??
               new();
    }

    public async Task<GetClientResponse> GetClientsById(GetClientByIdRequest request)
    {
        return await httpClient.GetFromJsonAsync<GetClientResponse>(
            $"Clients/GetClientById?Id={request.Id}");
    }

    #endregion

    #region --Actions

    public async Task<CreateClientResponse> CreateClients(ClientRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("Clients/CreateClient", request);
        return await response.Content.ReadFromJsonAsync<CreateClientResponse>();
    }

    public async Task<bool> UpdateClients(ClientRequest request, Guid id)
    {
        var content = JsonContent.Create(request);
        var response = await httpClient.PatchAsync($"Clients/UpdateClient?id={id}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<HttpResponseMessage> DeleteClients(Guid Id)
    {
        return await httpClient.DeleteAsync(
            $"Clients/DeleteClient?id={Id}");
    }

    #endregion
}
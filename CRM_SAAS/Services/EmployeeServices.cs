using System.Net.Http.Json;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response;
using Microsoft.AspNetCore.SignalR.Client;

namespace CRM_SAAS.Services;

public class EmployeeServices(HttpClient httpClient) : IEmployeeRepository
{
    private HubConnection? _hubConnection;
    public event Action<CreateEmployeeResponse>? OnEmployeesCreated;
    public event Action<bool>? OnEmployeeDeleted;

    #region --Hub

    public async Task InitializeConnectionHubEmployee()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5159/EmployeeHub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<CreateEmployeeResponse>("EmployeeCreated", (e) =>
        {
            OnEmployeesCreated?.Invoke(e);
        });
        
        _hubConnection.On<bool>("EmployeeDeleted", (client) =>
        {
            OnEmployeeDeleted?.Invoke(client);
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

    public async Task<PaginationDefault<GetEmployeeResponse>> GetPaginatedEmployees(EmployeeRequest request)
    {
        return await httpClient.GetFromJsonAsync<PaginationDefault<GetEmployeeResponse>>(
                   $"Employee/GetPaginatedEmployees?PageNumber={request.PageNumber}&PageSize={request.PageSize}&Page={request.Page}") ??
               new();
    }

    public async Task<bool> CreateEmployees(CreateEmployeeRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("Employee/CreateEmployee", request);
        return response.IsSuccessStatusCode;
    }

    #endregion

    #region --Actions

    public Task<bool> UpdateEmployees(CreateEmployeeRequest request, Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> DeleteEmployees(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<GetEmployeeResponse> GetEmployeesById(GetEmployeeByIdRequest request)
    {
        throw new NotImplementedException();
    }

    #endregion
}
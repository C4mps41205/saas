namespace Application.Repository;

public interface IClientHub
{
    Task OnConnectedAsync();
    Task OnDisconnectedAsync(Exception? exception);
    Task SendMessage(string user, string message);
}
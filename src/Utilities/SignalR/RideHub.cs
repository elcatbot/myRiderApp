namespace myRideApp.Utilities.SignalR;

public class MainHub : Hub
{
    public async Task JoinRideGroup(string itemId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, itemId);
}
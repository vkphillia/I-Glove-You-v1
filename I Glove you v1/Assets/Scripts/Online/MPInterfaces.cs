public interface MPLobbyListener
{
    void SetLobbyStatusMessage(string message);
    void HideLobby();

}

public interface MPUpdateListener
{
    void UpdateReceived(string participantId, float posX, float posY, float rotZ);
}

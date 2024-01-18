namespace Mitsyii_Services.Helpers;

public record SChatBotMessage
{
    public ETwitchIRCCommands Command;
    public string Message;
    public string User;
    public string Channel;

    public SChatBotMessage()
    {
        Message = string.Empty;
        User = string.Empty;
        Channel = string.Empty;
    }
}
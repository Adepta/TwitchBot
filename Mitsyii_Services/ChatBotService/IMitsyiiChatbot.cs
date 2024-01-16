using System.Threading.Tasks;

namespace Mitsyii_Services.ChatBotService;

public interface IMitsyiiChatbot
{
    public Task<bool> Run();
    public Task<bool> Setup(string TwitchChannel);
    public Task<bool> ConnectToTwitchIRChannel(string ChannelName);
    public Task<bool> ConnectBotToTwitchIRC();
}
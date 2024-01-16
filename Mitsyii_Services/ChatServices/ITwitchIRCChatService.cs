using System.Net.Sockets;
using System.Threading.Tasks;

namespace Mitsyii_Services;

// Used for Initializing the Connection Between Twitch Chat IRC and the Bot.
public interface ITwitchIRCChatService
{
    public Task<bool> Start();
    public TcpClient GetTwitchChatConnector();
}
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Mitsyii_Services.Parsers;

namespace Mitsyii_Services;

// Use for Connecting to the IRC Bot
public class TwitchIRCChatService : ITwitchIRCChatService

{
    private string _IpAddress { get; set; } = "irc.chat.twitch.tv";
    private int _Port { get; set; } = 6667;


    private TcpClient _TwitchChatConnector { get; set; } = new();

    public async Task<bool> Start()
    {
        try
        {
            await _TwitchChatConnector.ConnectAsync(_IpAddress, _Port);
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error with TwitchIRC Chat Service", ex);
        }
    }


    public TcpClient GetTwitchChatConnector()
    {
        if (_TwitchChatConnector != null)
        {
            return _TwitchChatConnector;
        }
        else
        {
            throw new ApplicationException("TCP Client Not Setup");
        }
    }
}
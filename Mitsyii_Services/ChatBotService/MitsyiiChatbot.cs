using System;
using System.IO;
using System.Threading.Tasks;
using Mitsyii_Services.Parsers;

namespace Mitsyii_Services.ChatBotService;

public class MitsyiiChatbot : IMitsyiiChatbot
{
    private ITwitchIRCChatService _twitchIrcChatService { get; set; } = new TwitchIRCChatService();
    private StreamReader _twitchChatReader { get; set; }
    private StreamWriter _twitchChatWriter { get; set; }
    
    private IChatParser _twitchChatParser { get; set; } = new TwitchChatParser();

    public async Task<bool> Setup(string TwitchChannel)
    {
        try
        {
            // Start the TCP Client.
            await _twitchIrcChatService.Start();
            // Create The Stream Reader and Writers.
            _twitchChatReader = new StreamReader(_twitchIrcChatService.GetTwitchChatConnector().GetStream());
            _twitchChatWriter = new StreamWriter(_twitchIrcChatService.GetTwitchChatConnector().GetStream())
                { NewLine = "\r\n", AutoFlush = true };
            
            
            await ConnectBotToTwitchIRC();
            await ConnectToTwitchIRChannel(TwitchChannel);
            
            
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Unable to Setup Bot", ex);
        }
    }

    public async Task<bool>  ConnectBotToTwitchIRC()
    {
        if (_twitchChatReader == null || _twitchChatWriter == null)
        {
            throw new ApplicationException("Unable to Connect to Twitch IRC, StreamReader/Writer Not Setup");
        }
        // Todo - Move to Settings Service to Pull In From Config File
        await _twitchChatWriter.WriteLineAsync($"PASS oauth:bvbdt3yu18kw2bb4ecravwnzzjssev");
        await _twitchChatWriter.WriteLineAsync($"NICK Fahey_Bot");
        Console.WriteLine($"Connecting as Mitsyii Bot");
        return true;

    }

    public async Task<bool> ConnectToTwitchIRChannel(string ChannelName)
    {
        try
        {
            await _twitchChatWriter.WriteLineAsync($"JOIN #{ChannelName}");
            Console.WriteLine($"Joining {ChannelName}");

            await _twitchChatWriter.WriteLineAsync($"CAP REQ :twitch.tv/commands twitch.tv/membership");
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Unable to Connect to Twitch IRC Channel {ChannelName}", ex);
        }
    } 
    
    public async Task<bool> Run()
    {
        while (true)
        {
            
            string line = await _twitchChatReader.ReadLineAsync();
            Console.WriteLine(line);
            var command = await _twitchChatParser.ParseChatMessage(line);

            
            Console.WriteLine($"{command.Command} - {command.User} - {command.Channel} - {command.Message}");
        }

        return true;
    }
}
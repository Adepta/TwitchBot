using System;
using System.Threading.Tasks;

namespace Mitsyii_Services.Parsers;

public class TwitchChatParser : IChatParser
{
    /// <summary>
    /// Implementation to Parse the Chat Message From Twitch In To Something More Manageable
    /// Does not work with "Tags" Enabled. 
    /// </summary>
    /// <param name="chatMessage"></param>
    /// <returns></returns>
    public async Task<ChatBotMessage> ParseChatMessage(string chatMessage)
    {
        // We need to Parse The Message From Chat. 

        var message = new ChatBotMessage();


        // We Need to work out the differance between a message with an @ and a message without one.

        // Does the String Contain a @ Symbol?
        var split = chatMessage.Split((" "));

        if (!chatMessage.Contains("@"))
        {
            if (split[0].ToUpperInvariant() == "PING")
            {
                return new ChatBotMessage()
                {
                    Command = "PONG",
                    Message = split[1],
                    Channel = "Twitch",
                    User = "Twitch"
                };
            }
            else
            {
                return new ChatBotMessage()
                {
                    Command = "Unknown",
                    Message = chatMessage,
                    Channel = "Twitch",
                    User = "Twitch"
                };
            }
        }
        else
        {

            var username = split[0].Split("!")[0];
            switch (split[1].ToUpperInvariant())
            {
                case "JOIN":
                {
                    return new ChatBotMessage()
                    {
                        Command = "JOIN",
                        Message = "Join Command Sent",
                        Channel = split[2],
                        User = username
                    };
                    break;
                }

                case "PRIVMSG":
                {
                    return new ChatBotMessage()
                    {
                        Command = "PRIVMSG",
                        Message = split[3],
                        Channel = split[2],
                        User = username
                    };
                    break;
                }

                default:
                    Console.WriteLine($"Unable to Parse Message - {chatMessage}");
                    return new ChatBotMessage()
                    {
                        Command = "Unknown",
                        Message = chatMessage,
                        Channel = "Twitch",
                        User = "Twitch"
                    };
                    break;
            }

            ;
        }
    }
}

public record struct ChatBotMessage
{
    public string Command;
    public string Message;
    public string User;
    public string Channel;

    public ChatBotMessage()
    {
        Command = string.Empty;
        Message = string.Empty;
        User = string.Empty;
        Channel = string.Empty;
    }
}
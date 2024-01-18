using System;
using System.Threading.Tasks;
using Mitsyii_Services.Helpers;

namespace Mitsyii_Services.Parsers;

public class TwitchChatParser : IChatParser
{
    /// <summary>
    /// Implementation to Parse the Chat Message From Twitch In To Something More Manageable
    /// Does not work with "Tags" Enabled. 
    /// </summary>
    /// <param name="chatMessage"></param>
    /// <returns></returns>
    public async Task<SChatBotMessage> ParseChatMessage(string chatMessage)
    {


        // We Need to work out the differance between a message with an @ and a message without one.

        // Does the String Contain a @ Symbol?
        var split = chatMessage.Split((" "));

        if (!chatMessage.Contains("@"))
        {
            if (split[0].ToUpperInvariant() == "PING")
            {
                return new SChatBotMessage()
                {
                    Command = ETwitchIRCCommands.PONG,
                    Message = split[1],
                    Channel = "Twitch",
                    User = "Twitch"
                };
            }
            else
            {
                return new SChatBotMessage()
                {
                    Command = ETwitchIRCCommands.UNKNOWN,
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
                    return new SChatBotMessage()
                    {
                        Command = ETwitchIRCCommands.JOIN,
                        Message = "Join Command Sent",
                        Channel = split[2],
                        User = username
                    };
                    break;
                }

                case "PRIVMSG":
                {
                    return new SChatBotMessage()
                    {
                        Command = ETwitchIRCCommands.PRIVMSG,
                        Message = split[3],
                        Channel = split[2],
                        User = username
                    };
                    break;
                }

                default:
                    Console.WriteLine($"Unable to Parse Message - {chatMessage}");
                    return new SChatBotMessage()
                    {
                        Command = ETwitchIRCCommands.UNKNOWN,
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

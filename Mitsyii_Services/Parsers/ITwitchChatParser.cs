using System.Threading.Tasks;
using Mitsyii_Services.Helpers;

namespace Mitsyii_Services.Parsers;

// Interface for Any Chat Parser

public interface IChatParser
{
    public Task<SChatBotMessage> ParseChatMessage(string chatMessage);
    
}
using System.Threading.Tasks;

namespace Mitsyii_Services.Parsers;

// Interface for Any Chat Parser

public interface IChatParser
{
    public Task<ChatBotMessage> ParseChatMessage(string chatMessage);
    
}
// See https://aka.ms/new-console-template for more information

using System;
using Mitsyii_Services.ChatBotService;

IMitsyiiChatbot ChatBot = new MitsyiiChatbot();

try
{
    await ChatBot.Setup("cohhcarnage");
   
    while (true)
    {
        await ChatBot.Run();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.ReadLine();
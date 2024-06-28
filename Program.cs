using DisCatSharp;
using DisCatSharp.Enums;
using NuGet.Common;

namespace Androbot
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration(){
            Token = File.ReadAllText("token.txt"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContent
            });

            discord.MessageCreated += async (s, e) => {
                if (e.Message.Content.ToLower().StartsWith("boop"))
                    await e.Message.RespondAsync("beep!");
            };
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}

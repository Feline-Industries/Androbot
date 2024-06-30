using DisCatSharp;
using DisCatSharp.Entities;
using DisCatSharp.Entities.Core;
using DisCatSharp.Enums;
using DisCatSharp.EventArgs;
using Microsoft.Extensions.Logging;


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
            DiscordClient discord = new DiscordClient(new DiscordConfiguration(){
            Token = File.ReadAllText("token.txt"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContent,
            MinimumLogLevel = LogLevel.Debug,
            LogTimestampFormat = "MMM dd yyyy -- hh:mm:ss tt",
            });

            discord.MessageCreated += MessageCreatedHandler;
            discord.MessageDeleted += MessageDeletedHandler;

            //this assess created message
            async Task MessageCreatedHandler(DiscordClient s, MessageCreateEventArgs e)
            {
                if (e.Message.Content.ToLower().StartsWith("boop"))
                    await e.Message.RespondAsync("beep!");
            }

            //this asses deleted messages
            async Task MessageDeletedHandler(DiscordClient s, MessageDeleteEventArgs e){
                if (e.Message.Content.ToLower().StartsWith("yo")){
                    await e.Message.RespondAsync("hahaha");
                }
            }
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}

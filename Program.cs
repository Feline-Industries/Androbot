using DisCatSharp;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using DisCatSharp.EventArgs;
using Microsoft.Extensions.Logging;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;

namespace Androbot
{
    internal class Program
    {
        public class GuildCommands : ApplicationCommandsModule
        {
            [SlashCommand("Talk","a kind response")]
            public static async Task Talk(InteractionContext ctx)
            {
               await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
               new DiscordInteractionResponseBuilder(new DiscordMessageBuilder()
               .WithContent("UwU")));
            }
        }
        
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static DiscordClient DiscordConfig(string tokenLocation){
            DiscordClient discord = new DiscordClient(new DiscordConfiguration(){
            Token = File.ReadAllText(tokenLocation),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContent,
            MinimumLogLevel = LogLevel.Debug,
            LogTimestampFormat = "MMM dd yyyy -- hh:mm:ss tt",
            });
            return discord;
        }

        static async Task MainAsync()
        {
            DiscordClient discord = DiscordConfig("token.txt");

            discord.MessageCreated += MessageCreatedHandler;
            discord.MessageDeleted += MessageDeletedHandler;

            //this assess created message
            async Task MessageCreatedHandler(DiscordClient s, MessageCreateEventArgs e)
            {
                DiscordAttachment firstAttach = e.Message.Attachments.First();

                //note --> fix this to respond with beep!
                if (e.Message.Content.ToLower().StartsWith("boop"))
                    await e.Message.RespondAsync("beep!");

                if(firstAttach.Flags == AttachmentFlags.Spoiler){
                    await e.Message.RespondAsync($"{e.
                    Message.Author.GlobalName} spoilered");
                };
            }

            //this asses deleted messages
            async Task MessageDeletedHandler(DiscordClient s, MessageDeleteEventArgs e){
                if (e.Message.Content.ToLower().StartsWith("yo")){
                    await e.Message.RespondAsync("hahaha");
                }
            }

           //a list of commands 
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
        
        
    }
}

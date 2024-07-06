using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using AndroCommands;
using System.Reflection;


namespace Androbot
{
    internal class Program
    {
        private static DiscordSocketClient? client;
        
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static DiscordSocketClient DiscordConfig(){
            var client = new DiscordSocketClient(new DiscordSocketConfig{
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.All
            });
            return client;
        }

        static async Task MainAsync()
        {
            var token = File.ReadAllText("token.txt");
            client = DiscordConfig();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            var interactionservice = new InteractionService(client.Rest,new InteractionServiceConfig{
                LogLevel = LogSeverity.Error
            });
            
            await interactionservice.RegisterCommandsGloballyAsync();
            
            client.MessageReceived += MessageCreatedHandler;
            client.MessageDeleted += MessageDeletedHandler;
            //setup commands

            //this assess created message
            async Task MessageCreatedHandler(SocketMessage e)
            {
                if (e.Content.ToLower().StartsWith("boop")){
                    await e.Channel.SendMessageAsync("beep!");
                }
                if(e.Attachments.Count > 0){
                    var firstAttach = e.Attachments.First();
                    if(AttachmentExtensions.IsSpoiler(firstAttach)){
                        await e.Channel.SendMessageAsync($"{e.
                        Author.GlobalName} spoilered");
                    };
                }
                
            }

            //this assess deleted messages, only works in guilds not in dms
            async Task MessageDeletedHandler(Cacheable<IMessage, ulong> s, 
            Cacheable<IMessageChannel, ulong> e){
                IMessage delMessage = await s.GetOrDownloadAsync();
                var mesChannel = await e.GetOrDownloadAsync();
                if (s.HasValue == true){
                    if (delMessage.Content.ToLower().StartsWith("yo")){
                        await mesChannel.SendMessageAsync("aint no way");
                    
                    }
                }

            }

           //a list of commands 
            await Task.Delay(-1);
        }
        
    }
}

using Discord;
using Discord.WebSocket;
//using AndroCommands;

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
            client = new DiscordSocketClient(new DiscordSocketConfig{
                MessageCacheSize = 100,
                AlwaysDownloadUsers = true
            });
            return client;
        }

        static async Task MainAsync()
        {
            var token = File.ReadAllText("D:\\Documents\\Code\\Github Repositories\\Androbot\\token.txt");
            DiscordSocketClient client = DiscordConfig();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            

            client.MessageReceived += MessageCreatedHandler;
            client.MessageDeleted += MessageDeletedHandler;
            //setup commands

            //this assess created message
            async Task MessageCreatedHandler(SocketMessage e)
            {
                if (e.Content.ToLower().StartsWith("boop")){
                    await e.Channel.SendMessageAsync("beep!");
                }
                Attachment firstAttach = e.Attachments.First();
                if(AttachmentExtensions.IsSpoiler(firstAttach)){
                    await e.Channel.SendMessageAsync($"{e.
                    Author.GlobalName} spoilered");
                };
            }

            //this assess deleted messages
            async Task MessageDeletedHandler(Cacheable<IMessage, ulong> s, 
            Cacheable<IMessageChannel, ulong> e){
                var delMessage = await s.GetOrDownloadAsync();
                var mesChannel = await e.GetOrDownloadAsync();
                if (delMessage.Content.ToLower().StartsWith("yo")){
                    await mesChannel.SendMessageAsync("aint no way");
                    
                }
            }

           //a list of commands 
            await Task.Delay(-1);
        }
        
        
    }


}

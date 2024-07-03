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
            var client = new DiscordSocketClient(new DiscordSocketConfig{
                MessageCacheSize = 10000
            });
            return client;
        }

        static async Task MainAsync()
        {
            var token = File.ReadAllText("token.txt");
            client = DiscordConfig();
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
                if(e.Attachments.Count > 0){
                    var firstAttach = e.Attachments.First();
                    if(AttachmentExtensions.IsSpoiler(firstAttach)){
                        await e.Channel.SendMessageAsync($"{e.
                        Author.GlobalName} spoilered");
                    };
                }
                
            }

            //this assess deleted messages
            async Task MessageDeletedHandler(Cacheable<IMessage, ulong> s, 
            Cacheable<IMessageChannel, ulong> e){
                IMessage delMessage = await s.DownloadAsync();
                Console.WriteLine(s.HasValue);
                var mesChannel = await e.DownloadAsync();
                //bug occurs with this conditional because s == null
                if (delMessage.Content.ToLower().StartsWith("yo")){
                    await mesChannel.SendMessageAsync("aint no way");
                    
                }
            }

           //a list of commands 
            await Task.Delay(-1);
        }
        
        
    }


}

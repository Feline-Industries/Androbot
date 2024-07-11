using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Program;



namespace Androbot
{
    internal class Program
    {
        private static DiscordSocketClient? client;
        private static CommandService? commands;
        private static CommandHandler? cmdHandler;
        
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

        static CommandService DiscordService(){
            var command = new CommandService(new CommandServiceConfig{
                LogLevel = LogSeverity.Error,
                CaseSensitiveCommands = false
            });
            return command;
        }

        static async Task MainAsync()
        {
            var token = File.ReadAllText("token.txt");
            
            //setup commands
            client = DiscordConfig();
            client.Log += Log;
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            commands = DiscordService();
            cmdHandler = new CommandHandler(client, commands);
            await cmdHandler.InstallCommandsAsync();
            
            client.MessageReceived += MessageCreatedHandler;
            client.MessageDeleted += MessageDeletedHandler;
            

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

                Console.WriteLine($"{e}");
                
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
                Console.Write($"{delMessage}");

            }

            static Task Log(LogMessage msg){
                Console.WriteLine(msg.ToString());
                return Task.CompletedTask;
            }

           //a list of commands 
            await Task.Delay(-1);
        }
        
    }
}

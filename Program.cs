using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Program;
using Microsoft.Extensions.DependencyInjection;
using Discord.Interactions;

namespace Androbot
{
    internal class Program
    {
        //Variables
        private static DiscordSocketClient? client;
        private static CommandService? commands;
        private static CommandHandler? cmdHandler;
        public static IServiceProvider? serviceProvider {get; set;}

        //Config/Creation Commands for later code
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

        static IServiceProvider CreateProvider(){
            var config = new DiscordSocketConfig{
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.All
            };

            var collection =  new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>();

            return collection.BuildServiceProvider();
        }

        //Main Function
        static void Main(string[] args)
        {
            serviceProvider = CreateProvider();
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var token = File.ReadAllText("token.txt");
            
            //setup commands
            client = DiscordConfig();
            client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            var _interactionService = new InteractionService(client.Rest);
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

           //loops
            await Task.Delay(Timeout.Infinite);
        }
        
    }
}

using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace Program{
    public class CommandHandler{
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        //constructor
        public CommandHandler(DiscordSocketClient _client, CommandService _commands){
            client = _client;
            commands = _commands;
        }

        //installs the needed commands
        public async Task InstallCommandsAsync(){
            client.MessageReceived += HandleCommandAsync;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: Androbot.Program.serviceProvider);
        }

        private async Task HandleCommandAsync(SocketMessage mes){
            var message = mes as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || 
            message.HasMentionPrefix(client.CurrentUser, ref argPos)) || message.Author.IsBot)
            return ;

            var context = new SocketCommandContext(client, message);

            await commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null
            );
        }
    }
}

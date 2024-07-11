using Discord.Commands;

namespace Program{
    public class Commands : ModuleBase<SocketCommandContext>
        {
        [Command("talk")]
        [Summary("cute response")]
        public async Task Talk(){
            Console.WriteLine("command used");
            await Context.Channel.SendMessageAsync("UwU");
        }
    }
}

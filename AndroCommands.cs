using Discord.Commands;

namespace AndroCommands{
    public class Commands : ModuleBase<SocketCommandContext>
        {
        [Command("talk")]
        public async Task Talk(){
            await ReplyAsync("UwU");
        }
    }
}

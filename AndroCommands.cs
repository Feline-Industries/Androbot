using System.Reflection;
using Discord;
using Discord.Interactions;

namespace AndroCommands{
    public class Commands : InteractionModuleBase
        {
        [SlashCommand("Talk","a cute reponse")]
        public async Task Talk(){
            await RespondAsync("UwU");
        }
    }
}

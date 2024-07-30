using Discord;
using Discord.Interactions;
namespace Androbot 
{
    public class SlashAndroCommands : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("talk", "a kind responses")]
        public async Task Talk(){
            await RespondAsync("UwU");
        }
    }
}

using DisCatSharp.Entities;
using DisCatSharp.Enums;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;

namespace AndroCommands{
    public class GuildCommands : ApplicationCommandsModule
        {
            [SlashCommand("Talk","a kind response")]
            public static async Task Talk(InteractionContext ctx)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder(new DiscordMessageBuilder()
                {Content = "UwU"}));
            }
        }
}
    
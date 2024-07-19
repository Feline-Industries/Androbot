using Discord.Commands;
using Discord.WebSocket;

namespace Program{
    public class BasicInfo : ModuleBase<SocketCommandContext>
        {
        [Command("talk")]
        [Summary("cute response")]
        public async Task TalkAsync(){
            Console.WriteLine("command used");
            await Context.Channel.SendMessageAsync("UwU");
        }
    }

    //to probe for user's information
    [Group("userinfo")]
    public class UserInfo : ModuleBase<SocketCommandContext>
    {
        [Command("username")]
        [Summary("to pull up a user's username & unique id")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("user id")] SocketUser? user = null){
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}

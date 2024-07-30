using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using Discord;
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

        [Command("help")]
        [Alias("h")]
        [Summary("a basic bg info on AndroBot")]
        public async Task HelpAsync(){
            
            var Author = new EmbedAuthorBuilder()
                .WithName("Androbot");
            var introField = new EmbedFieldBuilder()
                .WithName("Basics: \n")
                .WithValue("add a ! before each command");
            var commandsField = new EmbedFieldBuilder()
                .WithName("Commands: \n")
                .WithValue("`talk`,`help`,`userinfo`");
            
            var embed = new EmbedBuilder()
                .WithAuthor(Author)
                .AddField(introField)
                .AddField(commandsField)
                .WithColor(new Color(0xb997db));

            await ReplyAsync(embed: embed.Build());
        }
    }

    //to probe for user's information
    [Group("userinfo")]
    public class UserInfo : ModuleBase<SocketCommandContext>
    {
        [Command("")]
        [Summary("userinfo help")]
        public async Task UserInfoHelp(){
            var commandsField = new EmbedFieldBuilder()
                .WithName("Commands: \n")
                .WithValue("`username`,`presence`");
            
            var embed = new EmbedBuilder()
                .AddField(commandsField);

            await ReplyAsync(embed: embed.Build());
        }

        [Command("username")]
        [Summary("to pull up a user's username & unique id")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("user id")] SocketUser? user = null){
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }


        //pulls up all of the user's activitys
        [Command("presence")]
        [Summary("gets a user's current presence")]
        [Alias("pres", "activity")]
        public async Task UserPresenceAsync([Summary("user id")] SocketUser? user = null){
            var userInfo = user ?? Context.Client.CurrentUser;
            int trackFor = 1;
            var embedDetails = new EmbedBuilder();
            foreach (var activity in userInfo.Activities){
                if (activity.Type == ActivityType.Listening){
                    embedDetails.AddField("Listening To: ", activity.Details.ToString());
                } else if (activity.Type == ActivityType.CustomStatus){
                    embedDetails.AddField("CustomStatus: ", userInfo.Status);
                }
                await ReplyAsync($"Activity: {trackFor}\n{userInfo.Username} is currently using" +  
                    $" **{activity.Name}**", embed: embedDetails.Build());
                
                trackFor++;
            }
        }

        //Pulls up the activity id
        [Command("presence+")]
        [Summary("gets a user's current presence")]
        [Alias("pres+", "activity+")]
        public async Task UserPresenceAsync([Summary("user id")] SocketUser? user = null, [Summary("activity id")] int? activityId = null){
            var userInfo = user ?? Context.Client.CurrentUser;
            int trackFor = 1;
            foreach (var activity in userInfo.Activities){
                var embedDetails = new EmbedBuilder();
                if (trackFor == activityId){
                    if (activity.Type == ActivityType.Listening){
                        embedDetails.AddField("Listening To: ", activity.Details.ToString());
                    } else if (activity.Type == ActivityType.CustomStatus){
                        embedDetails.AddField("CustomStatus: ", userInfo.Status);
                    }
                    await ReplyAsync($"Activity: {trackFor}\n{userInfo.Username} is currently using" +  
                        $" **{activity.Name}**", embed: embedDetails.Build());  
                }
                trackFor++;
            }
        }
    }
}

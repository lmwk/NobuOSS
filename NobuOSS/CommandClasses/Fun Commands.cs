using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace Nobu
{
    public class Fun_Commands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Respongs with ")]
        public async Task Ping(CommandContext ctx)
        {
            string ping = ctx.Client.Ping.ToString();

            await ctx.Channel.SendMessageAsync("The ping for this server is "+ping+"." ).ConfigureAwait(false);
        }

        [Command("add")]
        [Description("Adds two numbers")]
        public async Task Add(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel.SendMessageAsync(Convert.ToString(numberOne + numberTwo)).ConfigureAwait(false);
        }

        [Command("Gamer")]
        public async Task Gamer(CommandContext ctx)
        {
            Random rnd = new Random();
            int rndint = rnd.Next(0, 2);

            switch (rndint)
            {
                case 1:
                    await ctx.Channel.SendMessageAsync("Bruh moment").ConfigureAwait(false);
                    break;
                case 2:
                    await ctx.Channel.SendMessageAsync("Why the fuck you lyin").ConfigureAwait(false);
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("Death is iminant to all, you can't cheat death").ConfigureAwait(false);
                    break;
            }

        }

        [Command("responsemessage")]
        public async Task Responsemessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("responseemoji")]
        [Cooldown(3, 3, CooldownBucketType.User)]
        public async Task Responseemoji(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            if(message.Result.Content == DiscordEmoji.FromName(ctx.Client, ":hot_face:"))
            {
                await ctx.Channel.SendMessageAsync("Bro what was that emoji");
                await ctx.Channel.SendMessageAsync("like tf");
                await ctx.Channel.SendMessageAsync("I know im hot but damn");
            }
            else
            {
                await ctx.Channel.SendMessageAsync(message.Result.Content);
            }
            
        }

        [Command("responsereaction")]
        public async Task Responsereaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel && x.User == ctx.User).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }
    }
}

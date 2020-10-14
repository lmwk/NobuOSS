using System;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Net.Serialization;
using Reddit;
using RestSharp.Extensions;

namespace Nobu.RedditClasses
{
    public class RedditCommands : BaseCommandModule
    {
        [Command("dankmeme")]
        [RequireNsfw]

        public async Task dankmeme(CommandContext ctx)
        {
            //Link of a tutorial in the github repo
            var r = new RedditClient("PlaceHolder App ID", "PlaceHolder Refresh token", "PlaceHolder App Secret");

            var dankmemes = r.Subreddit("dankmemes").About();

            var rnd = new Random();

            var toppost = dankmemes.Posts.Hot[rnd.Next(0, 100)];

            var post = toppost.Listing;

            var ratio = toppost.UpvoteRatio.ToString();

            var author = toppost.Author;

            await ctx.Channel.SendMessageAsync("This post has an upvote ratio of " + ratio + " And was created by "+ author).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(post.URL).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("Do you like this post? (Y/N)").ConfigureAwait(false);

            Thread.Sleep(20);

            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author == ctx.User).ConfigureAwait(false);

            if (message.Result.Content == "Y")
            {
                await toppost.UpvoteAsync().ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Upvoting...").ConfigureAwait(false);
            }
            else if (message.Result.Content == "N")
            {
                await toppost.DownvoteAsync().ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Downvoting...").ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Entered incorrect response, I wont do anything b-b-b-b-baka!").ConfigureAwait(false);
            }
        }

        [Command("meme")]

        public async Task Meme(CommandContext ctx)
        {
            var r = new RedditClient("PlaceHolder App ID", "PlaceHolder Refresh token", "PlaceHolder App Secret");

            var dankmemes = r.Subreddit("dankmemes").About();

            var rnd = new Random();

            var toppost = dankmemes.Posts.Hot[rnd.Next(0, 100)];

            var post = toppost.Listing;

            var ratio = toppost.UpvoteRatio.ToString();

            var author = toppost.Author;

            await ctx.Channel.SendMessageAsync("This post has an upvote ratio of " + ratio+ " and the author is" +author).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(post.URL).ConfigureAwait(false);

            if(toppost.IsUpvoted == false && toppost.IsDownvoted == false)
            {
                await ctx.Channel.SendMessageAsync("Do you like this post? (Y/N)").ConfigureAwait(false);

                Thread.Sleep(20);

                var interactivity = ctx.Client.GetInteractivity();

                var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author == ctx.User).ConfigureAwait(false);

                if (message.Result.Content == "Y")
                {
                    await toppost.UpvoteAsync().ConfigureAwait(false);
                    await ctx.Channel.SendMessageAsync("Upvoting...").ConfigureAwait(false);
                }
                else if (message.Result.Content == "N")
                {
                    await toppost.DownvoteAsync().ConfigureAwait(false);
                    await ctx.Channel.SendMessageAsync("Downvoting...").ConfigureAwait(false);
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Entered incorrect response, I wont do anything b-b-b-b-baka!").ConfigureAwait(false);
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Hey "+ctx.User.Mention+ ", This post is already voted, No need to vote :)").ConfigureAwait(false);
            }  
        }
    }  
}

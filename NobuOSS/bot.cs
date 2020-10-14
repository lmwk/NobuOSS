using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nobu;
using Nobu.RedditClasses;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NobuOSS
{
    class bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Command { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                
            };

            Client = new DiscordClient(config);

            Client.Ready += OnclientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true,

            };

            Command = Client.UseCommandsNext(commandsConfig);

            Command.RegisterCommands<Fun_Commands>();
            Command.RegisterCommands<RedditCommands>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
        private Task OnclientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}

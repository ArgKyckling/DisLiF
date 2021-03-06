﻿using System;
using Discord;
using Discord.Modules;
using Discord.Commands.Permissions.Levels;
using System.Reflection;

namespace DisLiF.Modules {
    internal partial class DinMammaModule : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager) {
            _manager = manager;
            _client = manager.Client;

            manager.CreateCommands(String.Empty, group => {
                group.CreateCommand("leave")
                    .Description("Sternly tells the bot to leave the server. Requires Manage Server permission.")
                    .Do(async e => {
                        //e.Channel.SendIsTyping();
                        if (e.User.ServerPermissions.ManageServer) {
                            await _client.Reply(e, "Leaving. ;_;");
                            await e.Server.Leave();
                        } else {
                            await e.User.SendMessage("You don't have sufficent permissions for this command.");
                        }
                    });
                group.CreateCommand("dinmamma")
                    .Description("Gissa tre gånger.")
                    .Do(async e => {
                        //e.Channel.SendIsTyping();
                        await e.Channel.SendMessage(DinMammaJoke());
                    });
                group.CreateCommand("bork")
                    .Description("Bork bork bork.")
                    .Do(async e => {
                        //e.Channel.SendIsTyping();
                        await e.Channel.SendMessage("GIB BORK! BORK STRONK!");
                    });
                group.CreateCommand("debug")
                    .Description("Prints versions of used libraries.")
                    .Do(async e => {
                        e.Channel.SendIsTyping();
                        Assembly BungieSharp = Assembly.GetAssembly(typeof(BungieSharp.BungieClient));
                        Assembly OverwatchSharp = Assembly.GetAssembly(typeof(OverwatchSharp.OverwatchClient));

                        string response = $"BungieSharp: {BungieSharp.GetName().Version} \nOverwatchSharp: {OverwatchSharp.GetName().Version}";
                        await e.Channel.SendMessage(response);
                    });
                group.CreateCommand("slowclap")
                    .Description("Great. Just great. That was really, really great.")
                    .Do(async e => {
                        //e.Channel.SendIsTyping();
                        await e.Channel.SendMessage("http://i.imgur.com/BOK1lew.gif");
                    });
                group.CreateCommand("magnumdong")
                    .Do(async e => {
                        //e.Channel.SendIsTyping();
                        await e.Channel.SendMessage("https://www.youtube.com/watch?v=RH5EPDkmyFw");
                    });
                group.CreateCommand("copypasta")
                    .Alias("copypaste", "pasta", "spamerino")
                    .Description("Get some fresh copypasta!")
                    .Do(async e => {
                        //e.Channel.SendIsTyping();
                        await e.Channel.SendMessage(Copypasta());
                    });
                    
                if (!String.IsNullOrEmpty(GlobalSettings.Discord.ClientId)) {
                    group.CreateCommand("addtoserverlink")
                        .Description("Returns a link for adding the bot to another server.")
                        .Do(async e => {
                            //e.Channel.SendIsTyping();
                            await _client.Reply(e, $"https://discordapp.com/oauth2/authorize?&client_id={GlobalSettings.Discord.ClientId}&scope=bot&permissions=0");
                        });
                }
            });
        }

        private static readonly string[] _jokes = {
            "Din mamma är så fet att hon tar på sitt bälte med en boomerang.",
            "Din mamma är så fet att när hon har en gul regnrock på sig, så ropar folk taxi efter henne.",
            "Din mamma är så fet att hon betalar skatt i tre länder.",
            "Din mamma är så fet att hon har ett eget postnummer.",
            "Din mamma är så fet att de hittade spår av blod i hennes Nutella-system.",
            "Din mamma är så fet att inte ens Bill Gates har råd att ge henne en fettsugning.",
            "Din mamma är så fet att det tog slut på mörk materia i världen."
        };
        private static Random _rand = new Random();
        private string DinMammaJoke() {
            return _jokes[_rand.Next(0, _jokes.Length)];
        }
        /// <summary>
        /// Pulls a copypasta from <see cref="_copypasta"/>
        /// </summary>
        private string Copypasta() {
            return _copypasta[_rand.Next(0, _copypasta.Length)];
        }
    }
}

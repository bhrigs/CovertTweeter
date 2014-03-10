﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using CovertTweeter.Core;
using TweetSharp;

namespace CovertTweeter
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) => {
                                          eventArgs.Cancel = true;
                                          exitEvent.Set();
                                      };

            (new TweetMonitor()).Run();

            exitEvent.WaitOne();
        }
    }

    public class TweetMonitor
    {                
        public void Run()
        {
            var lastUpdated = DateTime.Now;
            var repo = new TweetRepository();

            // Get last 10 tweets/mentions/etc

            while (true)
            {
                long lastHomeId = 0;

                foreach (var tweet in repo.GetTweetsFromHomeTimeline())
                {
                    ShowTweet(tweet);
                    lastHomeId = Math.Max(lastHomeId,tweet.Id);
                }

                // set lastupdated to latest received if any               
            }
        }

        private void ShowTweet(TwitterStatus tweet)
        {
            ColorConsole.Write(ConsoleColor.DarkYellow, tweet.Author.ScreenName);
            ColorConsole.WriteLine(ConsoleColor.DarkGray, ": {0}", tweet.Text);
        }
    }
}

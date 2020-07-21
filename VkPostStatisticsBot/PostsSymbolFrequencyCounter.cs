using System;
using System.Collections.Generic;
using System.Linq;

namespace VkPostStatisticsBot
{
    public class PostsSymbolFrequencyCounter : IPostsStatisticsCounter
    {
        private const int DigitsCount = 4;

        private readonly HashSet<char> symbolsToCount;

        public PostsSymbolFrequencyCounter(IEnumerable<char> symbolsToCount)
        {
            this.symbolsToCount = new HashSet<char>(symbolsToCount);
        }

        public Dictionary<string, double> CountStatistics(IEnumerable<string> posts)
        {
            var symbolCounts = new Dictionary<string, double>();
            var totalSymbols = 0;

            foreach (var post in posts)
            {
                foreach (var symbol in post.ToLower())
                {
                    if (!symbolsToCount.Contains(symbol))
                        continue;

                    var symbolKey = symbol.ToString();
                    if (!symbolCounts.ContainsKey(symbolKey))
                        symbolCounts[symbolKey] = 0;
                    symbolCounts[symbolKey]++;
                    totalSymbols++;
                }
            }

            return GetFrequency(symbolCounts, totalSymbols);
        }

        private static Dictionary<string, double> GetFrequency(Dictionary<string, double> symbolCounts,
            int totalSymbols)
        {
            var statistics = new Dictionary<string, double>();

            foreach (var (key, value) in symbolCounts.ToArray())
                statistics[key] = Math.Round(value / totalSymbols, DigitsCount);

            return statistics;
        }
    }
}
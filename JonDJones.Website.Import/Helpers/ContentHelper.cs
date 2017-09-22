using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JonDJones.Fixtures.Helpers
{
    using JonDJones.Fixtures.Fixtures.Factory;
    using JonDJones.Website.Shared.Helpers;

    public class ContentHelper
    {
        private readonly BlockFixturesFactory _blockFixturesFactory;

        public ContentHelper(BlockFixturesFactory blockFixturesFactory)
        {
            Guard.ValidateObject(blockFixturesFactory);
            _blockFixturesFactory = blockFixturesFactory;
        }
        public static string LoremIpsum(
            int minWords,
            int maxWords,
            int minSentences,
            int maxSentences,
            int numParagraphs)
        {
            var loremIpsumWords = new List<string>
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var rand = new Random();
            var numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
            var numWords = rand.Next(maxWords - minWords) + minWords + 1;

            var result = new StringBuilder();

            for (var paragraphs = 0; paragraphs < numParagraphs; paragraphs++)
            {
                result.Append("<p>");

                for (var sentences = 0; sentences < numSentences; sentences++)
                {
                    for (var words = 0; words < numWords; words++)
                    {
                        var randomSentance = string.Join(" ", loremIpsumWords.OrderBy(x => rand.Next()));
                        result.Append(randomSentance);
                    }

                    result.Append(". ");
                }

                result.Append("</p>");
            }

            return result.ToString();
        }

    }
}

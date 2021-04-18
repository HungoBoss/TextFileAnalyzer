using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class TextFileModel
    {
        public string Name { get; }
        public string Text { get; set; }
        public int NumberOfWords { get; set; }
        public int NumberOfSentences { get; set; }
        public string LongestWord { get; set; }
        public string LongestSentence { get; set; }
        public string ShortestWord { get; set; }
        public string ShortestSentence { get; set; }
        public string MostOccurredWord { get; set; }

        public TextFileModel(string name)
        {
            Name = name;
        }

        public void GetNumberOfWords()
        {
            var substrings = Text.Split(' ', '\n');
            NumberOfWords = substrings.Length;
        }

        public void GetNumberOfSentences()
        {
            const string firstPattern = @"[.!?]\s*[A-Z]";   // Matches for sentences followed by next sentence (uppercase char)
            const string secondPattern = @"[.!?]\z";        // Matches for sentences followed by end of string

            var numberOfFirstMatches = Regex.Matches(Text, firstPattern).Count;
            var numberOfSecondMatches = Regex.Matches(Text, secondPattern).Count;

            NumberOfSentences = numberOfFirstMatches + numberOfSecondMatches;
        }

        private static string RemoveSpecificCharacters(string originalText)
        {
            return Regex.Replace(originalText, @"[.!?,()]", string.Empty);
        }

        public void GetMostOccurredWord()
        {
            var modifiedText = RemoveSpecificCharacters(Text);

            var result = modifiedText.Split(' ')
                .Where(x => x.Length > 0)
                .GroupBy(x => x)
                .Select(x => new {Count = x.Count(), Word = x.Key})
                .OrderByDescending(x => x.Count);

            MostOccurredWord = result.First().Word;
        }

        public void GetLongestAndShortestWord()
        {
            var modifiedText = RemoveSpecificCharacters(Text);
            var subs = modifiedText.Split(' ', '\n');

            Array.Sort(subs, (x, y) => x.Length.CompareTo(y.Length));

            ShortestWord = subs[0];
            LongestWord = subs[subs.Length - 1];
        }

        public void GetLongestAndShortestSentence()
        {
            var modifiedText = Regex.Split(Text,@"(?<=[\.!\?])\s+").Where(s => s != string.Empty).ToArray<string>();

            LongestSentence = modifiedText[0];
            ShortestSentence = modifiedText[modifiedText.Length - 1];
        }

    }
}

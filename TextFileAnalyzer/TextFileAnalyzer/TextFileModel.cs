using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class TextFileModel
    {
        public string Name { get; }
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
    }
}

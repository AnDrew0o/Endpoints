using System;
using System.Collections.Generic;

namespace endpoints
{
    public class WordFrequency
    {
        public static Dictionary<string, int> GetDictionary(string text)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();

            foreach (string word in text.ToLower().Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (words.ContainsKey(word))
                {
                    words[word]++;
                }
                else
                {
                    words.Add(word, 1);
                }
            }
            return words;
        }
    }
}
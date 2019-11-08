using System;
using System.Collections.Generic;
using System.Linq;

namespace scrabble_score
{
    public static class ScrabbleScore
    {
        static Dictionary<char, int> values = new Dictionary<string, int>()
            {
                {"aeioulnrst", 1},
                {"dg", 2},
                {"bcmp", 3},
                {"fhvwy", 4},
                {"k", 5},
                {"jx", 8},
                {"qz", 10}
        }.SelectMany(kv => kv.Key.Select(c => (c, kv.Value))).ToDictionary(kv => kv.c, kv => kv.Value);
        private static int letterValues(char letter)
        {   
            values.TryGetValue(letter, out int value);

            return value;
        } 
        public static int Score(string input)
        {
            int score = 0;
            string word = input.ToLower();

            var scores = word.Select(c => letterValues(c));

            score = scores.Sum();
            
            return score;
        }
        public static int SpecialLetterScore(int score, string word, string type, IEnumerable<char> letters, int num = 1) 
        {
            type = type.ToLower();
            int addScore = 0;

            if (num == letters.Count())
                if (type == "double" && num > 0 && num <= 3)  
                {
                    foreach (var letter in letters)
                        addScore += word.Contains(letter) ? letterValues(letter) : 0;

                }
                else if (type == "triple" && num < 3 && num > 0)
                {
                    foreach (var letter in letters)
                        addScore += word.Contains(letter) ? (2 * letterValues(letter)) : 0;
                }
            
            return score + addScore;
            
        }
        public static int SpecialWordScore(int score, string type, int num = 1) 
        {
            type = type.ToLower();
            int finalScore = score;
            
            if (num == 1)
                finalScore = type == "double" ? score * 2 : type == "triple" ? score * 3 : score;
            else if (num == 2)
                finalScore = type == "double" ? score * 4 : type == "triple" ? score * 3 : score;

            return finalScore;
        }
    }
}
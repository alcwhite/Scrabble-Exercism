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
        public static int SpecialLetterScore(int score, string type, char letter) 
        {
            type = type.ToLower();

            int finalScore = type == "double" ? score + letterValues(letter) : type == "triple" ? score + (2 * letterValues(letter)): score;

            return finalScore;
            
        }
        public static int SpecialWordScore(int score, string type) 
        {
            type = type.ToLower();
            
            int finalScore = type == "double" ? score * 2 : type == "triple" ? score * 3 : score;

            return finalScore;
        }
    }
}
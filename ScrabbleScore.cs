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
        public static int SpecialLetterScore(int score, string type, List<char> letters, int number = 1) 
        {
            type = type.ToLower();
            int finalScore = score;
            int addScore = 0;

            if (number == 1 || number == 2)
            {
                if (type == "double")  
                {
                    letters.ForEach(letter => addScore += letterValues(letter));
                }
                else if (type == "triple")
                {
                    letters.ForEach((letter) => addScore += (2 * letterValues(letter)));
                }
            }
            else if (number == 3) {
                if (type == "double")
                {
                    letters.ForEach(letter => addScore += letterValues(letter));
                }
            }
            
            return score + addScore;
            
        }
        public static int SpecialWordScore(int score, string type, int number = 1) 
        {
            type = type.ToLower();
            int finalScore = score;
            
            if (number == 1) {
                finalScore = type == "double" ? score * 2 : type == "triple" ? score * 3 : score;
            }
            else if (number == 2) {
                finalScore = type == "double" ? score * 4 : type == "triple" ? score * 3 : score;
            }
            return finalScore;
        }
    }
}
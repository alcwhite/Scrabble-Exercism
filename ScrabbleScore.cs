using System;
using System.Collections.Generic;
using System.Linq;

public static class ScrabbleScore
{
    static Dictionary<char, int> values = new Dictionary<char, int>()
        {
            {'a', 1},
            {'e', 1},
            {'i', 1},
            {'o', 1},
            {'u', 1},
            {'l', 1},
            {'n', 1},
            {'r', 1},
            {'s', 1},
            {'t', 1},
            {'d', 2},
            {'g', 2},
            {'b', 3},
            {'c', 3}, 
            {'m', 3}, 
            {'p', 3},
            {'f', 4},
            {'h', 4},
            {'v', 4},
            {'w', 4},
            {'y', 4},
            {'k', 5},
            {'j', 8},
            {'x', 8},
            {'q', 10},
            {'z', 10}
      };
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
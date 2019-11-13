using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

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
        static Dictionary<Tuple<int, int>, string> specialSquares = new Dictionary<List<Tuple<int, int>>, string>()
        {
            // (y -- down, x -- across)
            {new List<Tuple<int, int>>{new Tuple<int, int>(1, 1), new Tuple<int, int>(1, 8), new Tuple<int, int>(1, 15), new Tuple<int, int>(8, 1), new Tuple<int, int>(8, 15), new Tuple<int, int>(15, 1), new Tuple<int, int>(15, 8), new Tuple<int, int>(15, 15)}, "triple word"},
            {new List<Tuple<int, int>>{new Tuple<int, int>(2, 2), new Tuple<int, int>(2, 14), new Tuple<int, int>(3, 3), new Tuple<int, int>(3, 13), new Tuple<int, int>(4, 4), new Tuple<int, int>(4, 12), new Tuple<int, int>(5, 5), new Tuple<int, int>(5, 11), new Tuple<int, int>(8, 8), new Tuple<int, int>(11, 5), new Tuple<int, int>(11, 11), new Tuple<int, int>(12, 4), new Tuple<int, int>(12, 12), new Tuple<int, int>(13, 3), new Tuple<int, int>(13, 13), new Tuple<int, int>(14, 2), new Tuple<int, int>(14, 14)}, "double word"},
            {new List<Tuple<int, int>>{new Tuple<int, int>(2, 6), new Tuple<int, int>(2, 10), new Tuple<int, int>(6, 2), new Tuple<int, int>(6, 6), new Tuple<int, int>(6, 10), new Tuple<int, int>(6, 14), new Tuple<int, int>(10, 2), new Tuple<int, int>(10, 6), new Tuple<int, int>(10, 10), new Tuple<int, int>(10, 14), new Tuple<int, int>(14, 6), new Tuple<int, int>(14, 10)}, "triple letter"},
            {new List <Tuple<int, int>>{new Tuple<int, int>(1, 4), new Tuple<int, int>(1, 12), new Tuple<int, int>(3, 7), new Tuple<int, int>(3, 9), new Tuple<int, int>(4, 1), new Tuple<int, int>(4, 8), new Tuple<int, int>(4, 15), new Tuple<int, int>(7, 3), new Tuple<int, int>(7, 7), new Tuple<int, int>(7, 9), new Tuple<int, int>(7, 13), new Tuple<int, int>(8, 4), new Tuple<int, int>(8, 12), new Tuple<int, int>(9, 3), new Tuple<int, int>(9, 7), new Tuple<int, int>(9, 9), new Tuple<int, int>(9, 13), new Tuple<int, int>(12, 1), new Tuple<int, int>(12, 8), new Tuple<int, int>(12, 15), new Tuple<int, int>(13, 7), new Tuple<int, int>(13, 9), new Tuple<int, int>(15, 4), new Tuple<int, int>(15, 12)}, "double letter"},
            {new List <Tuple<int, int>>{new Tuple<int, int>(15, 15)}, "final corner"}
        }.SelectMany(kv => kv.Key.Select(c => (c, kv.Value))).ToDictionary(kv => kv.c, kv => kv.Value);


        private static int LetterValues(char letter)
        {   
            values.TryGetValue(letter, out int value);

            return value;
        } 

        public static int Score(string word, Tuple<int, int> start, string direction)
        {
            var squares = GetSquares(word, start, direction);
            int score = 0;
            int multiplier = 1;
            foreach (var square in squares)
            {
                score += square.Item1;
                multiplier *= square.Item2 == "double word" ? 2 : square.Item2 == "triple word" ? 3 : 1;
            }
            bool possible = Possible(word, start, direction);

            return possible ? score * multiplier : 0;
        }

        public static List<Tuple<int, string>> GetSquares(string word, Tuple<int, int> start, string direction)
        {
            int starty = start.Item1;
            int startx = start.Item2;
            var squares = new List<Tuple<int, string>>();
            foreach (char letter in word)
            {
                squares.Add(SquareValue(word, letter, start, direction));
            }
            
            return squares;
        }
        public static Tuple<int, string> SquareValue(string word, char letter, Tuple<int, int> start, string direction)
        {
            int add = word.IndexOf(letter);
            var square = direction == "across" ? new Tuple<int, int>(start.Item1, start.Item2 + add) : new Tuple<int, int>(start.Item1 + add, start.Item2);
            int value = LetterValues(letter);
            int multiply = 1;
            specialSquares.TryGetValue(square, out string special);
            value *= special == "double letter" ? 2 : special == "triple letter" ? 3 : 1;

            return new Tuple<int, string>(value * multiply, special);
        }
        public static bool Possible(string word, Tuple<int, int> start, string direction)
        {
            int length = word.Length;
            int startx = start.Item2;
            int starty = start.Item1;
            var finalSquare = specialSquares.FirstOrDefault(x => x.Value == "final corner").Key;
            int lastx = finalSquare.Item2;
            int lasty = finalSquare.Item1;
            int endx = direction == "across" ? startx + length - 1 : startx;
            int endy = direction == "down" ? starty + length - 1 : starty;
            return endx > lastx || endy > lasty || (direction != "across" && direction != "down") ? false : true;
        }

        // {a, a1}


        // public static int Score(string input)
        // {
        //     int score = 0;
        //     string word = input.ToLower();

        //     var scores = word.Select(c => LetterValues(c));

        //     score = scores.Sum();
            
        //     return score;
        // }
        // public static int SpecialLetterScore(int score, string word, int type, IEnumerable<char> letters) 
        // {
        //     int addScore = 0;

        //     if (type == 2)  
        //     {
        //         foreach (var letter in letters)
        //             addScore += word.Contains(letter) ? LetterValues(letter) : 0;

        //     }
        //     else if (type == 3)
        //     {
        //         foreach (var letter in letters)
        //             addScore += word.Contains(letter) ? (2 * LetterValues(letter)) : 0;
        //     }
            
        //     return addScore;
            
        // }
        // public static int SpecialWordScore(int score, int type, int num = 1) 
        // {
        //     int finalScore = score;
            
        //     if (num == 1)
        //         finalScore = type == 2 ? score * 2 : type == 3 ? score * 3 : score;
        //     else if (num == 2)
        //         finalScore = type == 2 ? score * 4 : type == 3 ? score * 3 : score;

        //     return finalScore;
        // }
    }
}
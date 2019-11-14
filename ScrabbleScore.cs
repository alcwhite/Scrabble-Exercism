using System;
using System.Collections.Generic;
using System.Linq;

namespace scrabble_score
{
    public static class ScrabbleScore
    {
        static Dictionary<char, int> values = new Dictionary<string, int>()
            {
                {"_", 0},
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
            {new List <Tuple<int, int>>{new Tuple<int, int>(1, 4), new Tuple<int, int>(1, 12), new Tuple<int, int>(3, 7), new Tuple<int, int>(3, 9), new Tuple<int, int>(4, 1), new Tuple<int, int>(4, 8), new Tuple<int, int>(4, 15), new Tuple<int, int>(7, 3), new Tuple<int, int>(7, 7), new Tuple<int, int>(7, 9), new Tuple<int, int>(7, 13), new Tuple<int, int>(8, 4), new Tuple<int, int>(8, 12), new Tuple<int, int>(9, 3), new Tuple<int, int>(9, 7), new Tuple<int, int>(9, 9), new Tuple<int, int>(9, 13), new Tuple<int, int>(12, 1), new Tuple<int, int>(12, 8), new Tuple<int, int>(12, 15), new Tuple<int, int>(13, 7), new Tuple<int, int>(13, 9), new Tuple<int, int>(15, 4), new Tuple<int, int>(15, 12)}, "double letter"}
        }.SelectMany(kv => kv.Key.Select(c => (c, kv.Value))).ToDictionary(kv => kv.c, kv => kv.Value);

        static Tuple<int, int> finalCorner = new Tuple<int, int> (15, 15);

        private static int LetterValues(char letter)
        {   
            values.TryGetValue(letter, out int value);

            return value;
        } 

        public static int Score(string word, Tuple<int, int> start, string direction)
        {
            word = word.ToLower();
            int score = 0;
            int multiplier = 1;

            bool possible = Possible(word, start, direction);
            if (!possible)
                return 0;

            var squares = GetSquares(word, start, direction);
            foreach (var square in squares)
            {
                score += square.Item1;
                multiplier *= square.Item2 == "double word" ? 2 : square.Item2 == "triple word" ? 3 : 1;
            }

            return score * multiplier;
        }

        public static List<Tuple<int, string>> GetSquares(string word, Tuple<int, int> start, string direction)
        {
            var squares = new List<Tuple<int, string>>();
            int add = 0;
            foreach (char letter in word)
            {   
                squares.Add(SquareValue(letter, start, direction, add));
                add++;
            }
            
            return squares;
        }
        public static Tuple<int, string> SquareValue(char letter, Tuple<int, int> start, string direction, int add)
        {
            var square = direction == "across" ? new Tuple<int, int>(start.Item1, start.Item2 + add) : new Tuple<int, int>(start.Item1 + add, start.Item2);
            int value = LetterValues(letter);
            int multiplier = 1;
            specialSquares.TryGetValue(square, out string special);
            multiplier *= special == "double letter" ? 2 : special == "triple letter" ? 3 : 1;

            return new Tuple<int, string>(value * multiplier, special);
        }
        public static bool Possible(string word, Tuple<int, int> start, string direction)
        {
            IEnumerable<char> validChars = values.Keys;
            IEnumerable<char> validWord = word.Where(letter => validChars.Contains(letter));
            int length = word.Length;
            int startx = start.Item2;
            int starty = start.Item1;
            var finalSquare = finalCorner;
            int lastx = finalSquare.Item2;
            int lasty = finalSquare.Item1;
            int endx = direction == "across" ? startx + length - 1 : startx;
            int endy = direction == "down" ? starty + length - 1 : starty;
            return validWord.Count() != word.Length ||endx > lastx || endy > lasty || startx <= 0 || starty <= 0 || (direction != "across" && direction != "down") ? false : true;
        }
    }
}
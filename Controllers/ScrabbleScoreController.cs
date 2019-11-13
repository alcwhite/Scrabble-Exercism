using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static scrabble_score.ScrabbleScore;

namespace scrabble_score.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScrabbleScoreController : ControllerBase
    {
        private readonly ILogger<ScrabbleScoreController> _logger;

        public ScrabbleScoreController(ILogger<ScrabbleScoreController> logger)
        {
            _logger = logger;
        }

        // [Route("/{word}")]
        // public int Get(string word)
        // {
        //     return Score(word); 
        // }
        // [Route("/{word}/{type}Letter/{letters}")]
        // public int Get(string word, int type, IEnumerable<char> letters)
        // {
        //     return SpecialLetterScore(Score(word), word, type, letters);
        // }
        // [Route("/{word}/{type}Word/{count}")]
        // public int Get(string word, int type, int count)
        // {
        //     return SpecialWordScore(Score(word), type, count);
        // }
        // [Route("/{word}/{letterType}Letter/{letters}/{wordType}Word/{count}")]
        // public int Get(string word, int letterType, IEnumerable<char> letters, int wordType, int count)
        // {
        //     return SpecialWordScore(SpecialLetterScore(Score(word), word, letterType, letters), wordType, count);
        // }
        [Route("/{word}/{startx}/{starty}/{direction}")]
        public int Get(string word, int startx = 8, int starty = 8, string direction = "across")
        {
            return Score(word, new Tuple<int, int> (starty, startx), direction);
        }
    }
}
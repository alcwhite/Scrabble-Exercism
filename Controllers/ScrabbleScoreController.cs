using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpGet]
        public int Get([FromBody] Input i)
        {
            if (!i.SpecialLetter && !i.SpecialWord)
            {
                return ScrabbleScore.Score(i.Word);
            } 
            else if (i.SpecialLetter && !i.SpecialWord) 
            {
                int score = 0;
                i.LetterNames.ForEach((letter) => {
                    var e = i.Word.Contains(letter) ? score = ScrabbleScore.SpecialLetterScore(ScrabbleScore.Score(i.Word), i.LetterType, i.LetterNames, i.LetterCount = 1) : score = ScrabbleScore.Score(i.Word);
                });
                      
                return score;
            } 
            else if (!i.SpecialLetter && i.SpecialWord)
            {
                return ScrabbleScore.SpecialWordScore(ScrabbleScore.Score(i.Word), i.WordType, i.WordCount = 1);
            } 
            else
            {
                int score = 0;
                i.LetterNames.ForEach((letter) => {
                    var e = i.Word.Contains(letter) ? score = ScrabbleScore.SpecialWordScore(ScrabbleScore.SpecialLetterScore(ScrabbleScore.Score(i.Word), i.LetterType, i.LetterNames, i.LetterCount = 1), i.WordType, i.WordCount = 1) : score = ScrabbleScore.SpecialWordScore(ScrabbleScore.Score(i.Word), i.WordType, i.WordCount = 1);
                });
                return score;
            }
        }
    }
}
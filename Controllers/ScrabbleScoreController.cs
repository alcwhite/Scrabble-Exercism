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
                return ScrabbleScore.Score(i.Word); 
            else if (i.SpecialLetter && !i.SpecialWord) 
                return ScrabbleScore.SpecialLetterScore(ScrabbleScore.Score(i.Word), i.Word, i.LetterType, i.LetterNames, i.LetterCount);
            else if (!i.SpecialLetter && i.SpecialWord)
                return ScrabbleScore.SpecialWordScore(ScrabbleScore.Score(i.Word), i.WordType, i.WordCount);
            else
                return ScrabbleScore.SpecialWordScore(ScrabbleScore.SpecialLetterScore(ScrabbleScore.Score(i.Word), i.Word, i.LetterType, i.LetterNames, i.LetterCount), i.WordType, i.WordCount);
        }
    }
}
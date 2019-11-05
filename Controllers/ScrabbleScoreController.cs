using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace scrabble_score.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class ScrabbleScoreController : ControllerBase
    {
        private readonly ILogger<ScrabbleScoreController> _logger;

        public ScrabbleScoreController(ILogger<ScrabbleScoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int GetScore(string word, bool specialLetter = false, char letterName = '/', string letterType = "", bool specialWord = false, string wordType = "")
        {
            if (!specialLetter && !specialWord)
            {
                return ScrabbleScore.Score(word);
            } 
            else if (specialLetter && !specialWord)
            {
                return ScrabbleScore.SpecialLetterScore(ScrabbleScore.Score(word), letterType, letterName);
            } 
            else if (!specialLetter && specialWord)
            {
                return ScrabbleScore.SpecialWordScore(ScrabbleScore.Score(word), wordType);
            } 
            else
            {
                return ScrabbleScore.SpecialWordScore(ScrabbleScore.SpecialLetterScore(ScrabbleScore.Score(word), letterType, letterName), wordType);
            }
        }
    }
}
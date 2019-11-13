using System;
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

        [Route("/{word}/x{startx?}/y{starty?}/{direction?}")]
        public string Get(string word, int startx = 8, int starty = 8, string direction = "across")
        {
            int score = Score(word, new Tuple<int, int> (starty, startx), direction);
            return score <= 0 ? "Something went wrong" : "Your score is " + score;
        }
    }
}
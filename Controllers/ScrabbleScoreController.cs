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

        [Route("/{word}/{startx?}/{starty?}/{direction?}")]
        public string Get(string word, int startx = 8, int starty = 8, string direction = "across")
        {
            int score = Score(word, new Tuple<int, int> (starty, startx), direction);
            return score <= 0 ? "Something went wrong. Please check that your word fits on the board, goes in a valid direction, and contains only letters and underscores, which represent blank tiles." : "Your score is " + score;
        }
    }
}
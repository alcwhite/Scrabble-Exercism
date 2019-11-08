using System.Collections.Generic;

public class Input
{
    public string Word { get; set; }
    public bool SpecialLetter { get; set; }
    public bool SpecialWord { get; set; }
    public IEnumerable<char> LetterNames { get; set; }
    public string LetterType { get; set; }
    public string WordType { get; set; }
    public int WordCount { get; set; }
    public int LetterCount { get; set; }
}
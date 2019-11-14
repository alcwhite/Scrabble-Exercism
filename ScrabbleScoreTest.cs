// This file was auto-generated based on version 1.1.0 of the canonical data.

using Xunit;
using System;


public class ScrabbleScoreTest
{
    [Fact]
    public void Lowercase_letter()
    {
        Assert.Equal(2, scrabble_score.ScrabbleScore.Score("a", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Uppercase_letter()
    {
        Assert.Equal(2, scrabble_score.ScrabbleScore.Score("A", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Valuable_letter()
    {
        Assert.Equal(8, scrabble_score.ScrabbleScore.Score("f", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Short_word()
    {
        Assert.Equal(4, scrabble_score.ScrabbleScore.Score("at", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Short_valuable_word()
    {
        Assert.Equal(24, scrabble_score.ScrabbleScore.Score("zoo", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Medium_word()
    {
        Assert.Equal(22, scrabble_score.ScrabbleScore.Score("apples", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Medium_valuable_word()
    {
        Assert.Equal(54, scrabble_score.ScrabbleScore.Score("quirky", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Long_mixed_case_word()
    {
        Assert.Equal(972, scrabble_score.ScrabbleScore.Score("OxyphenButazone", new Tuple<int, int>(8, 1), "across"));
    }

    [Fact]
    public void English_like_word()
    {
        Assert.Equal(18, scrabble_score.ScrabbleScore.Score("pinata", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Empty_input()
    {
        Assert.Equal(0, scrabble_score.ScrabbleScore.Score("", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Random_character()
    {
        Assert.Equal(0, scrabble_score.ScrabbleScore.Score("[", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Random_character_in_word()
    {
        Assert.Equal(52, scrabble_score.ScrabbleScore.Score("Oxy-phen", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Down()
    {
        Assert.Equal(22, scrabble_score.ScrabbleScore.Score("apples", new Tuple<int, int>(8, 8), "across"));
    }

    [Fact]
    public void Street()
    {
        Assert.Equal(14, scrabble_score.ScrabbleScore.Score("street", new Tuple<int, int>(8, 8), "down"));
    }
}
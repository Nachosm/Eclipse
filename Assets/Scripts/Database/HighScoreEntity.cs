using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class HighScoreEntity {

    public string _map;
    public int _highScore;
    public int _respectScore;
    public Boolean _isUnlocked;
    public String _lastPlayed;
    

    public HighScoreEntity(string map, int highScore, int respectScore, bool unlocked)
    {
        _map = map;
        _highScore = highScore;
        _respectScore = respectScore;
        _isUnlocked = unlocked;
        _lastPlayed = "";
        
    }

    public HighScoreEntity(string map, int highScore, int respectScore, bool unlocked, string lastPlayed)
    {
        _map = map;
        _highScore = highScore;
        _respectScore = respectScore;
        _isUnlocked = unlocked;
        _lastPlayed = lastPlayed;
    }

    public static HighScoreEntity getFakeHighScore()
    {
        return new HighScoreEntity("0", 1, 9999, false, "0.0.0");
    }
}

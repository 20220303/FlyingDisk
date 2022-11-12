using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//记分员
class ScoreRecorder
{
    
    private int score;

    public void setScore(int score_)
    {
        score = score_;
    }

    public void addScore(int add_)
    {
        score += add_;
    }
    

    public int getScore()
    {
        return score;
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : BaseManager
{
    private int _score;
    private int _limitScore;

    public int GetScore()
    {
        return _score;
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }

    public void RemoveScore(int amount)
    {
        _score -= amount;
    }

    public void SetLimitScore(int amount)
    {
        _limitScore = amount;
    }

    public bool LimitScoreReached()
    {
        return _score >= _limitScore;
    }

    public override void Reset()
    {
        _score = 0;
    }

    public void SaveMaxScore()
    {
        int maxScore = 0;
        maxScore = PlayerPrefs.GetInt("MaxScore");
        if (_score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", _score);
        }
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static ScoreManager _instance;

    public static ScoreManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}

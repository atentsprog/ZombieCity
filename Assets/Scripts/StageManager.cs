using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehavior<StageManager>
{
    public SaveInt highScore;
    public int score;
    public SaveInt gold;

    new private void Awake()
    {
        base.Awake();
        highScore = new SaveInt("highScore");
        gold = new SaveInt("Gold");

        GoldUIRefresh();
        ScoreUIRefresh();
    }
    public void AddScore(int addScore)
    {
        score += addScore;

        if (highScore < score)
            highScore.Value = score;

        ScoreUIRefresh();
    }

    internal void AddGold(int amount)
    {
        gold += amount;

        GoldUIRefresh();
    }

    private void GoldUIRefresh()
    {
        GoldUI.Instance.UpdateUI(gold);
    }
    private void ScoreUIRefresh()
    {
        ScoreUI.Instance.UpdateUI(score, highScore);
    }
}

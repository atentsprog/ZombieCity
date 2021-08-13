using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultUI : BaseUI<GameResultUI>
{
    Image starScore;
    TextMeshProUGUI currentScore;
    TextMeshProUGUI bestScoreText;
    Transform ui;

    protected override void OnInit()
    {
        ui = transform.Find("UI");
        starScore = ui.Find("StarScore").GetComponent<Image>();
        currentScore = ui.Find("CurrentScore").GetComponent<TextMeshProUGUI>();
        bestScoreText = ui.Find("BestScoreText").GetComponent<TextMeshProUGUI>();


        ui.Find("Buttons/Home").GetComponent<Button>().AddListener(this, OnClickHome);
        ui.Find("Buttons/Restart").GetComponent<Button>().AddListener(this, OnClickRestart);
        ui.Find("Buttons/Ranking").GetComponent<Button>().AddListener(this, OnClickRanking);
    }

    private void OnClickRanking()
    {
        Close();
        print("OnClickRanking");
    }

    private void OnClickRestart()
    {
        Close();
        SceneManager.LoadSceneAsync("Stage1");
    }

    private void OnClickHome()
    {
        Close();
        SceneManager.LoadSceneAsync("Title");
    }

    [System.Serializable]
    public class StarScore
    {
        public int minScore;
        public Sprite sprite;
    }
    public List<StarScore> minStarScore;

    public float initScale = 0.7f;
    public float endScale = 0.7f;
    public float scaleDuration = 0.5f;
    internal void ShowResult(int score, int highScore)
    {
        base.Show();
        ui.localScale = Vector3.one * initScale;
        ui.DOScale(1, scaleDuration);
        // 현재 점수.
        // 최고 점수 표시..

        currentScore.text = score.ToNumber();
        bestScoreText.text = highScore.ToNumber();
        starScore.sprite = GetStarScoreSprite(score);
    }

    private Sprite GetStarScoreSprite(int score)
    {
        foreach(var item in minStarScore)
        {
            if (item.minScore > score)
                return item.sprite;
        }

        return minStarScore[minStarScore.Count - 1].sprite;
    }
}

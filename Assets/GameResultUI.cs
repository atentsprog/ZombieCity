using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultUI : SingletonMonoBehavior<GameResultUI>
{
    TextMeshProUGUI youScoreNumber;
    Image starScore;
    TextMeshProUGUI bestScoreNumber;

    [System.Serializable]
    public class StartScore
    {
        public int minScore;
        public Sprite sprite;
    }
    public List<StartScore> startScores;
    protected override void OnInit()
    {
        youScoreNumber = transform.Find("YouScoreNumber").GetComponent<TextMeshProUGUI>();
        starScore = transform.Find("StarScore").GetComponent<Image>();
        bestScoreNumber = transform.Find("BestScoreNumber").GetComponent<TextMeshProUGUI>();

        transform.Find("Buttons/Restart").GetComponent<Button>().AddListener(this, OnClickRestart);
        transform.Find("Buttons/Ranking").GetComponent<Button>().AddListener(this, OnClickRanking);
        transform.Find("Buttons/Home").GetComponent<Button>().AddListener(this, OnClickHome);
    }

    private void OnClickRestart()
    {
        Close();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Stage1");
    }

    private void OnClickHome()
    {
        Close();
        InstantiateHelper.IngSceneLoad = true;
        SceneManager.LoadScene("Title");
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        InstantiateHelper.IngSceneLoad = false;
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void OnClickRanking()
    {
        Close();
        RankingUI.Instance.ShowRanking(int.Parse(youScoreNumber.text));
    }

    internal void ShowResult(int score, int highScore)
    {
        base.Show();
        youScoreNumber.text = score.ToNumber();
        bestScoreNumber.text = highScore.ToNumber();
        starScore.sprite = GetStarSprite(score);
    }
    private Sprite GetStarSprite(int score)
    {
        foreach( var item in startScores)
        {
            if (item.minScore > score)
                return item.sprite;
        }
        return startScores[startScores.Count - 1].sprite;
    }
}

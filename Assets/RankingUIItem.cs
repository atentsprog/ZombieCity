using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIItem : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    Image icon;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        icon = transform.Find("Icon").GetComponent<Image>();
        button = transform.Find("Button").GetComponent<Button>();
    }
}

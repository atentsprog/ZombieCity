using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data<T>
{
    public T data;
}

[System.Serializable]
public class PlayerPrefsData<T>
{
    public T data;

    public PlayerPrefsData(string _key)
    {
        key = _key;
        LoadData();
    }

    string key;
    public void LoadData()
    {
        data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        if (data == null)
        {
            Debug.LogWarning("record == null");
            return;
        }

        Debug.LogWarning("Load Complete");
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);

        try
        {
            PlayerPrefs.SetString(key, json);
            Debug.Log("json:" + json);
        }
        catch (System.Exception err)
        {
            Debug.Log("Got: " + err);
        }
    }
}


[System.Serializable]
public class RankInfo
{
    public string stringValue;
    public int score;
}

[System.Serializable]
public class RankInfoData
{
    public List<RankInfo> ranking = new List<RankInfo>();
    public int myInt;
    public string myString;
}

public class RankingUI : MonoBehaviour
{
    public PlayerPrefsData<RankInfoData> record;
    // Start is called before the first frame update
    void Start()
    {
        record = new PlayerPrefsData<RankInfoData>("Record");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
            record.SaveData();

        if (Input.GetKeyDown(KeyCode.F8))
            record.LoadData();

        if (Input.GetKeyDown(KeyCode.F9))
        {
            var ranking = record.data.ranking;
            foreach (var item in ranking)
                print(item);

            print(record.data.myInt);
        }
    }
}

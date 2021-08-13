using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrefsData<T>
{
    public PlayerPrefsData(string _key)
    {
        key = _key;
    }

    string key;
    public T LoadData()
    {
        T record = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        if (record == null)
        {
            Debug.LogWarning("record == null");
            return default(T);
        }

        Debug.LogWarning("Load Complete");
        return record;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);

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
public class Record : PlayerPrefsData<Record>
{
    public List<RankInfo> ranking = new List<RankInfo>();

    public Record(string key):base(key)
    {
        Record savedValue = LoadData();
        ranking = savedValue.ranking;
    }
}

public class RankingUI : MonoBehaviour
{
    public Record record;
    // Start is called before the first frame update
    void Start()
    {
        record = new Record("Record");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
            record.SaveData();

        if (Input.GetKeyDown(KeyCode.F8))
            record.LoadData();
    }
}

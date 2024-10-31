using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuperManager : MonoBehaviour
{
    public static SuperManager Instance;


    private int m_Points;
    public Text ScoreText;
    public Text HighScoreText;
    public MainManager mainManager;

    public int highScore;
    public string highScoreName;
    public string playerName;

    Scene m_Scene;
    string sceneName;

    private void Start()
    {
    }

    public void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

        if (sceneName == "main")
        {
            mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
            m_Points = mainManager.m_Points;
            HighScoreText = mainManager.HighScoreText;
            ScoreText = mainManager.ScoreText;
        }

    }   

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScoreName;
        public string playerName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = m_Points;
        data.highScoreName = playerName;
        data.playerName = playerName;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;

            if (sceneName == "main")
            {
                HighScoreText.text = $"Best Score : {highScoreName} : {highScore}";
            }
        }
    }

    public void SaveName(string inputtedName)
    {
        SaveData data = new SaveData();
        data.playerName = inputtedName;
        data.highScoreName = highScoreName;
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;

            Debug.Log(playerName);

            ScoreText.text = $"Score : {playerName} : {m_Points}";
        }
    }
}

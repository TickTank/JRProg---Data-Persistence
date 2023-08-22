using TMPro;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public string name;
    public string highScoreName;
    public int bestScore;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;    
        DontDestroyOnLoad(transform.gameObject);

        LoadScore();
    }

    private void Update()
    {
        scoreText.text = "High Score: " + highScoreName + " : " + bestScore;
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);

            bestScore = data.scoreHS;
            highScoreName = data.nameHS;
        }
    }
}

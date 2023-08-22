using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public int highScore;
    
    public string name;
    public string highScoreName;
    private Text ScoreText;
    private Text bestScoreText;
    public GameObject GameOverText;
    public Rigidbody Ball;

    private bool m_Started = false;
    public int m_Points;
    private bool m_GameOver = false;

    void Start()
    {
        highScore = ScoreManager.Instance.bestScore;
        highScoreName = ScoreManager.Instance.highScoreName;
        name = ScoreManager.Instance.name;
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        bestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();

        const float step = 0.6f;    
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }   
    }
    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //SceneManager.LoadScene(1);
            }
        }

        if (ScoreText.text == "Score : 0")
        {
            ScoreText.text = $"Score : {ScoreManager.Instance.name} : {m_Points}";
            bestScoreText.text = $"Best Score : {ScoreManager.Instance.highScoreName} : {ScoreManager.Instance.bestScore}";
        }
        bestScoreText.text = $"Best Score : {highScoreName} : {highScore}";
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {name} : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_Points > highScore) { highScore = m_Points; ScoreManager.Instance.bestScore = m_Points; highScoreName = name; ScoreManager.Instance.highScoreName = name; }

        SaveScore();
    }

    [System.Serializable]
    public class SaveData
    {
        public int scoreHS;
        public string nameHS;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.scoreHS = highScore;
        data.nameHS = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("highScoreName = " + data.nameHS);
    }
}

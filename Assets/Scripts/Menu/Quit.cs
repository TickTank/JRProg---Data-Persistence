using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

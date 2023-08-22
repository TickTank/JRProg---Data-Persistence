using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button button;
    public TMP_InputField nameText;
    public GameObject emptyNameText;
    public Animator fade;

    void Start()
    {
        button.onClick.AddListener(LoadMain);
    }

    public void LoadMain()
    {
        if(nameText.text == "")
        {
            fade.enabled = true;
            StartCoroutine(Wait());

            Debug.Log("Should be done FadeIN and FadeOUT");
        }
        else { ScoreManager.Instance.name = nameText.text; SceneManager.LoadScene(1); }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.4f);
        fade.enabled=false;
    }
}

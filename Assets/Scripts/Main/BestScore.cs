using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public static BestScore Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
}

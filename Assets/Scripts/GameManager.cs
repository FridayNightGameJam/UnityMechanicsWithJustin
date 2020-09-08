using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartAfter(3));
    }

    private IEnumerator RestartAfter(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

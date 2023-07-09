using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOnWin : MonoBehaviour
{
    public Image Img;
    public GameObject RestartText;
    public float fadeTime;

    Subscription<PlayerWon> winSub;
    bool playerWon = false;
    float timeSinceWin = 0f;

    // Start is called before the first frame update
    void Start()
    {
        winSub = EventBus.Subscribe<PlayerWon>(OnPlayerWin);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWon)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            timeSinceWin += Time.unscaledDeltaTime;

            var tempColor = Img.color;
            tempColor.a = Mathf.Lerp(0, 1f, timeSinceWin / fadeTime);
            Img.color = tempColor;
        }
        if (timeSinceWin >= fadeTime && !RestartText.activeSelf)
        {
            RestartText.SetActive(true);
        }
    }

    void OnPlayerWin(PlayerWon e)
    {
        playerWon = true;
    }
}

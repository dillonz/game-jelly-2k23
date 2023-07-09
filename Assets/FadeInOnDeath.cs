using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOnDeath : MonoBehaviour
{
    public Image Img;
    public GameObject RestartText;
    public float fadeTime;

    Subscription<PlayerDied> deathSub;
    bool playerDead = false;
    float timeSinceDeath = 0f;

    // Start is called before the first frame update
    void Start()
    {
        deathSub = EventBus.Subscribe<PlayerDied>(OnPlayerDeath);     
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            timeSinceDeath += Time.unscaledDeltaTime;

            var tempColor = Img.color;
            tempColor.a = Mathf.Lerp(0, 1f, timeSinceDeath / fadeTime);
            Img.color = tempColor;
        }
        if (timeSinceDeath >= fadeTime && !RestartText.activeSelf)
        {
            RestartText.SetActive(true);
        }
    }

    void OnPlayerDeath(PlayerDied e)
    {
        playerDead = true;
    }
}

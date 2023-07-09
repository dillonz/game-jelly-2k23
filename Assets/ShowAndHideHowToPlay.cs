using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHideHowToPlay : MonoBehaviour
{
    public GameObject HowToPlay;

    private Subscription<ShowHowToPlay> showSub;
    private Subscription<HideHowToPlay> hideSub;

    // Start is called before the first frame update
    void Start()
    {
        showSub = EventBus.Subscribe<ShowHowToPlay>(Show);
        hideSub = EventBus.Subscribe<HideHowToPlay>(Hide);
    }

    void Show(ShowHowToPlay e)
    {
        HowToPlay.SetActive(true);
    }

    void Hide(HideHowToPlay e)
    {
        HowToPlay.SetActive(false);
    }
}

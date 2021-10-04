using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public TMPro.TMP_Text tutorialText;
    public BoolReference isFirstRun;
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePlay(bool enable)
    {
        this.playButton.interactable = enable;
    }

    public void ShowShop(bool show)
    {
        if (show && this.isFirstRun) {
            this.tutorialText.text = "Buy the Box Placer to get started!";
            EnablePlay(false);
        } else if (!show && !this.isFirstRun) {
            this.tutorialText.text = "";
        }
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
    }
}

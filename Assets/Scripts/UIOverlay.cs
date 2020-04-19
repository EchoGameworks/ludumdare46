using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOverlay : MonoBehaviour
{
    [Header("Tooltip Banner")]
    public TextMeshProUGUI TooltipText;
    public GameObject TooltipBanner;
    public GameObject TooltipUnderline;

    [Header("Intro Banner")]
    public GameObject IntroBanner;
    public GameObject IntroButton;
    public GameObject IntroUnderline;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTooltipText(string Title, string Description, string AddInfo)
    {
        TooltipText.text = "<color=#00CEFF>" +Title + "</color><br><size=60%>" + Description + "<br>" + AddInfo;
    }

    public void SetShowToolTip(string Title, string Description, string AddInfo)
    {
        SetTooltipText(Title, Description, AddInfo);
        ShowTooltip();
    }

    public void ShowTooltip()
    {
        LeanTween.moveX(TooltipBanner, 600f, 0.3f).setEaseInOutCirc();
        LeanTween.moveX(TooltipUnderline, 600f, 0.3f).setDelay(0.2f).setEaseInOutCirc();
    }

    public void HideToolTip()
    {
        LeanTween.moveX(TooltipBanner, 1200f, 0.3f).setDelay(0.2f).setEaseInOutCirc();
        LeanTween.moveX(TooltipUnderline, 1200f, 0.3f).setEaseInOutCirc();
    }

    public void ShowIntro()
    {
        LeanTween.moveX(IntroBanner, 260f, 0.3f).setEaseInOutCirc().setDelay(0.3f);
        LeanTween.moveX(IntroButton, -500f, 0.3f).setEaseInOutCirc();
        LeanTween.moveX(IntroUnderline, 326f, 0.3f).setDelay(0.5f).setEaseInOutCirc();
    }

    public void HideIntro()
    {
        LeanTween.moveX(IntroBanner, -540f, 0.3f).setDelay(0.2f).setEaseInOutCirc();
        LeanTween.moveX(IntroButton, 60f, 0.3f).setEaseInOutCirc().setDelay(0.4f);
        LeanTween.moveX(IntroUnderline, -540f, 0.3f).setEaseInOutCirc();
    }
}

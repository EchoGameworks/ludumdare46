using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOverlay : MonoBehaviour
{
    [Header("Tooltip Banner")]
    public TextMeshProUGUI TooltipText;
    public RectTransform TooltipBanner;
    public RectTransform TooltipUnderline;

    [Header("Intro Banner")]
    public RectTransform IntroBanner;
    public RectTransform IntroButton;
    public RectTransform IntroUnderline;

    [Header("Gameover Banner")]
    public RectTransform GameOverBanner;
    public RectTransform GameOverUnderline;

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
        TooltipText.text = "<color=#00CEFF>" +Title + "</color><br><size=50%>" + Description + "<br><br>" + AddInfo;
    }

    public void SetShowToolTip(string Title, string Description, string AddInfo)
    {
        SetTooltipText(Title, Description, AddInfo);
        ShowTooltip();
    }

    public void ShowTooltip()
    {

        LeanTween.value(TooltipBanner.gameObject, TooltipBanner.anchoredPosition, new Vector2(-230f, TooltipBanner.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setOnUpdate(
            (Vector2 val) => {
                TooltipBanner.anchoredPosition = val;
            }
        );
        LeanTween.value(TooltipUnderline.gameObject, TooltipUnderline.anchoredPosition, new Vector2(-230f, TooltipUnderline.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.15f)
            .setOnUpdate(
            (       Vector2 val) => {
                TooltipUnderline.anchoredPosition = val;
            }
        );
    }

    public void HideToolTip()
    {
        LeanTween.value(TooltipBanner.gameObject, TooltipBanner.anchoredPosition, new Vector2(300f, TooltipBanner.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.15f)
            .setOnUpdate(
                (Vector2 val) => {
                    TooltipBanner.anchoredPosition = val;
                }
            );
        LeanTween.value(TooltipUnderline.gameObject, TooltipUnderline.anchoredPosition, new Vector2(300f, TooltipUnderline.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setOnUpdate(
            (Vector2 val) => {
                TooltipUnderline.anchoredPosition = val;
            }
        );
    }

    public void ShowIntro()
    {
        LeanTween.value(IntroBanner.gameObject, IntroBanner.anchoredPosition, new Vector2(260f, IntroBanner.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.3f)
            .setOnUpdate(
            (Vector2 val) => {
                IntroBanner.anchoredPosition = val;
            }
        );

        LeanTween.value(IntroButton.gameObject, IntroButton.anchoredPosition, new Vector2(-70f, IntroButton.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setOnUpdate(
            (Vector2 val) => {
                IntroButton.anchoredPosition = val;
            }
        );

        LeanTween.value(IntroUnderline.gameObject, IntroUnderline.anchoredPosition, new Vector2(245, IntroUnderline.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.5f)
            .setOnUpdate(
            (Vector2 val) => {
                IntroUnderline.anchoredPosition = val;
            }
        );
    }

    public void HideIntro()
    {
        LeanTween.value(IntroBanner.gameObject, IntroBanner.anchoredPosition, new Vector2(-600f, IntroBanner.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.2f)
            .setOnUpdate(
            (Vector2 val) => {
                IntroBanner.anchoredPosition = val;
            }
        );

        LeanTween.value(IntroButton.gameObject, IntroButton.anchoredPosition, new Vector2(60f, IntroButton.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.4f)
            .setOnUpdate(
            (Vector2 val) => {
                IntroButton.anchoredPosition = val;
            }
        );

        LeanTween.value(IntroUnderline.gameObject, IntroUnderline.anchoredPosition, new Vector2(-600f, IntroUnderline.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()

            .setOnUpdate(
            (Vector2 val) => {
                IntroUnderline.anchoredPosition = val;
            }
        );
    }

    public void ShowGameOver()
    {
        LeanTween.value(GameOverBanner.gameObject, GameOverBanner.anchoredPosition, new Vector2(-663f, GameOverBanner.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.3f)
            .setOnUpdate(
            (Vector2 val) => {
                GameOverBanner.anchoredPosition = val;
            }
        );
        LeanTween.value(GameOverUnderline.gameObject, GameOverUnderline.anchoredPosition, new Vector2(-325, GameOverUnderline.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.5f)
            .setOnUpdate(
            (Vector2 val) => {
                GameOverUnderline.anchoredPosition = val;
            }
        );

    }

    public void HideGameOver()
    {
        LeanTween.value(GameOverBanner.gameObject, GameOverBanner.anchoredPosition, new Vector2(260f, GameOverBanner.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setDelay(0.2f)
            .setOnUpdate(
            (Vector2 val) => {
                GameOverBanner.anchoredPosition = val;
            }
        );
        LeanTween.value(GameOverUnderline.gameObject, GameOverUnderline.anchoredPosition, new Vector2(600f, GameOverUnderline.anchoredPosition.y), 0.3f)
            .setEaseInOutCubic()
            .setOnUpdate(
            (Vector2 val) => {
                GameOverUnderline.anchoredPosition = val;
            }
        );
    }
}

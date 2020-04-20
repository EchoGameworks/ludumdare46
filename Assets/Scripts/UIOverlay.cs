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
    public TextMeshProUGUI GameoverText;
    public RectTransform GameOverUnderline;


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
        LeanTween.value(IntroBanner.gameObject, IntroBanner.anchoredPosition, new Vector2(280f, IntroBanner.anchoredPosition.y), 0.3f)
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

        LeanTween.value(IntroUnderline.gameObject, IntroUnderline.anchoredPosition, new Vector2(265, IntroUnderline.anchoredPosition.y), 0.3f)
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

    public void ShowGameOver(string Title, string Description)
    {
        GameoverText.text = "<size=120%><color=#00CEFF>" + Title + "</color></size><br><br><size=60%>" +
                            Description + "<br><br>" +
                            "Thanks for playing Valley of the Dying. This game was made during Ludum Dare 46 - a 48 hour, one-man game jam with the theme 'Keep it alive.' I hope you enjoyed playing!" +
                            "<br><br>Drew of Echo Gameworks";


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

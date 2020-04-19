using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatus : MonoBehaviour
{
    public GameObject Header;

    public Image HealthBarMax;
    public Image HealthBarCurrent;
    public Image HealthBarLag;
    public TextMeshProUGUI HeaderText;

    public Image SicknessIcon;
    public Image FillBackground;
    private Color fillColorStart;

    public float freezeLag;

    private CharacterBase characterBase;
    private Vector3 HeaderShowScale;
    private Vector3 HeaderHideScale;

    public Vector3 PointRotation;

    // Start is called before the first frame update
    void Start()
    {
        fillColorStart = FillBackground.color;
        HeaderShowScale = new Vector3(0.7f, 0.7f, 0.7f);
        HeaderHideScale = new Vector3(0f, 0.7f, 0.7f);
        Header.transform.localScale = HeaderHideScale;
        Header.SetActive(false);

        //PointRotation = new Vector3(90f, 0f, 90f);
        //PointRotation = new Vector3(115f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterBase == null) return;
        this.transform.eulerAngles = PointRotation;
        if(freezeLag > 0)
        {
            freezeLag -= Time.deltaTime;
        }
        if(freezeLag <= 0 && freezeLag > -1)
        {
            LeanTween.value(gameObject, HealthBarLag.fillAmount, ((characterBase.HealthCurrent / characterBase.HealthMax) * 0.55f) + 0.35f, 0.6f)
            .setOnUpdate((float v) => HealthBarLag.fillAmount = v)
            .setEaseInOutExpo();

            freezeLag = -1f;
        }
    }

    public void ToggleUI(bool showUI)
    {
        if (showUI)
        {
            ShowHeader();
        }
        else
        {
            HideHeader();
        }
    }

    public void ShowHeader()
    {
        Header.SetActive(true);
        LeanTween.scale(Header, HeaderShowScale, 0.3f).setEaseInOutCirc();
    }

    public void HideHeader()
    {
        LeanTween.scale(Header, HeaderHideScale, 0.3f).setEaseInOutCirc().setOnComplete(() => Header.SetActive(false));

    }

    public void SetName(string name)
    {
        HeaderText.text = name;
    }

    public void SetHealth(CharacterBase cb)
    {
        characterBase = cb;
        //HealthBarMax.fillAmount = ((characterBase.HealthMax / characterBase.HealthMaxGlobal) * 0.55f) +0.35f;
        HealthBarCurrent.fillAmount = ((characterBase.HealthCurrent / characterBase.HealthMax) * 0.55f) +0.35f;
        HealthBarLag.fillAmount = ((characterBase.HealthCurrent / characterBase.HealthMax) * 0.55f) +0.35f;
    }

    public void TakeTemporaryDamage()
    {        
        LeanTween.value(gameObject, HealthBarCurrent.fillAmount, ((characterBase.HealthCurrent / characterBase.HealthMax) * 0.55f) + 0.35f, 0.3f)
            .setOnUpdate((float v) => HealthBarCurrent.fillAmount = v)
            .setEaseInOutExpo();
        freezeLag = 0.5f;
    }

    public void TakePermanentDamage()
    {
        //HealthBarMax.fillAmount = characterBase.HealthMax / characterBase.HealthMax;
        HealthBarCurrent.fillAmount = characterBase.HealthCurrent / characterBase.HealthMax;
        HealthBarLag.fillAmount = characterBase.HealthCurrent / characterBase.HealthMax;
    }

    public void AttackFlash(Color flashColor)
    {
        LeanTween.value(gameObject, fillColorStart, flashColor, 0.5f)
            .setOnUpdateColor((Color c) => FillBackground.color = c)
            .setEaseInOutCubic()
            .setLoopPingPong(1);
    }
}

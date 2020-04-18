using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackArea : MonoBehaviour
{
    public GameObject AttackArea;

    private Vector3 AttackAreaShowScale;
    private Vector3 AttackAreaHideScale;

    // Start is called before the first frame update
    void Start()
    {
        AttackAreaShowScale = new Vector3(4f, 4f, 4f); //default
        AttackAreaHideScale = Vector3.zero;
        AttackArea.transform.localScale = AttackAreaHideScale;
        AttackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttackAreaSize(float radius)
    {
        this.transform.localScale = new Vector3(radius, radius, radius);
    }

    public void ToggleUI(bool isOn)
    {
        if (isOn)
        {
            ShowArea();
        }
        else
        {
            HideArea();
        }
    }

    public void ShowArea()
    {
        AttackArea.SetActive(true);
        LeanTween.scale(AttackArea, AttackAreaShowScale, 0.3f).setEaseInOutCirc();
    }

    public void HideArea()
    {
        LeanTween.scale(AttackArea, AttackAreaHideScale, 0.3f).setEaseInOutCirc().setOnComplete(() => AttackArea.SetActive(false));
    }
}

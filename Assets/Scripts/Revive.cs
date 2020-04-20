using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        HeroBase hb = other.GetComponent<HeroBase>();
        if (hb != null)
        {
            if(hb.StatusCurrent == CharacterBase.CharacterStatus.Sick)
            {
                hb.uiStatus.ShowRevive();
            }
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        HeroBase hb = other.GetComponent<HeroBase>();
        if (hb != null)
        {
            hb.uiStatus.HideRevive();
        }
    }
}

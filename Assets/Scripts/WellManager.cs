using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HeroBase hb = other.GetComponent<HeroBase>();
        if(hb != null)
        {
            if(hb.HeroType == HeroBase.HeroTypes.Tree)
            {
                hb.SetSickness(CharacterBase.CharacterSickness.Water);

                AudioManager.instance.PlaySound(AudioManager.SoundEffects.Water);
            }
        }
    }
}

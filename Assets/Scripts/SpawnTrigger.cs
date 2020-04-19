using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public enum SpawnTriggerTypes { Village, Well };

    public SpawnTriggerTypes SpawnTriggerType;
    public TripInfo TripInfo;

    private void OnTriggerEnter(Collider other)
    {
        HeroBase hb = other.GetComponent<HeroBase>();
        if(hb != null)
        {
            if(hb.HeroType == HeroBase.HeroTypes.Tree)
            {
                if(SpawnTriggerType == SpawnTriggerTypes.Village)
                {
                    if(hb.SicknessLevel == CharacterBase.CharacterSickness.None)
                    {
                        //next stage
                        TripInfo.NextStage();
                    }
                }
                else
                {
                    if (hb.SicknessLevel == CharacterBase.CharacterSickness.Water)
                    {
                        //next stage
                        TripInfo.NextStage();
                    }
                }
            }
        }
    }
}

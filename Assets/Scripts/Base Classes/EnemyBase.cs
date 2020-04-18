using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HeroBase;

public class EnemyBase : CharacterBase
{     

    public enum AttackStrategy { Closest, HeroType, Bloodthirst, Glory }

    [Header("Enemy Stats")]
    public HeroTypes HeroThirst;
    public AttackStrategy CurrentStrategy;
    public LayerMask CaravanLM;

    protected override void Start()
    {
        base.Start();
    }

    public HeroBase Scout()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 40f, CaravanLM);
        List<HeroBase> heroes = new List<HeroBase>();
        foreach(Collider c in hitColliders)
        {
            HeroBase hb = c.GetComponent<HeroBase>();
            if(hb != null)
            {
                heroes.Add(hb);
            }
        }
        HeroBase target = null;

        switch (CurrentStrategy)
        {
            case AttackStrategy.Bloodthirst:
                //Lowest HP
                target = heroes.OrderBy(t => (t.HealthCurrent))
                    .FirstOrDefault();
                break;
            case AttackStrategy.Closest:                
                target = heroes.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
                   .FirstOrDefault();
                break;
            case AttackStrategy.Glory:
                //Most HP
                target = heroes.OrderByDescending(t => (t.transform.GetComponent<HeroBase>().HealthCurrent))
                    .FirstOrDefault();
                break;
            case AttackStrategy.HeroType:
                target = heroes.OrderBy(t => (t.transform.GetComponent<HeroBase>().HeroType == HeroThirst)).FirstOrDefault();
                break;
            default:
                break;
        }

        print("Scount found:" + target.CharacterName);
        return target;

    }


 



}

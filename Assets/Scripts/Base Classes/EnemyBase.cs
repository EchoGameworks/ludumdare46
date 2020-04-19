using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AudioManager;
using static HeroBase;

public class EnemyBase : CharacterBase
{     

    public enum AttackStrategy { Closest, HeroType, Bloodthirst, Glory }

    [Header("Enemy Stats")]
    public HeroTypes HeroThirst;
    public AttackStrategy CurrentStrategy;
    public LayerMask CaravanLM;

    private float ScoutTimerMax = 2f;
    [Header("Enemy Runtime")]
    public float ScoutTimer;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (StatusCurrent == CharacterStatus.Idle) return;

        CharacterBase newTarget = null;
        if(ScoutTimer > 0f)
        {
            ScoutTimer -= Time.deltaTime;
        }
        else if(AttackTarget == null)
        {
            ScoutTimer = ScoutTimerMax;

            newTarget = Scout();            
            if (newTarget != AttackTarget)
            {
                AudioManager.instance.PlaySound(spawnSound);
            }
            AttackTarget = newTarget;
        }

        if(AttackTarget != null)
        {
            Pursue();
        }
    }


    public HeroBase Scout()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 100f, CaravanLM);
       
        List<HeroBase> heroes = new List<HeroBase>();
        foreach(Collider c in hitColliders)
        {
            HeroBase hb = c.GetComponent<HeroBase>();
            if(hb != null)
            {
                
                if (hb.HealthCurrent > 0f)
                {
                    
                    heroes.Add(hb);
                }               
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

        if(target == null)
        {             
            target = heroes.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
                .FirstOrDefault();
        }
        //if(target != null) print("Scount found:" + target.CharacterName);
        return target;

    }

    public override void Die()
    {
        base.Die();
        LeanTween.scale(gameObject, Vector3.zero, 0.3f).setOnComplete(() => Destroy(gameObject));
    }




}

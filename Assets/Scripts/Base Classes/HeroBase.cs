using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroBase : CharacterBase
{
    public enum HeroTypes { Leader, Priest, Rogue, Brute, Tree }

    [Header("Hero Stats")]
    public HeroTypes HeroType;
    public Vector3 StartingPosition;

    private StageManager stageManager;
    private float scoutTimer;
    private float scoutTimerMax;
    private bool isDead = false;

    protected override void Start()
    {
        base.Start();
        stageManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<StageManager>();
        scoutTimerMax = 0.5f;
        scoutTimer = scoutTimerMax;
    }

    private void Update()
    {
        //if(StatusCurrent == CharacterStatus.Attacking)
        //{
        if (scoutTimer >= 0)
        {
            scoutTimer -= Time.deltaTime;
        }
        else
        {
            AttackTarget = Scout();
            scoutTimer = scoutTimerMax;
        }
            
        //}

        if (AttackTarget != null)
        {
            PrepareAttack();
            //Attack(AttackTarget);
        }
    }

    public EnemyBase Scout()
    {
        //print("scouting");
        EnemyBase target = null;
        List<EnemyBase> enemies = new List<EnemyBase>();
        foreach(CharacterBase cb in uiAttackArea.AttackableCharacters)
        {
            if(cb != null)
            {
                EnemyBase eb = cb.GetComponent<EnemyBase>();
                if (eb != null)
                {
                    if (eb.HealthCurrent > 0)
                    {
                        enemies.Add(eb);
                    }

                }
            }
         
        }
        if(enemies != null) 
        {
            //print("enemy count: " + enemies.Count);
            if(enemies.Count > 0)
            {
                target = enemies.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
                .FirstOrDefault();
            }
        }

        List<HeroBase> heroes = new List<HeroBase>();
        foreach (CharacterBase cb in uiAttackArea.AttackableCharacters)
        {
            if (cb != null)
            {
                HeroBase hb = cb.GetComponent<HeroBase>();
                if (hb != null)
                {
                    heroes.Add(hb);
                }
            }
        }

        return target;
    }

    public override void Die()
    {
        base.Die();

        if (HeroType == HeroTypes.Tree)
        {
            stageManager.Defeat();
           // LeanTween.scale(gameObject, Vector3.zero, 0.3f).setOnComplete(() => Destroy(gameObject));
        }
        else
        {
            if (!isDead)
            {
                isDead = true;
                LeanTween.scale(gameObject, Vector3.zero, 0.3f).setOnComplete(() => Destroy(gameObject));
            }
        }

        
    }

    public override void Move(Vector3 location)
    {
        base.Move(location);

        //#FIX
        if (location != nma.steeringTarget)
        {
            //print("sounding");
            AudioManager.instance.PlaySound(AudioManager.SoundEffects.Order);
        }

    }
}

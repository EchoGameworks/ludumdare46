using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static AudioManager;

public class CharacterBase : SelectableBase
{
    public enum CharacterStatus { Moving, Attacking, Sick, Idle }
    public enum CharacterSickness { Severe, Moderate, Mild, Water, None }

    [HideInInspector]
    public float HealthMaxGlobal = 200f;

    [Header("Presets")]
    public Color Blue;
    public Color Red;
    public Color Grey;
    public Color Green;

    [Header("General Stats")]
    public string CharacterName;

    [TextArea(3, 10)]
    public string Description;
    public string StatusDescription;
    public CharacterStatus StatusCurrent;
    public CharacterSickness SicknessLevel;
    public float HealthMax = 200f;
    public float HealthCurrent;
    public float AttackTimeMax;
    public float MoveSpeedMax = 8f;
    public float AccerlationMax = 6f;
    public float AttackDamage = 1f;
    public float AttackDistance = 1f;
    public float TurnSpeed = 200f;
    public SoundEffects attackSound;
    public SoundEffects spawnSound;
    public SoundEffects deathSound;


    [Header("General Runtime")]
    public CharacterBase AttackTarget;
    public float AttackTimeCurrent;

    [HideInInspector]
    public NavMeshAgent nma;
    //#FIX

    protected override void Start()
    {
        base.Start();

        //#FIX
        nma = GetComponent<NavMeshAgent>();
        nma.angularSpeed = TurnSpeed;
        nma.speed = MoveSpeedMax;
        nma.acceleration = AccerlationMax;
        if (HealthMax > HealthMaxGlobal) Debug.LogWarning("Max Health above Global");
        HealthCurrent = HealthMax;
        uiStatus.SetName(CharacterName);
        uiStatus.SetHealth(this);
        uiAttackArea.SetAttackAreaSize(AttackDistance);
        StatusCurrent = CharacterStatus.Attacking;

        SetSickness(SicknessLevel);
    }

    void Update()
    {
        //if (nma.steeringTarget != null && nma.remainingDistance < 2f)
        //{
        //    StatusCurrent = CharacterStatus.Attacking;
        //    //print("at target");
        //}
        //else
        //{
        //    StatusCurrent = CharacterStatus.Moving;
        //}
    }

    public override void Select()
    {
        base.Select();
        if (uiOverlay != null) uiOverlay.SetShowToolTip(CharacterName, Description, StatusDescription);
    }

    public void Pursue()
    {
        CharacterBase targetClose = uiAttackArea.AttackableCharacters.Where(o => o == AttackTarget).FirstOrDefault();
        if (targetClose)
        {
            StatusCurrent = CharacterStatus.Attacking;
            //#FIX
            nma.isStopped = true;
            PrepareAttack();
        }
        else
        {
            StatusCurrent = CharacterStatus.Moving;
            Move(AttackTarget.transform.position);
        }
    }

    public void PrepareAttack()
    {
        if (StatusCurrent == CharacterStatus.Sick) return;
        if (AttackTimeCurrent > 0)
        {
            AttackTimeCurrent -= Time.deltaTime;
        }
        else
        {
            AttackTimeCurrent = AttackTimeMax;
            Attack(AttackTarget);
        }
    }

    public void Attack(CharacterBase target)
    {
        //attack effect
        target.TakeDamage(AttackDamage);
        AudioManager.instance.PlaySound(attackSound, true);
        uiStatus.AttackFlash(Red);
        if (target.HealthCurrent <= 0)
        {
            AttackTarget = null;
        }

    }

    public void TakeDamage(float dmg)
    {
        HealthCurrent -= dmg;
        if (HealthCurrent > HealthMax) HealthCurrent = HealthMax;
        if (HealthCurrent <= 0)
        {
            HealthCurrent = 0f;
            Die();
        }
        uiStatus.TakeTemporaryDamage();
    }

    public virtual void Die()
    {
        AudioManager.instance.PlaySound(deathSound);
    }

    public void Revive()
    {
        StatusCurrent = CharacterStatus.Attacking;
        SetSickness(CharacterSickness.None);
        uiStatus.HideRevive();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Caravan");
        foreach (GameObject go in gos)
        {
            HeroBase hb = go.GetComponent<HeroBase>();
            if (hb != null)
            {
                if(hb.HeroType == HeroBase.HeroTypes.Tree)
                {
                    hb.SetSickness(CharacterSickness.None);
                }
            }
        }

        CheckWin();
    }
    
    public void CheckWin()
    {
        print("Got to check win");
        bool isWin = true;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Caravan");
        int heroCount = 0;
        foreach (GameObject go in gos)
        {
            HeroBase hb = go.GetComponent<HeroBase>();
            if (hb != null)
            {
                if(hb.StatusCurrent == CharacterStatus.Sick)
                {
                    
                    isWin = false;
                    break;
                }
                heroCount++;
            }
        }

        if (isWin)
        {
            //Game Over
            if(heroCount == 5)
            {
                uiOverlay.ShowGameOver("You Won!", "You saved everyone! Grats - that's a really tough thing to do.");
            }
            else
            {
                uiOverlay.ShowGameOver("You Won!", "Looks like you lost some people along the way though...you should try again!");
            }
            AudioManager.instance.PlaySound(SoundEffects.Win);
        }
    }

    public void SetSickness(CharacterSickness sickness)
    {
        SicknessLevel = sickness;
        switch (SicknessLevel)
        {
            case CharacterSickness.Mild:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = Red;
                StatusDescription = "<size=55%><color=#00CEFF>" + CharacterName + "</color> is <color=#FF6161>moderately injured</color>. They can't move or attack.";
                StatusCurrent = CharacterStatus.Sick;
                break;
            case CharacterSickness.Moderate:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = Red;
                StatusDescription = "<size=55%><color=#00CEFF>" + CharacterName + "</color> is <color=#FF6161>heavily injured</color>. They can't move or attack.";
                IsPlayerMovable = false;
                StatusCurrent = CharacterStatus.Sick;
                break;
            case CharacterSickness.Severe:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = Red;
                StatusDescription = "<size=55%><color=#00CEFF>" + CharacterName + "</color> is <color=#FF6161>gravely injured</color>. They can't move or attack.";
                StatusCurrent = CharacterStatus.Sick;
                break;
            case CharacterSickness.Water:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = Blue; //new Color(206, 255, 255); //aqua
                StatusDescription = "<size=55%><color=#00CEFF>Genesis</color> produced <color=#00CEFF>Magic Fruit</color>. Use it to heal an ally.</size>";
                StatusCurrent = CharacterStatus.Attacking;
                break;
            case CharacterSickness.None:
                uiStatus.SicknessIcon.gameObject.SetActive(false);
                StatusCurrent = CharacterStatus.Attacking;
                StatusDescription = "";
                break;
        }
    }

    public virtual void Move(Vector3 location)
    {
        if (StatusCurrent == CharacterStatus.Sick) return;
        //#FIX
        if (nma.isActiveAndEnabled && nma.isOnNavMesh)
        {
            nma.isStopped = false;
            nma.SetDestination(location);
        }

        StatusCurrent = CharacterStatus.Moving;
    }

}



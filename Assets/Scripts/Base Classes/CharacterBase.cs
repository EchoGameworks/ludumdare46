using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBase : SelectableBase
{
    public enum CharacterStatus { Moving, Attacking }
    public enum CharacterSickness { Severe, Moderate, Mild, Water, None }

    [HideInInspector]
    public float HealthMaxGlobal = 200f;

    [Header("Presets")]
    public Color Blue;
    public Color Red;

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
    public float MoveSpeedMax = 3.5f;
    public float AttackDamage = 1f;
    public float AttackDistance = 1f;
    public float TurnSpeed = 160f;

    [Header("General Runtime")]
    public CharacterBase AttackTarget;
    public float AttackTimeCurrent;

    [HideInInspector]
    public NavMeshAgent nma;

    protected override void Start()
    {
        base.Start();
        nma = GetComponent<NavMeshAgent>();
        nma.angularSpeed = TurnSpeed;
        nma.speed = MoveSpeedMax;
        if (HealthMax > HealthMaxGlobal) Debug.LogWarning("Max Health above Global");
        HealthCurrent = HealthMax;
        uiStatus.SetName(CharacterName);
        uiStatus.SetHealth(this);
        uiAttackArea.SetAttackAreaSize(AttackDistance);
        StatusCurrent = CharacterStatus.Attacking;
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
        if(AttackTimeCurrent > 0)
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
        if(target.HealthCurrent < 0)
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
        //die effect
    }


    public void SetSickness(CharacterSickness sickness)
    {
        SicknessLevel = sickness;
        switch (SicknessLevel)
        {
            case CharacterSickness.Mild:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = new Color(255, 97, 97);
                StatusDescription = "<size=40%> <color=#00CEFF>" + CharacterName + "</color> is gravely injured.";
                break;
            case CharacterSickness.Moderate:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = new Color(255, 97, 97);
                StatusDescription = "<size=40%> <color=#00CEFF>" + CharacterName + "</color> is gravely injured.";
                break;
            case CharacterSickness.Severe:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = new Color(255, 97, 97);
                StatusDescription = "<size=40%> <color=#00CEFF>" + CharacterName + "</color> is gravely injured.";
                break;
            case CharacterSickness.Water:
                uiStatus.SicknessIcon.gameObject.SetActive(true);
                uiStatus.SicknessIcon.color = Blue; //new Color(206, 255, 255); //aqua
                StatusDescription = "<size=40%> <color=#00CEFF>Genesis</color> produced a <color=#00CEFF>Magic Fruit</color>. Take it back west to heal an ally. </size>";
                break;
            case CharacterSickness.None:
                uiStatus.SicknessIcon.gameObject.SetActive(false);
                break;
        }
    }

    public virtual void Move(Vector3 location)
    {
        if (!IsPlayerMovable) return;
        nma.SetDestination(location);
        StatusCurrent = CharacterStatus.Moving;
    }

}



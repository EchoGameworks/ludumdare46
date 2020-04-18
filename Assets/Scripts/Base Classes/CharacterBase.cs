using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBase : SelectableBase
{
    public enum CharacterStatus { Moving, Attacking }
    public enum CharacterSickness { Severe, Moderate, Mild, None }

    [HideInInspector]
    public float HealthMaxGlobal = 200f;

    [Header("General Stats")]
    public string CharacterName;

    [TextArea(3, 10)]
    public string Description;
    public CharacterStatus StatusCurrent;
    public CharacterSickness SicknessLevel;
    public float HealthMax = 200f;
    public float HealthCurrent;
    public float AttackTimeMax;
    public float AttackTimeCurrent;
    public float MoveSpeedMax = 3.5f;
    public float AttackDamage = 1f;
    public float AttackDistance = 1f;
    public float TurnSpeed = 160f;

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
        if (nma.steeringTarget != null && nma.remainingDistance < 4f)
        {
            StatusCurrent = CharacterStatus.Attacking;
            //print("at target");
        }
        else
        {
            StatusCurrent = CharacterStatus.Moving;
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

    public void Attack(CharacterBase target)
    {
        //attack effect
        target.TakeDamage(AttackDamage);
    }

    protected virtual void Die()
    {
        //die effect
    }


    public void Move(Vector3 location)
    {
        if (!IsPlayerMovable) return;
        print("moving - " + location);
        nma.SetDestination(location);
        StatusCurrent = CharacterStatus.Moving;
    }

}



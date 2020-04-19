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


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(StatusCurrent == CharacterStatus.Attacking)
        {
            AttackTarget = Scout();
        }

        if (AttackTarget != null)
        {
            Attack(AttackTarget);
        }
    }

    public EnemyBase Scout()
    {
        EnemyBase target = null;
        List<EnemyBase> enemies = new List<EnemyBase>();
        foreach(CharacterBase cb in uiAttackArea.AttackableCharacters)
        {
            EnemyBase eb = cb.GetComponent<EnemyBase>();
            enemies.Add(eb);
        }

        target = enemies.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
            .FirstOrDefault();

        return target;
    }

    public override void Die()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.3f).setOnComplete(() => Destroy(gameObject));
    }

    public override void Move(Vector3 location)
    {
        base.Move(location);
        if(location != nma.steeringTarget)
        {
            print("sounding");
            AudioManager.instance.PlaySound(AudioManager.SoundEffects.Order);
        }
        
    }
}

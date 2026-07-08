using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Basic Attack")]
public class BasicAttack : Skill
{
    [Header("Damage Multiplier")]
    public float damageMultiplier = 1f;


    //animation
    public override IEnumerator Use(Unit caster, 
                                    Unit[] target)
    {
        // unit walk into target
        Vector3 OriginalLocation = caster.unitPosition.position;
        Vector3 TargetLocation = target[target.Length/2].unitPosition.position;
        TargetLocation.z -= 1;
        Quaternion OriginalRotation = caster.unitPosition.rotation;
        Quaternion faceToward = target[target.Length/2].unitPosition.rotation;
        caster.WalkToPosition(TargetLocation,
                              faceToward,
                              5f);
        yield return new WaitUntil(() => caster.AnimationFinished);
        caster.animator.SetTrigger("Attack");
        yield return new WaitUntil(() => caster.AnimationFinished);
        caster.WalkToPosition(OriginalLocation,
                              OriginalRotation,
                              5f);
    }

    //effect
    public override void Effect(Unit caster,
                         Unit[] target)
    {
        float damage = caster.currentATK * damageMultiplier;
        foreach (Unit unit in target)
        {
            unit.TakeDamage(damage);
        }
    }
}
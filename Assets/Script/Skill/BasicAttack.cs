 using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Basic Attack")]
public class BasicAttack : Skill
{
    [Header("Damage Multiplier")]
    public float damageMultiplier = 1f;

    public override void Use(Unit caster, 
                             Unit[] target)
    {
        StartCoroutine(Use(caster,target));
    }

    private override IEnumerator Use(Unit caster, 
                                     Unit[] target)
    {
        Vector3 OriginalLocation = caster.unitPosition.position;
            Vector3 TargetLocation = target.unitPosition[target.Length/2].position;
        TargetLocation.z -= 1f;
        caster.WalkToPosition(TargetLocation,
                              5f);
        yield return null;
        caster.Animator.SetTrigger["Attack"];
        yield return new WaitUntil(() => caster.AnimationFinished);
        caster.WalkToPosition(OriginalLocation,
                              5f);
    }

    public void doDamage(Unit caster,
                         Unit[] target)
    {
        float damage = caster.currentATK * damageMultiplier;
        foreach (Unit unit in target)
        {
            unit.TakeDamage(damage);
        }
    }
}
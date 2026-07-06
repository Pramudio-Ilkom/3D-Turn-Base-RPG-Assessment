 using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Basic Attack")]
public class BasicAttack : Skill
{
    public override void Use(Unit caster, Unit[] target)
    {
        float damage = caster.currentATK;
        foreach (Unit unit in target)
        {
            unit.TakeDamage(damage);
        }
    }
}
 using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Basic Attack")]
public class BasicAttack : Skill
{
    [Header("Damage Multiplier")]
    public float damageMultiplier = 1f;

    public override void Use(Unit caster, Unit[] target)
    {
        float damage = caster.currentATK * damageMultiplier;
        foreach (Unit unit in target)
        {
            unit.TakeDamage(damage);
        }
    }
}
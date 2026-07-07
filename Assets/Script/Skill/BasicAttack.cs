 using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Attack", menuName = "Skill/Basic Attack")]
public class BasicAttack : Skill
{
    [Header("Skill Name")]
    public string skillName = "Basic Attack";
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
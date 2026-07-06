 using unityEngine;

[CreateAssetMenu(menuName = "Skill/Basic Attack")]
public class BasicAttack : Skill
{
    bool isCasterFriendly;
    bool isTargetFriendly;

    public override void Use(Unit caster, Unit[] target)
    {
        float damage = caster.ATK;
        foreach (Unit unit in target)
        {
            unit.TakeDamage(damage);
        }
    }
}
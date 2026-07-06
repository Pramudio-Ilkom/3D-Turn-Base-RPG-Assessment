 using unityEngine;

[CreateAssetMenu(menuName = "Skill/Heal")]
public class Heal : Skill
{
    bool isCasterFriendly;
    bool isTargetFriendly;

    public override void Use(Unit caster, Unit[] target)
    {
        float heal = caster.ATK*0.6f;
        foreach (Unit unit in target)
        {
            unit.Heal(heal);
        }
    }
}
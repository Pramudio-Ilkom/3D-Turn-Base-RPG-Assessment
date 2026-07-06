 using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Heal")]
public class Heal : Skill
{
    public override void Use(Unit caster, Unit[] target)
    {
        float heal = caster.currentATK * 0.6f;
        foreach (Unit unit in target)
        {
            unit.Heal(heal);
        }
    }
}
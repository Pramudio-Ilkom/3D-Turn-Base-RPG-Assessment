 using UnityEngine;

[CreateAssetMenu(fileName = "New Heal", menuName = "Skill/Heal")]
public class Heal : Skill
{
    [Header("Skill Name")]
    public string skillName = "Heal";
    [Header("Heal Multiplier")]
    public float healMultiplier = 0.6f;

    public override void Use(Unit caster, Unit[] target)
    {
        float heal = caster.currentATK * healMultiplier;
        foreach (Unit unit in target)
        {
            unit.Heal(heal);
        }
    }
}
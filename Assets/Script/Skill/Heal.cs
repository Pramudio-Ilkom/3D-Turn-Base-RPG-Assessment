 using System.Collections;
 using UnityEngine;
[CreateAssetMenu(menuName = "Skill/Heal")]
public class Heal : Skill
{
    [Header("Heal Multiplier")]
    public float healMultiplier = 0.6f;

    public override IEnumerator Use(Unit caster, 
                                    Unit[] target)
    {
        // float heal = caster.currentATK * healMultiplier;
        // foreach (Unit unit in target)
        // {
        //     unit.Heal(heal);
        // }
        yield return null;
    }

    
    public override void Effect(Unit caster,
                         Unit[] target)
    {
        float heal = caster.currentATK * healMultiplier;
        foreach (Unit unit in target)
        {
            unit.Heal(heal);
        }
    }
}
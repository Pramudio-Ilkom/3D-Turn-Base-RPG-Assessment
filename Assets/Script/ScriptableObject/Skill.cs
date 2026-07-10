using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public abstract class Skill : ScriptableObject
{
    [Header("Skill Name")]
    public string skillName;
    [Header("Skill Properties")]
    public bool Damage;
    // public bool isTargetFriendly;
    // public bool isSingleTarget;
    [Header("Skill Description")]
    public string skillDescription;

    public abstract IEnumerator Use(Unit caster, 
                                    Unit[] target);
    public abstract void Effect(Unit caster, 
                                Unit[] target);
}
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public abstract class Skill : ScriptableObject
{
    [Header("Skill Name")]
    public string skillName;
    [Header("Skill Properties")]
    public bool isCasterFriendly;
    public bool isTargetFriendly;
    public bool isSingleTarget;
    [Header("Skill Description")]
    public string skillDescription;

    public abstract void Use(Unit caster, Unit[] target);

}
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public abstract class Skill : ScriptableObject
{
    public string skillName;
    public bool isCasterFriendly;
    public bool isTargetFriendly;
    public bool isSingleTarget;

    public abstract void Use(Unit caster, Unit[] target);

}
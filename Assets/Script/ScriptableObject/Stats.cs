using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class Stats : ScriptableObject 
{
    // Normal HP is at 1000
    [Header("HP")]
    public float HP;
    // Normal ATK 300
    [Header("ATK")]
    public float ATK;
    // Normal Def 40 and 100 give 100% effective HP
    [Header("DEF")]
    public float DEF;
    // Normal SPD 100
    [Header("SPD")]
    public float SPD;
    [Header("Friendly or Enemy")]
    public bool Friendly = true;
    [Header("Starting Skill")]
    public Skill[] startingSkill;
}

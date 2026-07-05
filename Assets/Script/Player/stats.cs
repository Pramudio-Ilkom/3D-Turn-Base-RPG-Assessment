using UnityEngine;

public class stats : MonoBehaviour {
    // Normal HP is at 1000
    [Header("HP")]
    public float MaxHP = 1000;
    public float HP;
    // Normal ATK 300
    [Header("ATK")]
    public float MaxATK = 300;
    public float ATK;
    // Normal Def 40 and 100 give 100% effective HP
    [Header("DEF")]
    public float MaxDEF = 40;
    public float DEF;
    // Normal SPD 100
    [Header("SPD")]
    public float MaxSPD = 100;
    public float SPD;
    [Header("Friendly or Enemy")]
    public bool Friendly = true;

    private void Awake()
        {
            // if(HP == null)
            // {
            //     HP = MaxHP;
            // }
            // if(ATK == null)
            // {
            //     ATK = MaxATK;
            // }
            // if(DEF == null)
            // {
            //     DEF = MaxDEF;
            // }
            // if(SPD == null)
            // {
            //     SPD = MaxSPD;
            // }
        }
    void TakeDamage(float Damage)
    {
        HP = (HP*DEFCalculation(DEF) - Damage)/DEFCalculation(DEF);
        if(HP < 0)
        {
            HP = 0;
        }
    }

    public float ActionValue()
    {
        return 10000/SPD;
    }

    float DEFCalculation(float Def)
    {
        return 1+(Def/100);
    }

}

using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Battle Manager")]
    public GameBattleManager battleManager;
    public PostProcessingController postProcessingManager;
    [Header("Unit Properties")]
    public Stats stats;
    public Renderer meshRenderer;
    public Animator animator;
    public bool AnimationFinished = true;
    public Transform unitPosition;
    [Header("Skills")]
    public Skill[] skills;
    public Skill SelectedSkill;
    public Unit[] Target;

    // Current stats
    public float currentHP;
    public float currentATK;
    public float currentDEF;
    public float currentSPD;

    [Header("Status")]
    public bool isPlayer;
    public bool isAlive;
    public bool isFriendly;
    public bool Turn = false;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        unitPosition = GetComponent<Transform>();
        battleManager = FindFirstObjectByType<GameBattleManager>();
        postProcessingManager = FindFirstObjectByType<PostProcessingController>();
        currentHP = stats.HP;
        currentATK = stats.ATK;
        currentDEF = stats.DEF;
        currentSPD = stats.SPD;
        isPlayer = stats.Player;
        isFriendly = stats.Friendly;
        skills = stats.startingSkill;
        isAlive = true;
    }

    public float ActionValue()
    {
        return 10000 / currentSPD;
    }

    // HP Modifier //
    public void TakeDamage(float damage)
    {
        currentHP = ((currentHP*DefCalculation(currentDEF)) - damage)/DefCalculation(currentDEF);
        animator.SetTrigger("Hit");
        if(isPlayer && isFriendly)
        {
            postProcessingManager.TemporaryEffect(2f, 
                                                  0.5f,
                                                  "Bloom");
            postProcessingManager.TemporaryEffect(0.5f, 
                                                  0.5f,
                                                  "Vignette");
            postProcessingManager.TemporaryEffect(0.3f, 
                                                  0.5f,
                                                  "ColorAdjustmentsRed",0.5f);
            // Debug.Log("Current HP: " + currentHP + ", Max HP: " + stats.HP);
            if(currentHP <= stats.HP * 0.3f)
            {
                float HPPercentage = currentHP / stats.HP;
                float VignetteIntensityMultiplier = 0.6f - HPPercentage;
                Color ColorMultiplier = new Color(1f, HPPercentage*2f, HPPercentage*2f, 0f);
                postProcessingManager.VignetteChange(VignetteIntensityMultiplier, 0.5f);
                postProcessingManager.ColorAdjustmentsChange(ColorMultiplier, 0.5f);
            }
        }
        else if(!isPlayer && isFriendly)
        {
            postProcessingManager.TemporaryEffect(2f, 
                                                  0.5f,
                                                  "Bloom");
            postProcessingManager.TemporaryEffect(1f, 
                                                  0.5f,
                                                  "Vignette");
            postProcessingManager.TemporaryEffect(1f, 
                                                  0.5f,
                                                  "ColorAdjustmentsIntensity");
        }
        else
        {
            postProcessingManager.TemporaryEffect(2f, 
                                                  0.5f,
                                                  "Bloom");
            postProcessingManager.TemporaryEffect(1f, 
                                                  0.5f,
                                                  "ColorAdjustmentsIntensity");
        }
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath();
        }
    }

    public void Heal(float heal)
    {
        currentHP += heal;
        if (currentHP > stats.HP)
        {
            currentHP = stats.HP;
        }
    }

    private float DefCalculation(float def)
    {
        return 1+(def/100);
    }


    // Buff methods // 
    public void HPBuff(float hp)
    {
        currentHP += stats.HP * hp;
    }

    public void AtkBuff(float atk)
    {
        currentATK += stats.ATK * atk;
    }

    public void DefBuff(float def)
    {
        currentDEF += stats.DEF * def;
    }

    public void SpdBuff(float spd)
    {
        currentSPD += stats.SPD * spd;
    }

    public void UseSkill(int skillIndex, 
                         Unit[] target)
    {
        Debug.Log("Skill Started");
        if (skillIndex >= 0 && skillIndex < skills.Length)
        {
            SelectedSkill = skills[skillIndex];
            Target = target;
            StartCoroutine(SelectedSkill.Use(this, target));
        }
        else
        {
            Debug.Log("Invalid skill index: " + skillIndex);
        }
    }

    public void SkillEffect()
    {
        Debug.Log("Skill Affected");
        if (Target != null)
        {
            SelectedSkill.Effect(this, Target);
        }
        else
        {
            Debug.Log("No Target");
        }
        Target = null;
    }

    //interaction


    //Animation Related
    public void AnimationStart()
    {
        AnimationFinished = false;
    }

    public void AnimationFinish()
    {
        AnimationFinished = true;
    }
    public void IsWalking(bool isWalking)
    {
        animator.SetBool("Walk", isWalking);
    }   

    public void WalkToPosition(Vector3 targetPosition,
                                       Quaternion faceToward,
                                       float speed = 5f)
    {
        StartCoroutine(WalkToPositionCoroutine(targetPosition,
                                               faceToward, 
                                               speed));
    }

    private IEnumerator WalkToPositionCoroutine(Vector3 targetPosition,
                                                Quaternion faceToward,
                                                float speed = 5f)
    {
        AnimationStart();
        Vector3 WalkTarget = targetPosition;
        Quaternion targetRotation = Quaternion.LookRotation(WalkTarget - unitPosition.position);
        animator.SetTrigger("Stop");
        while (Quaternion.Angle(unitPosition.rotation, targetRotation) > 0.1f)
        {
            unitPosition.rotation = Quaternion.RotateTowards(
                unitPosition.rotation,
                targetRotation,
                180f * Time.deltaTime); // degrees per second
            yield return null;
        }
        unitPosition.rotation = targetRotation;
        Debug.Log("Its Done 1");
        animator.SetBool("Walk", true);
        while (Vector3.Distance(unitPosition.position, 
                                WalkTarget) > 0.1f)
        {
            unitPosition.position = Vector3.MoveTowards(unitPosition.position, 
                                                        WalkTarget, 
                                                        speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Its Done 2");
        animator.SetBool("Walk", false);
        while (Quaternion.Angle(unitPosition.rotation, faceToward) > 0.1f)
        {
            unitPosition.rotation = Quaternion.RotateTowards(
                unitPosition.rotation,
                faceToward,
                180f * Time.deltaTime);

            yield return null;
        }
        unitPosition.rotation = faceToward;
        unitPosition.position = WalkTarget;
        AnimationFinish();
        Debug.Log("Its Done 3");
    }

    private void OnDeath()
    {
        isAlive = false;
        animator.SetBool("Death", true);
        // Additional logic for when the unit dies (e.g., play death animation, drop loot, etc.)
    }
}
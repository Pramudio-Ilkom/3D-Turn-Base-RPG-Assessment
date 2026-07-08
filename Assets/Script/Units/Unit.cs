    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        [Header("Battle Manager")]
        public GameBattleManager battleManager;
        public PostProcessingController postProcessingManager;
        [Header("Unit Properties")]
        public Stats stats;
        private Renderer meshRenderer;
        private Animator Animator;
        public bool AnimationFinished = true;
        private Transform unitPosition;
        [Header("Skills")]
        public Skill[] skills;

        // Current stats
        public float currentHP;
        public float currentATK;
        public float currentDEF;
        public float currentSPD;

        [Header("Status")]
        public bool isPlayer;
        public bool isAlive;
        public bool isFriendly;

        private void Awake()
        {
            meshRenderer = GetComponent<Renderer>();
            Animator = GetComponent<Animator>();
            unitPosition = GetComponent<Transform>();
            battleManager = FindFirstObjectByType<GameBattleManager>();
            postProcessingManager = FindFirstObjectByType<PostProcessingController>();
            currentHP = stats.HP;
            currentATK = stats.ATK;
            currentDEF = stats.DEF;
            currentSPD = stats.SPD;
            isPlayer = stats.Player;
            isFriendly = stats.Friendly;
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
            Animator.SetTrigger("TakeDamage");
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
            if (skillIndex >= 0 && skillIndex < skills.Length)
            {
                Skill skill = skills[skillIndex];
                skill.Use(this, target);
            }
            else
            {
                Debug.LogError("Invalid skill index: " + skillIndex);
            }
        }

        //interaction
        public void InteractDamage(Unit[] targetUnit, 
                                   float Damage)
        {
            // Implement interaction logic here
            foreach (Unit unit in targetUnit)
            {
                unit.TakeDamage(Damage);
            }
        }

        public void InteractHeal(Unit[] targetUnit, 
                                 float Heal)
        {
            // Implement interaction logic here
            foreach (Unit unit in targetUnit)
            {
                unit.Heal(Heal);
            }
        }


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
            Animator.SetBool("IsWalking", isWalking);
        }   

        public void WalkToPosition(Vector3 targetPosition, 
                                   float speed = 5f)
        {
            StartCoroutine(WalkToPositionCoroutine(targetPosition, speed));
        }

        private IEnumerator WalkToPosition(Vector3 targetPosition, 
                                           float speed = 5f)
        {
            while (unitPosition.rotation != Quaternion.LookRotation(targetPosition - unitPosition.position))
            {
                unitPosition.rotation = Quaternion.RotateTowards(unitPosition.rotation, 
                                                                 Quaternion.LookRotation(targetPosition - unitPosition.position), 
                                                                 speed * Time.deltaTime);
                yield return null;
            }
            unitPosition.rotation = Quaternion.LookRotation(targetPosition - unitPosition.position);
            Animator.SetBool("IsWalking", true);
            while (Vector3.Distance(unitPosition.position, 
                                    targetPosition) > 0.1f)
            {
                unitPosition.position = Vector3.MoveTowards(unitPosition.position, 
                                                            targetPosition, 
                                                            speed * Time.deltaTime);
                yield return null;
            }
            unitPosition.position = targetPosition;
            Animator.SetBool("IsWalking", false);
        }

        private void OnDeath()
        {
            isAlive = false;
            Animator.SetBool("Death", true);
            // Additional logic for when the unit dies (e.g., play death animation, drop loot, etc.)
        }
    }
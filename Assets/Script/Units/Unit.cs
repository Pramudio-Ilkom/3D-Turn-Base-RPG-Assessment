    using unityEngine;

    public class Unit : MonoBehaviour
    {
        [Header("Battle Manager")]
        public GameBattleManager battleManager;
        public PostProcessingController postProcessingManager;
        [Header("Stats")]
        public Stats stats;
        [Header("Skills")]
        public Skill[] skills;

        // Current stats
        private float currentHP;
        private float currentATK;
        private float currentDEF;
        private float currentSPD;

        [Header("Status")]
        public bool isPlayer;
        public bool isAlive;
        public bool isFriendly;

        private void Awake()
        {
            battleManager = FindFirstObjectOfType<GameBattleManager>();
            postProcessingManager = FindFirstObjectOfType<PostProcessingController>();
            currentHP = stats.HP;
            currentATK = stats.ATK;
            currentDEF = stats.DEF;
            currentSPD = stats.SPD;
            isAlive = true;
        }

        // HP Modifier //
        public void TakeDamage(float damage)
        {
            currentHP = ((currentHP*DefCalculation(currentDEF)) - damage)/DefCalculation(currentDEF);
            if (currentHP <= 0)
            {
                currentHP = 0;
                isAlive = false;
            }
            if(isPlayer && isFriendly)
            {
                postProcessingManager.TemporaryEffect(2f, 0.5f,"Bloom");
                postProcessingManager.TemporaryEffect(2f, 0.5f,"Vignette");
                postProcessingManager.TemporaryEffect(2f, 0.5f,"ColorAdjustmentsRed");
                if(currentHP <= stats.HP * 0.3f)
                {
                    float HPPercentage = currentHP / stats.HP;
                    float VignetteIntensityMultiplier = (1f - HPPercentage)* 0.5f;
                    Color ColorMultiplier = new Color(1f, HPPercentage, HPPercentage, 0f);
                    postProcessingManager.VignetteChange(VignetteIntensityMultiplier, 0.5f);
                    postProcessingManager.ColorAdjustmentsChange(ColorMultiplier, 0.5f);
                }
            }
            elif(!isPlayer && isFriendly)
            {
                postProcessingManager.TemporaryEffect(2f, 0.5f,"Bloom");
                postProcessingManager.TemporaryEffect(1f, 0.5f,"Vignette");
                postProcessingManager.TemporaryEffect(1f, 0.5f,"ColorAdjustmentsIntensity");
            }
            else
            {
                postProcessingManager.TemporaryEffect(2f, 0.5f,"Bloom");
                postProcessingManager.TemporaryEffect(1f, 0.5f,"ColorAdjustmentsIntensity");
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

        public void UseSkill(int skillIndex, Unit[] target)
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
    }
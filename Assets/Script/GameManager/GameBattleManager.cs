using System.Data;
using System.Collections;

using UnityEngine;

public class GameBattleManager : MonoBehaviour
{
    [Header("Units")]
    private int number_of_units;
    public GameObject[] units;
    public Transform[] friendly_positions;
    public Transform[] enemy_positions;
    public Unit[] unit_stats;
    public int friendly_count = 0;
    public int enemy_count = 0;

    [Header("Action Order")]
    public float[] ActionOrder;
    int whoseturn;

    //testing

    private void Awake()
    {
        number_of_units = units.Length;
        ActionOrder = new float[number_of_units];
        unit_stats = new Unit[number_of_units];
        for (int i = 0; i < number_of_units; i++)
        {
            unit_stats[i] = units[i].GetComponent<Unit>();
        }
        for (int i = 0; i < number_of_units; i++)
        {
            if (unit_stats[i].isFriendly)
            {
                units[i].transform.position = friendly_positions[friendly_count].position;
                units[i].transform.rotation = friendly_positions[friendly_count].rotation;
                friendly_count++;
            }
            else
            {
                units[i].transform.position = enemy_positions[enemy_count].position;
                units[i].transform.rotation = enemy_positions[enemy_count].rotation;
                enemy_count++;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActionOrderStart();
        StartAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActionOrderStart()
    {
        for (int i = 0; i < number_of_units; i++)
        {
            ActionOrder[i] = unit_stats[i].ActionValue();
        }
        Debug.Log(ActionOrder);
    }
    void StartAction()
    {
        float MinimumValue = Mathf.Min(ActionOrder);
        for (int i = 0; i < number_of_units; i++)
        {
            if (ActionOrder[i] == MinimumValue)
            {
                whoseturn = i;
                ActionOrder[i] -= MinimumValue;
            }
            else
            {
                ActionOrder[i] -= MinimumValue;
            }
        }
        StartCoroutine(Action(whoseturn));
    }

    IEnumerator Action(int WhoseTurn)
    {
        Debug.Log("TurnStart");
        Unit CurrentTurn = unit_stats[WhoseTurn];

        CurrentTurn.Turn = true;
        if(CurrentTurn.isPlayer)
        {
            //there will be function here
        }
        else
        {
            int skillIndexUse = Random.Range(0,CurrentTurn.skills.Length-1);
            Unit[] Target = new Unit[1];
            if(CurrentTurn.skills[skillIndexUse].Damage)
            {
                Target[0] = unit_stats[CorrectTarget(CurrentTurn)];
                CurrentTurn.UseSkill(skillIndexUse, Target);
            }
            else
            {
                Target[0] = unit_stats[CorrectTarget(CurrentTurn,
                                                     false)];
                CurrentTurn.UseSkill(skillIndexUse, Target);
            }
        }
        yield return new WaitUntil(() => !CurrentTurn.Turn);
        ActionOrder[WhoseTurn] = CurrentTurn.ActionValue();
        StartAction();
        Debug.Log("Turn End");
    }

    int CorrectTarget(Unit unit, 
                      bool damage = true) //if true then it target its enemy
    {
        int TargetIndex = Random.Range(0,number_of_units);
        Unit Target = unit_stats[TargetIndex];
        if(damage)
        {
            if(unit.isFriendly)
            {
                if(Target.isFriendly)
                {
                    return CorrectTarget(unit);
                }
                else
                {
                    return TargetIndex;
                }
            }
            else
            {
                if(Target.isFriendly)
                {
                    return TargetIndex;
                }
                else
                {
                    return CorrectTarget(unit);
                }
            }
        }
        else
        {
            if(unit.isFriendly)
            {
                if(Target.isFriendly)
                {
                    return TargetIndex;
                }
                else
                {
                    return CorrectTarget(unit);
                }
            }
            else
            {
                if(Target.isFriendly)
                {
                    return CorrectTarget(unit);
                }
                else
                {
                    return TargetIndex;
                }
            }
        }
    }
}

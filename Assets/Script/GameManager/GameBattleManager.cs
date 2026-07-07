using System.Data;
using UnityEngine;

public class GameBattleManager : MonoBehaviour
{
    [Header("Units")]
    private int number_of_units;
    public GameObject[] units;
    public Transform[] friendly_positions;
    public Transform[] enemy_positions;
    Unit[] unit_stats;

    [Header("Action Order")]
    public float[] ActionOrder;
    int whoseturn;

    private void Awake()
    {
        number_of_units = units.Length;
        ActionOrder = new float[number_of_units];
        unit_stats = new Unit[number_of_units];
        int friendly_count = 0;
        int enemy_count = 0;
        for (int i = 0; i < number_of_units; i++)
        {
            unit_stats[i] = units[i].GetComponent<Unit>();
        }
        for (int i = 0; i < number_of_units; i++)
        {
            if (unit_stats[i].isFriendly)
            {
                units[i].transform.position = friendly_positions[friendly_count].position;
                friendly_count++;
            }
            else
            {
                units[i].transform.position = enemy_positions[enemy_count].position;
                enemy_count++;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActionOrderStart();
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
    }
    void StartAction()
    {
        float MinimumValue = Mathf.Min(ActionOrder);
        for (int i = 0; i < number_of_units; i++)
        {
            if (ActionOrder[i] == MinimumValue)
            {
                whoseturn = i;
            }
            else
            {
                ActionOrder[i] -= MinimumValue;
            }
        }
    }
}

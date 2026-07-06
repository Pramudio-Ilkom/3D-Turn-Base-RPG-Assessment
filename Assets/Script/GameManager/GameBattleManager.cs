using System.Data;
using UnityEngine;

public class GameBattleManager : MonoBehaviour
{
    [Header("Units")]
    public int number_of_units = 10;
    public GameObject[] units = new GameObject[10];
    public Transform[] unit_positions = new Transform[10];
    Unit[] unit_stats = new Unit[10];

    [Header("Action Order")]
    public float[] ActionOrder = new float[10];
    int whoseturn;

    private void Awake()
    {
        for (int i = 0; i < number_of_units; i++)
        {
            units[i].transform.position = unit_positions[i].position;
        }
        for (int i = 0; i < number_of_units; i++)
        {
            unit_stats[i] = units[i].GetComponent<Unit>();
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
                ActionOrder[i] += unit_stats[i].ActionValue();
            }
            ActionOrder[i] -= MinimumValue;
        }
    }
}

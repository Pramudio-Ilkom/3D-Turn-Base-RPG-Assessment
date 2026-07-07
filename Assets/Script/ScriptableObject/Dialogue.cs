using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject 
{
    [Header("Dialogue Name")]
    public string dialogueName;
    [Header("Dialogue")]
    public string[] dialogue;
}

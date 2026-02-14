using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "Game/Currency")]
public class Currency : ScriptableObject
{
    public string description;
    public Sprite icon;
    public int quantity;
}

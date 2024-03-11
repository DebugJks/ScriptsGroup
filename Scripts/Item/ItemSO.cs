using UnityEngine;


        // 게임내 아이템들
[CreateAssetMenu(menuName = "SO/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int itemId;
}

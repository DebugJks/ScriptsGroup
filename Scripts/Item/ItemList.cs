using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemList")]
public class ItemList : ScriptableObject
{
    public List<ItemSO> items;
}   
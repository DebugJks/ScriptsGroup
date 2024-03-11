using UnityEngine;
using StringSpace;

public class ItemObjs : MonoBehaviour
{
    [SerializeField] ItemSO item;   // 오브젝트 아이템 정보
    public int count = 1;   // 아이템 기본 증가 수량



    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag(StringSpaces.Player))
        {
            AddItems();
            
            gameObject.SetActive(false);
        }
    }

    void AddItems()
    {
        AudioManagerS.intance.PlaySoundEffect(AudioManagerS.Sfx.Alert);
        GameManagers.instance.inventory.Add(item, count);
    }
}

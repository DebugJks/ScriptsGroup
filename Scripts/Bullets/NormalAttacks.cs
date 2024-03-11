using UnityEngine;
using StringSpace;

public class NormalAttacks : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(StringSpaces.Player))
        {
            GameManagers.instance.player.OnHit(damage);
        }
    }
}
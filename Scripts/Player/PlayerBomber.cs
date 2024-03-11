using System.Collections;
using UnityEngine;

public class PlayerBomber : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform target;
    [SerializeField] GameObject meshObj;
    [SerializeField] GameObject effectObj;
    [SerializeField] LayerMask layer;
    [SerializeField] int dmg;

    Rigidbody rigid;

    WaitForSeconds seconds = new WaitForSeconds(4f);

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        StartCoroutine(Explosion());
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        target = GameManagers.instance.player.secondWeapon.nearTarget;
        if(target == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position
                                    ,target.transform.position, speed);
        transform.LookAt(target);
    }

    IEnumerator Explosion()
    {
        yield return seconds;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectObj.SetActive(true);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 0f 
                , layer);

        foreach(RaycastHit h in hits)
        {
            h.transform.GetComponent<AEnemys>().OnHit(dmg);
        }
    }
}
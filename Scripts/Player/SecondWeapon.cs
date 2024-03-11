using UnityEngine;

public class SecondWeapon : MonoBehaviour
{
    [SerializeField] float curShotDelay, maxShotDelay;

    public float scanRange, bulletSpeed;
    public LayerMask targetLayer;
    public RaycastHit[] targets;
    public Transform nearTarget;



    void Update()
    {
        curShotDelay += Time.deltaTime;
        Fire();
    }

    void FixedUpdate()
    {
        targets = Physics.SphereCastAll(transform.position, scanRange, transform.position, 0, targetLayer);
        nearTarget = GetNearTarget();
    }

    
    void Fire()
    {
        if(curShotDelay < maxShotDelay || nearTarget == null 
            || GameManagers.instance.player.isTalk) 
            return;

        
        GameObject bullet = GameManagers.instance.objectManager.Get(0);
        bullet.GetComponent<Bulletss>().isSkill = true;
        bullet.transform.position = transform.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        Vector3 dir = nearTarget.position - transform.position;
        rigid.AddForce(dir.normalized * bulletSpeed, ForceMode.Impulse);

        curShotDelay = 0;
    }

    public Transform GetNearTarget()
    {
        Transform result = null;
        float diff = 100;
        foreach(RaycastHit target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;

            float curDiff = Vector3.Distance(myPos, targetPos);
            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }

}

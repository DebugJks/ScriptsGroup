using UnityEngine;

public class EnemyRange : AEnemys
{
    [SerializeField] int attackNum; // 풀 + 타입 구분

    [SerializeField] Transform bulletPos;

    void Update()
    {
        curShotDelay += Time.deltaTime;
    }

    void FixedUpdate()
    {
        Think();
    }

    public override void Think()
    {  
        targetDis = Vector3.Distance(gameObject.transform.position, target.position);
        if((targetDis < detectDis))
        {
            if(targetDis < attackDis)
            {
                Attack();
                
            }
            else 
                Move();
        }
        else
            transform.position = originPos;  // 원래 위치로 귀환
    }

    public override void Attack()
    {
        rigid.velocity = Vector3.zero;
        if(curShotDelay < maxShotDelay)
            return;
        GameObject bullet = GameManagers.instance.objectManager.Get(attackNum);
        bullet.transform.position = bulletPos.position;
        Bulletss bulletLogic = bullet.GetComponent<Bulletss>();
        Rigidbody rigidA = bullet.GetComponent<Rigidbody>();
        bulletLogic.damage = dmg;
        
        Vector3 dirVec = target.position
            - transform.position;

        rigidA.AddForce(dirVec.normalized * bulletSpeed, ForceMode.Impulse);
        curShotDelay = 0;
    }

    public override void Move()
    {
        transform.LookAt(target);
        // 추적 시작
        transform.position = Vector3.MoveTowards(transform.position
                , target.position, Time.deltaTime*speed);
    }


    public override void Die()
    {
        AudioManagerS.intance.PlaySoundEffect(AudioManagerS.Sfx.EnemyDie);
        gameObject.SetActive(false);
    }

}
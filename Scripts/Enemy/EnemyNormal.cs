using System.Collections;
using UnityEngine;

public class EnemyNormal : AEnemys
{
    public BoxCollider attackRange;
    [SerializeField] ItemSO dropItem;   // 사망시 획득 아이템
    [SerializeField] bool isAttack;

    [SerializeField] NormalAttacks normalAttacks;

    WaitForSeconds delay02 = new WaitForSeconds(0.2f);
    WaitForSeconds delay1 = new WaitForSeconds(1f);


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        normalAttacks.damage = dmg;
    }

    void FixedUpdate() 
    {
        Think();
    }


    public override void Think()
    {
        float targetRad = 1.5f;
        float targetRange = 3f;
                // 타겟 존재 여부로 추척할지 공격할지 결정
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, targetRad
            ,transform.forward, targetRange, layer);
        
        if(targetDis < detectDis)
        {
            if((hits.Length > 0 && !isAttack ) && targetDis < attackDis)
                Attack();
            else
                Move();
        }
        else
            transform.position = originPos;
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position ,target.position, Time.deltaTime * speed);
        transform.LookAt(target);  
    }

    public override void Attack()
    {
        rigid.velocity = Vector3.zero;
        StartCoroutine(NormalAttack());
    }

    public override void Die()
    {
        GameManagers.instance.inventory.Add(dropItem, 1);  
        gameObject.SetActive(false);
    }


    IEnumerator NormalAttack()
    {
        isAttack = true;
        transform.LookAt(target);
        anim.SetBool("Attack", true);

        yield return delay02;
        attackRange.enabled = true;

        yield return delay1;
        attackRange.enabled = false;

            //  공격 종료
        yield return delay1;
        anim.SetBool("Attack", false);  
        isAttack = false;
    }
}
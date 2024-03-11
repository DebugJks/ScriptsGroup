using UnityEngine;
using UnityEngine.UI;
using StringSpace;

public abstract class AEnemys : MonoBehaviour
{
            // 기본 능력치
    public int curHp, maxHp;
    public int dmg;
    public float speed, bulletSpeed;
    public float curShotDelay, maxShotDelay;

    public float detectDis;// 탐지거리
    public float attackDis;     // 공격 거리 
    public float targetDis; // 플레이어와의 거리
    
    
    public Image hpBar;
    public LayerMask layer;     // 추적할 플레이어 레이어

    [HideInInspector] public Transform target;    // 플레이어 위치

    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Vector3 originPos;


    public abstract void Think();
    public abstract void Move();
    public abstract void Attack();
    public abstract void Die();


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        originPos = transform.position; // 일정 장소에 가면 추적 중지 후 원래 상태로 갈 위치
    }

    void Start() => target = GameManagers.instance.player.transform;

    void Update()
    {
        curShotDelay += maxShotDelay;
        ControlHpBar();
    }

    void OnEnable() => curHp = maxHp;
    
    public void OnHit(int dmg)
    {
        curHp -= dmg;

        if(curHp <= 0)
            Die();
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag(StringSpaces.PlayerBullet))
        {
            Bulletss playerBullet = other.gameObject.GetComponent<Bulletss>();
            OnHit(playerBullet.damage);
        }         
    }

    void ControlHpBar() => hpBar.fillAmount = (int) curHp / (int) maxHp;
}
using System.Collections;
using UnityEngine;
using StringSpace;

public class Bulletss : MonoBehaviour
{
    public bool isEnemy;
    public int damage;

    [Header("----발사 정보----")]
    public float speed;
    public float hitOffset;
    float originSpeed;

    //플레이어
    public GameObject hit;
    public GameObject flash;
    public GameObject[] Detached;

    public bool isSkill;    // 스킬인 경우 플레이어가 제어


    Rigidbody rigid;
    Vector3 dir;

    WaitForSeconds seconds = new WaitForSeconds(5);

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        originSpeed = speed;
        if(isEnemy) 
            return;

        StartCoroutine(DelHitEffect());
    }

    void FixedUpdate ()
    {
		if (speed != 0 && !isSkill)
        {
            transform.position += dir * (speed * Time.deltaTime);         
        }
	}

    IEnumerator DelBullet()
    {
        yield return seconds;
        gameObject.SetActive(false);
    }

    void OnEnable() 
    {
        speed = originSpeed;
        StartCoroutine(DelBullet());
        if(isEnemy)
            return;
        dir = GameManagers.instance.player.transform.forward;
        flash.transform.position = transform.position;
        flash.transform.forward = gameObject.transform.forward;
        StartCoroutine(DelFlash());
       
    }

    IEnumerator DelFlash()
    {
        var flashPs = flash.transform.GetChild(0).GetComponent<ParticleSystem>();
        flashPs.Play();
        yield return new WaitForSeconds(flashPs.main.duration);
        flash.SetActive(false);
    }

    IEnumerator DelHitEffect()
    {
        var hitPs = hit.transform.GetChild(0).GetComponent<ParticleSystem>();
        hitPs.Play();
        yield return new WaitForSeconds(hitPs.main.duration);
        hit.SetActive(false);

        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
            }
        }        
        isSkill = false;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other) 
    {      
        if ((other.gameObject.CompareTag(StringSpaces.Enemy) || other.gameObject.CompareTag(StringSpaces.Border))
                    && !isEnemy)
        {
            HitEffect();
        }
                // 적 공격
        if(isEnemy && other.gameObject.CompareTag(StringSpaces.Player))
        {
            GameManagers.instance.player.OnHit(damage);
            gameObject.SetActive(false);
        }
    }

    // void OnTriggerEnter(Collider other)
    // {

    // }

    void HitEffect()
    {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0; 
        hit.SetActive(true);
        StartCoroutine(DelHitEffect());
    }
}
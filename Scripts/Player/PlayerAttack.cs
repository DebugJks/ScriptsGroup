using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    readonly string Attack = "Attack";

    // 스킬관련
    [SerializeField] int curMp;   // 기본 mp
    int maxHp = 8;
    enum KeyType{MouseL, MouseR, Q, Heal, E}
    KeyType keyType;
    bool[] isSkill = {false, false, false, false, false};
    float[] coolTime = {1, 1, 5, 30, 7};   // 힐스킬원래 30
    int[] needMp = {0,0, 2,4, 3};  // 스킬 사용에 필요한 마나량

    [SerializeField] GameObject[] mpImages;
    [SerializeField] GameObject healEffect;


    Players player;
    WaitForSeconds seconds = new WaitForSeconds(6f);
    WaitForSeconds delay5 = new WaitForSeconds(5f);

    void Awake()
    {
        player = GetComponent<Players>();
        StartCoroutine(ChargeMp());
    }

    void Update()
    {
        Fire();
    }


    void Fire()
    {
        if(player.isTalk)  {return;}

        if(Input.GetMouseButton(0))
        {
            StartCoroutine(PlayerAttacks(KeyType.MouseL));
        }
        else if (Input.GetMouseButtonDown(1))
            StartCoroutine(PlayerAttacks(KeyType.MouseR)); 
        else if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(PlayerAttacks(KeyType.Q));
        else if (Input.GetKeyDown(KeyCode.Alpha1) && !isSkill[(int)KeyType.Heal])
            StartCoroutine(HealSkill(0));
        else if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(PlayerAttacks(KeyType.E));
    }
    


    IEnumerator PlayerAttacks(KeyType attackType)
    {
        if(isSkill[(int)attackType] == true || curMp < needMp[(int)attackType])
            yield break;
        if(attackType != KeyType.Heal)
            player.changeAnimation(Attack);
        isSkill[(int)attackType] = true;
        curMp -= needMp[(int)attackType];   // 마나 포인트 감소

        foreach(GameObject g in mpImages)       // 전체 비활성
        {
            g.SetActive(false);
        }
        SetCurMp();     // 남은만큼 활성

        switch(attackType)
        {
            case KeyType.MouseL:
                CreateBullet(KeyType.MouseL,0);
                break;
            case KeyType.MouseR:
                CreateBullet(KeyType.MouseR,1);
                break;
            case KeyType.Q:
                CreateBullet(KeyType.Q,0);
                break;
            case KeyType.Heal:
                StartCoroutine(HealSkill(0));
                break;
            case KeyType.E:
                CreateBullet(KeyType.E,(int)PoolState.Boomb);
                break;
        }
        
        yield return new WaitForSeconds(coolTime[(int)attackType]);
            // 쿨타임 종료
        isSkill[(int)attackType] = false;
    }

    void CreateBullet(KeyType keyType ,int num)
    {
        AudioManagerS.intance.PlaySoundEffect(AudioManagerS.Sfx.PlayerAttack);
            // 스킬별 패턴
        switch(keyType)
        {
            case KeyType.MouseL:
            case KeyType.MouseR:
                GameObject bullet = GameManagers.instance.objectManager.Get(num);
                bullet.transform.position = player.bulletPos.position;
                break;
            case KeyType.Q:
                int roundNum = 5;
                for(int i = 0; i < roundNum; i++)
                {
                    GameObject bulletA = GameManagers.instance.objectManager.Get(num);
                    Rigidbody rigidA = bulletA.GetComponent<Rigidbody>();
                    bulletA.GetComponent<Bulletss>().isSkill = true;
                    bulletA.transform.position = player.bulletPos.position;
                    Vector3 dir = new Vector3(Mathf.Cos(Mathf.PI * i/ (roundNum *2) ), 0,
                        Mathf.Sin(Mathf.PI * i / (roundNum*2)));
                    rigidA.AddForce(dir * 4, ForceMode.Impulse);
                }
                break;
            case KeyType.E:
                GameObject bomb = GameManagers.instance.objectManager.Get((int)PoolState.Boomb);
                bomb.transform.position = player.bulletPos.position;
                break;
        }
    }

    IEnumerator HealSkill(int repeatNum)
    {
        isSkill[(int)KeyType.Heal] = true;
        player.isShield = true;
        healEffect.SetActive(true);
        healEffect.transform.position = transform.position + Vector3.up * 0.7f;
        repeatNum +=1;
        player.playerData.curHp += 12;
        if(player.playerData.curHp > player.playerData.maxHp)
            player.playerData.curHp = player.playerData.maxHp;
        yield return delay5;
        if(repeatNum >= 6)
        {
            healEffect.SetActive(false);
            isSkill[(int)KeyType.Heal] = false;
            player.isShield = false;
            yield break;
        }
            
        StartCoroutine(HealSkill(repeatNum));
    }

    IEnumerator ChargeMp()
    {
        yield return seconds;
        if(curMp < maxHp)
        {
            curMp += 1;
            SetCurMp();
        }
        StartCoroutine(ChargeMp());
    }

    void SetCurMp()
    {
        for(int i = 0; i < curMp; i++)  // mp 이미지 활성
        {
            mpImages[i].SetActive(true);
        }
    }
}
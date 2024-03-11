using System.Collections.Generic;
using UnityEngine;

public class ObjectManagers : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        // 배열 안에 있는 프리팹들도 초기화 해주기
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
            // 선택한 풀에 놀고 있는(비활성화) 오브젝트에 접근
                
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {       
                    // 놀고있는거 발견하면 select에 할당
                select = item;
                select.SetActive(true);

                break;
            }
        }

            // 못찾은 경우 ( 전부 활성화 된 상황 ), 추가 생성이 더 필요한 경우
        if(!select)
        {
            select = Instantiate(prefabs[index], transform);
                // 생성한 걸 다시 풀에 추가해 주기
            pools[index].Add(select);
        }

        return select;
    }
}
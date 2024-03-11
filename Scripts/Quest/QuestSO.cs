using UnityEngine;

[CreateAssetMenu(menuName = "SO/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string quest_Title;
    public string quest_Info;

    public bool isComplete;

    public int curNum;  // 현재 수집량
    public int targetNum;   // 사냥 수집 등 목표 수량
}
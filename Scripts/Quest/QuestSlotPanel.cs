using UnityEngine;
using UnityEngine.UI;

public class QuestSlotPanel : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text info;
    int panelIndex;

    public void SetIndex(int index)
    {
        panelIndex = index;
    }

    public void Set(QuestSO questSO)
    {
        title.text = questSO.quest_Title;
        info.text = questSO.quest_Info;
    }

    public void Clean()
    {
        title.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
    }
}

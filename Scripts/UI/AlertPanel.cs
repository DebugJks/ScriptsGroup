using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AlertPanel : MonoBehaviour
{

    enum PosType {QuestAlert, Center}
    [SerializeField] PosType posType;

    WaitForSeconds delay3 = new WaitForSeconds(3);


    void OnEnable()
    {
        switch(posType)
        {
            case PosType.QuestAlert:
                QuestAlert_Open();
                break;
            
            case PosType.Center:
                CenterPopUp_Open();
                break;
        }
    }

    void QuestAlert_Open()
    {

    }


    void CenterPopUp_Open()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, 0.5f));
        sequence.Append(transform.DOScale(1f, 0.1f));

        StartCoroutine(HideCenterPopUp_Close());
    }

    IEnumerator HideCenterPopUp_Close()
    {
        yield return delay3;
        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.2f, 0.4f));
        seq.Append(transform.DOScale(0.2f, 0.2f));

        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}

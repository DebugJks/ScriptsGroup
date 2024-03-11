using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EscPanel : MonoBehaviour
{
    [SerializeField] Text questTitleText;
    [SerializeField] Text questInfoText;

    [SerializeField] Image PanelImage;
    RectTransform rect;


    WaitForSeconds seconds = new WaitForSeconds(0.8f);

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable() 
    {
        Panel_Open();
    }


    void Panel_Open()
    {
        AudioManagerS.intance.PlaySoundEffect(AudioManagerS.Sfx.Menu);
        PanelImage.DOFade(0.69f, 0.8f);
        rect.transform.DOLocalMoveY(0f,0.7f);
    }

    public void Panel_Close()
    {
        AudioManagerS.intance.PlaySoundEffect(AudioManagerS.Sfx.Menu);
        PanelImage.DOFade(0, 0.8f);
        rect.transform.DOLocalMoveY(-100,0.7f);
        DOVirtual.DelayedCall(0.8f, () => gameObject.SetActive(false));
    }
}
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using StringSpace;
using System.Collections;

public class MovieObj : MonoBehaviour
{
    [HideInInspector] public PlayableDirector playableDirector;
    [SerializeField] TimelineAsset timelineAsset;

    void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag(StringSpaces.Player))
        {
            AudioManagerS.intance.PlayBGM(AudioManagerS.BGM.SecondBGM);
            StartCoroutine(StartMovie());
        }    
    }

    IEnumerator StartMovie()
    {
        GameManagers.instance.player.isTalk = true;
        playableDirector.Play(timelineAsset);
        yield return new WaitForSeconds((float)timelineAsset.duration);
        GameManagers.instance.player.isTalk = false;
        Destroy(this.gameObject);
    }


}

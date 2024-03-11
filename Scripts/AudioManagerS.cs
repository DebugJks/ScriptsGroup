using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerS : MonoBehaviour
{
    const string SFX_NAME_FORMAT = "SFX - [{0}]";
    const string BGM_NAME_FORMAT = "BGM";

    public static AudioManagerS intance;
    public AudioMixerGroup bgmMixer;
    public AudioMixerGroup sfxMixer;
    AudioSource bgmSource;
    AudioSource[] effectSources;

            // 사운드 목록
    [SerializeField] AudioClip[] bgm_Clips;    
    [SerializeField] AudioClip[] clips;
    public int channels;
            // 사용 편리하게 하기 위함
    public enum Sfx {Alert,Menu, PlayerAttack, EnemyDie}
    public enum BGM {None, PrologueBGM, SecondBGM}
    [SerializeField] BGM firstBGM;

            // 볼륨조절 UI
    [Header("----Volume_UI-----")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;

    void Awake()
    {
        intance = this;

    
            // 오브젝트 생성
        GameObject sfxObj = new GameObject(SFX_NAME_FORMAT);    
        sfxObj.transform.parent = transform;
        effectSources = new AudioSource[channels];

        GameObject bgmObj = new GameObject(BGM_NAME_FORMAT);    
        bgmObj.transform.parent = transform;

        bgmSource = bgmObj.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;


        for (int i = 0; i < effectSources.Length; i++)
        {
            effectSources[i] = sfxObj.AddComponent<AudioSource>();
            effectSources[i].playOnAwake = false;
        }
    }

    void Start()
    {
        if(firstBGM != BGM.None)
            PlayBGM(firstBGM);
    }

    public void PlaySoundEffect(Sfx sfx,AudioMixerGroup mixer = null, float volume = 1)
    {
        if(mixer == null)
            mixer = sfxMixer;
        for(int i = 0; i < effectSources.Length; i++)
        {
            if(effectSources[i].isPlaying)  
                continue;

            effectSources[i].clip = clips[(int)sfx];
            effectSources[i].outputAudioMixerGroup = mixer;
            effectSources[i].volume = volume;
            
            effectSources[i].Play();
            break;
        }    
    }

    public void PlayBGM(BGM BGM,AudioMixerGroup mixer = null, float volume = 1, bool isLoop = true)
    {
        if(mixer == null)
            mixer = bgmMixer;

            bgmSource.clip = bgm_Clips[(int)BGM];
            bgmSource.outputAudioMixerGroup = mixer;
            bgmSource.volume = volume;
            bgmSource.loop = isLoop;
            bgmSource.Play();

    }

        // 볼륨 조절
    public void SetSFXVolume(float value)
    {
        value = sfxSlider.value;
        sfxMixer.audioMixer.SetFloat("SFX", value);
    }

    public void SetBGMVolume(float value)
    {
        value = bgmSlider.value;
        bgmMixer.audioMixer.SetFloat("BGM", value);
    }
}
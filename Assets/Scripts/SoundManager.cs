using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        GameObject.DontDestroyOnLoad(GameObject.Instantiate(Resources.Load("SoundManager")));
    }

    static public SoundManager Instance = null;

    [SerializeField] bool isMute = false;

    //����
    Dictionary<Sound, SoundPack> soundBank = new Dictionary<Sound, SoundPack>();
    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] List<SoundPack> soundList = new List<SoundPack>();
    //����
    Dictionary<Music, MusicPack> musicBank = new Dictionary<Music, MusicPack>();
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] List<MusicPack> musicList = new List<MusicPack>();

    [SerializeField] AudioMixerSnapshot maxVolume;
    [SerializeField] AudioMixerSnapshot minVolume;

    Music currentMusic = Music.None;
    bool isPreventPlayback = false;
    Coroutine prevenPlayback;

    /// <summary>�Ω󼽩�ݭn�j�q�ӷL�ܤƪ��n��(ex.�}�B�n)</summary>
    List<SoundPack> collectedSounds = new List<SoundPack>();

    bool hasSubscribeSceneChange = false;

    private void Awake()
    {
        //��SoundManager�����V�����A�n�����]�ഫ�����Ӥ��_�A�ýT�O�u���@�ӹ���
        if (FindObjectsOfType<SoundManager>().Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //��l�ƭ��Įw
        soundBank.Clear();
        foreach (SoundPack s in soundList)
        {
            soundBank.Add(s.sound, s);
        }
        //��l�ƭ��֮w
        musicBank.Clear();
        foreach (MusicPack m in musicList)
        {
            musicBank.Add(m.music, m);
        }
        Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        hasSubscribeSceneChange = true;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (isMute)
            return;

        isPreventPlayback = false;
        if (prevenPlayback != null)
            StopCoroutine(prevenPlayback);

        MuiscTransition(newScene);

    }

    #region ����

    private void MuiscTransition(Scene newScene)
    {
        maxVolume.TransitionTo(0f);
        //�������ƭȶ��̱M�׽վ� (�ثe�C���}�l�����4)
        int mainGameScene = 4;
        if (newScene.buildIndex >= mainGameScene && currentMusic != Music.MainGame)
        {
            StartCoroutine(PlayMusic(Music.MainGame));
            currentMusic = Music.MainGame;
        }
        else if (newScene.buildIndex < mainGameScene && currentMusic != Music.Menu)
        {
            StartCoroutine(PlayMusic(Music.Menu));
            currentMusic = Music.Menu;
        }

        if (newScene.name == "GameOver")
        {
            StopMusic(currentMusic);
        }
    }

    IEnumerator PlayMusic(Music music)
    {
        if (musicBank.Count <= 0)
            yield break;
        while (true)
        {
            //����񤧫e�����֡A�ñNmixer���q���̧ܳC
            musicPlayer.Stop();
            yield return null;
            minVolume.TransitionTo(0f);
            yield return null;
            //�}�l����ḛ̀Ѽ�FadeIn
            musicPlayer.clip = musicBank[music].audioClip;
            musicPlayer.volume = musicBank[music].volume;
            musicPlayer.Play();
            maxVolume.TransitionTo(musicBank[music].fadeIn);
            //�������FadeOut�I      
            yield return new WaitUntil(() => (musicPlayer.clip.length - musicPlayer.time) <= musicBank[music].fadeOut);
            minVolume.TransitionTo(musicBank[music].fadeOut);
            yield return new WaitForSeconds(musicBank[music].fadeOut);
        }
    }

    void StopMusic(Music music)
    {
        minVolume.TransitionTo(musicBank[music].fadeOut);
    }

    #endregion


    #region ����
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="sound"></param>
    public void Play(Sound sound)
    {
        StartCoroutine(PlayOnce(sound,0.1f));
    }

    /// <summary>
    /// ����
    /// <para>preventTime:����Ӯɶ�������A����</para>
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="preventTime"></param>
    public void Play(Sound sound,float preventTime)
    {
        StartCoroutine(PlayOnce(sound,preventTime));
    }

    /// <summary>
    /// ������������w�a�I����
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    public void Play(Sound sound, Vector3 position = default(Vector3))
    {
        StartCoroutine(PlayInScene(sound, position));
    }


    private IEnumerator PlayOnce(Sound sound,float preventTime = 0.1f)
    {
        Debug.Log("Play " + sound.ToString() + " " + prevenPlayback);
        sfxPlayer.clip = null;
        yield return new WaitForSeconds(soundBank[sound].offset);
        if (isPreventPlayback)
            yield break;

        sfxPlayer.PlayOneShot(soundBank[sound].clip, soundBank[sound].volume);
        if(preventTime > 0)
            prevenPlayback = StartCoroutine(PreventPlaybackTime(preventTime));
    }

    private IEnumerator PlayInScene(Sound sound, Vector3 pos)
    {
        yield return new WaitForSeconds(soundBank[sound].offset);
        AudioSource.PlayClipAtPoint(soundBank[sound].clip, pos, soundBank[sound].volume);
    }


    /// <summary>
    /// ����UI����
    /// </summary>
    /// <param name="sound"></param>
    public void PlayWithUI(Sound sound)
    {
        sfxPlayer.clip = soundBank[sound].clip;
        sfxPlayer.Play();
    }

    private void PlayRandomizedSound(Sound sound)
    {

    }

    private void Stop(Sound sound)
    {
        sfxPlayer.Stop();
    }

    #endregion

    IEnumerator PreventPlaybackTime(float time)
    {
        isPreventPlayback = true;
        yield return new WaitForSeconds(time);
        isPreventPlayback = false;
    }

    private void OnDisable()
    {
        if (hasSubscribeSceneChange)
            SceneManager.activeSceneChanged -= OnSceneChanged;
    }

}



public enum Sound
{
    DoorPass,
    DoorPortal,
    GetStar,
    SwitchClose,
    Die,
    Jump,
    UI_Click = 100,
    UI_Return = 101,
    UI_ScrollingSelect = 102,
}

public enum Music
{
    None,
    MainGame,
    Menu
}


[System.Serializable]
public struct SoundPack
{
    public AudioClip clip;
    public Sound sound;
    [Range(0f, 1f)] public float volume;
    [Range(0f, 3f)] public float offset;
}

[System.Serializable]
public struct MusicPack
{
    public AudioClip audioClip;
    public Music music;
    [Range(0f, 1f)] public float volume;
    public float fadeIn;
    public float fadeOut;
}
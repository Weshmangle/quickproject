using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musics;
    [SerializeField] private AudioSource _source;
    [SerializeField] AudioMixerGroup _soundEffectMixer;
    [SerializeField] private float _pitchBoostValue = .01f;
    [SerializeField] private float _maxPitchValue = 1.2f;
    [SerializeField] private bool _increaseMusicSpeed = true;

    private int _index = 0;
    private bool _playMusic = true;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of AudioManager already exist");
            return;
        }

        Instance = this;
    }

    public void StartMusic()
    {
        GameManager.Instance.OnGameSpeedChanged += SpeedMusic;
        GameManager.Instance.OnGameSpeedReset += ResetDefaultSpeedMusic;
        StartCoroutine(PlayMusic());
    }

    private void SpeedMusic()
    {
        if (_increaseMusicSpeed && _source.pitch >= _maxPitchValue) return;
        _source.pitch += _pitchBoostValue;
    }

    private void ResetDefaultSpeedMusic()
    {
        _source.pitch = 1f;
        _source.Play();
    }

    public void SetOffMusic()
    {
        _source.volume = .0f;
    }

    public void SetMiddleMusic()
    {
        _source.volume = .5f;
    }
    public void SetOnMusic()
    {
        _source.volume = 1.0f;
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        if (clip == null) return null;
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = _soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }

    private IEnumerator PlayMusic()
    {
        if (_musics.Length > 0)
        {
            while (_playMusic)
            {
                _index %= _musics.Length;

                _source.clip = _musics[_index];
                _source.Play();                

                yield return new WaitForSeconds(_musics[_index].length);
                _index++;
            }
        }
    }
}

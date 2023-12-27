////using UnityEngine;
////using UnityEngine.UI;

////public class AudioManager : MonoBehaviour
////{
////    [SerializeField] Sprite onSprite;
////    [SerializeField] Sprite offSprite;
////    [SerializeField] Button musicButton, soundButton;

////    public AudioSource musicSource,soundSource;
////    public AudioClip clickSound, gameOverSound, CorrectAnsSound;

////    private bool isOnMusic = false;
////    private bool isOnSound = false;

////    private void Awake()
////    {
////        isOnMusic = true;
////        isOnSound = true;
////    }
////    public void MusicManager()
////    {
////        if(isOnMusic)
////        {
////            musicButton.GetComponent<Image>().sprite = onSprite;
////            musicSource.Stop();
////            isOnMusic = false;    
////        }
////        else
////        {
////            musicButton.GetComponent <Image>().sprite = offSprite;
////            musicSource.Play();
////            isOnMusic =true;
////        }
////        soundSource.PlayOneShot(clickSound);
////    }
////    public void SoundManager()
////    {   
////        if (isOnSound)
////        {
////            soundButton.GetComponent<Image>().sprite = onSprite;

////            isOnSound = false;
////        }
////        else
////        {
////            soundButton.GetComponent<Image>().sprite = offSprite;
////            isOnSound = true;
////        }
////        soundSource.PlayOneShot(clickSound);
////    }
////    public void PlaySound(AudioClip clip)
////    {
////        soundSource.PlayOneShot(clip);
////    }
////}


//using UnityEngine;
//using UnityEngine.UI;

//public class AudioManager : MonoBehaviour
//{
//    [SerializeField] Sprite onSprite;
//    [SerializeField] Sprite offSprite;
//    [SerializeField] Button musicButton, soundButton;

//    public AudioSource musicSource, soundSource;
//    public AudioClip clickSound, gameOverSound, CorrectAnsSound;

//    private bool isOnMusic = false;
//    private bool isOnSound = false;
//    private bool isClickSoundPlaying = false; // Track whether the clickSound is playing

//    private void Awake()
//    {
//            //if (FindObjectsOfType<AudioManager>().Length > 1)
//            //{
//            //    Destroy(gameObject);
//            //}
//            //else
//            //{
//            //    DontDestroyOnLoad(gameObject);
//            //    // Initialize other settings and variables here
//            //}
//        isOnMusic = true;
//        isOnSound = true;
//    }

//    private void Start()
//    {
//        soundSource.clip = clickSound; // Set the initial clip for the soundSource
//    }

//    public void MusicManager()
//    {
//        if (isOnMusic)
//        {
//            musicButton.GetComponent<Image>().sprite = onSprite;
//            musicSource.Stop();
//            isOnMusic = false;
//            PlayerPrefs.Save();
//        }
//        else
//        {
//            musicButton.GetComponent<Image>().sprite = offSprite;
//            musicSource.Play();
//            isOnMusic = true;
//        }
//        PlaySound(clickSound); // Play the clickSound using PlaySound method
//    }

//    public void SoundManager()
//    {
//        if (isOnSound)
//        {
//            soundButton.GetComponent<Image>().sprite = onSprite;
//            isClickSoundPlaying = soundSource.isPlaying && soundSource.clip == clickSound; // Check if clickSound is playing
//            if (isClickSoundPlaying)
//            {
//                soundSource.Stop();
//            }
//            isOnSound = false;
//        }
//        else
//        {
//            soundButton.GetComponent<Image>().sprite = offSprite;
//            if (isClickSoundPlaying)
//            {
//                soundSource.Play();
//            }
//            isOnSound = true;
//        }
//        PlaySound(clickSound);
//    }

//    public void PlaySound(AudioClip clip)
//    {
//        soundSource.PlayOneShot(clip);
//    }
//}


using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    [SerializeField] Button musicButton, soundButton;

    public AudioSource musicSource, soundSource;
    public AudioClip clickSound, gameOverSound, CorrectAnsSound;

    private bool isOnMusic = true;
    private bool isOnSound = true;

    private const string MusicPrefKey = "MusicEnabled";
    private const string SoundPrefKey = "SoundEnabled";

    private void Awake()
    {
        LoadSettings();
    }

    private void Start()
    {
        soundSource.clip = clickSound; // Set the initial clip for the soundSource
    }

    private void LoadSettings()
    {
        isOnMusic = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
        isOnSound = PlayerPrefs.GetInt(SoundPrefKey, 1) == 1;
        UpdateUI();
        UpdateAudioVolumes();
    }

    private void UpdateUI()
    {
        musicButton.GetComponent<Image>().sprite = isOnMusic ? onSprite : offSprite;
        soundButton.GetComponent<Image>().sprite = isOnSound ? onSprite : offSprite;
    }

    private void UpdateAudioVolumes()
    {
        musicSource.volume = isOnMusic ? 1 : 0;
        soundSource.volume = isOnSound ? 1 : 0;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(MusicPrefKey, isOnMusic ? 1 : 0);
        PlayerPrefs.SetInt(SoundPrefKey, isOnSound ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void MusicManager()
    {
        isOnMusic = !isOnMusic;
        SaveSettings();
        UpdateUI();
        UpdateAudioVolumes();
        PlaySound(clickSound);
    }

    public void SoundManager()
    {
        isOnSound = !isOnSound;
        SaveSettings();
        UpdateUI();
        UpdateAudioVolumes();
        PlaySound(clickSound);
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
}

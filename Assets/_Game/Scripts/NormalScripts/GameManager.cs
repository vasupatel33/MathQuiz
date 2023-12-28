using GoogleMobileAds.Sample;
using GoogleMobileAds.Samples;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject firstPanel, loadingPanel, settingPanel;
    [SerializeField] Image slider;
    [SerializeField] Animator settingAnim;
    [SerializeField] Animator firstPanelAnim;

    [SerializeField] Button MusicBtn, SoundBtn;
    [SerializeField] Sprite OffSprite, OnSprite;

    
    [SerializeField] AudioClip MusicClip, clickClip, GameoverClip, CorrectAnsClip;

    float sliderValue = 0.3f;
    //float MaxValue;
    bool playBtnClicked;
    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().Play();

        //MaxValue = slider.maxValue;
        MusicSet();
        SoundSet();
    }
    public void PlayBtnClicked()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        loadingPanel.SetActive(true);
        firstPanel.SetActive(false);
        playBtnClicked = true;
    }
    public void QuitApplication()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        //audioManager.PlaySound(audioManager.clickSound);
        Application.Quit();
    }
    public void SettingBtnClicked()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        settingPanel.SetActive(true);
        firstPanelAnim.Play("FirstaPanelFadeIn");
    }
    public void BackBtnSecondPanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        firstPanelAnim.Play("FirstaPanelFadeOut");
        settingAnim.Play("SettingPanelClose");
        StartCoroutine(SettinPanelEnum());
    }
    IEnumerator SettinPanelEnum()
    {
        Debug.Log("workkkkkkk");
        yield return new WaitForSeconds(1f);
        settingPanel.SetActive(false);
        firstPanel.SetActive(true);
    }
    public void rewaredAwared()
    {
        RewardedAdController.onVideoSuccessReward = null;
        RewardedAdController.onVideoSuccessReward = OnSuccess;
        RewardedAdController.instance.ShowAd();
    }
    void OnSuccess()
    {
        Debug.Log("my rewared one");
    }
    public void rewaredAwared2()
    {
        RewardedAdController.onVideoSuccessReward = null;
        RewardedAdController.onVideoSuccessReward = OnSuccess2;
        RewardedAdController.instance.ShowAd();
    }
    void OnSuccess2()
    {
        Debug.Log("my rewared two");
    }
    void Update()
    {
        if (playBtnClicked)
        {
            if (slider.fillAmount < 1)
            {
                slider.fillAmount += sliderValue * Time.deltaTime;
                //slider.value += sliderValue * Time.deltaTime;

            }
            else
            {
                SceneManager.LoadScene("GamePlay");
            }

        }

    }
    
    public void MusicManagement()
    {
        if (CommonScript.instance.musicPlaying == true)
        {
            MusicBtn.GetComponent<Image>().sprite = OffSprite;
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = true;
            CommonScript.instance.musicPlaying = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = OnSprite;
            CommonScript.instance.musicPlaying = true;
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
        }
    }
    public void MusicSet()
    {
        CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().clip = MusicClip;
        CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
        //CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(MusicClip);
        if (CommonScript.instance.musicPlaying==true)
        {
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute=false;
            MusicBtn.GetComponent<Image>().sprite= OnSprite;
        }
        else
        {
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = true;
            MusicBtn.GetComponent<Image>().sprite = OffSprite;
        }
    }
    public void SoundManagement()
    {
        if (CommonScript.instance.soundPlaying == true)
        {
            SoundBtn.GetComponent<Image>().sprite = OffSprite;
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = true;
            CommonScript.instance.soundPlaying = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = OnSprite;
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = false;
            CommonScript.instance.soundPlaying = true;
        }
    }
    public void SoundSet()
    {
        if (CommonScript.instance.soundPlaying==true)
        {
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute=false;
            SoundBtn.GetComponent<Image>().sprite= OnSprite;
        }
        else
        {
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = true;
            SoundBtn.GetComponent<Image>().sprite = OffSprite;
        }
    }
    
}

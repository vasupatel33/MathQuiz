using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Samples;
//using static UnityEngine.ParticleSystem;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;

    public GameObject pausePanel, firstPanel, secondPanel, settingPanel, GameOverPanel, loadingOverPanel;

    [SerializeField] private Image SliderImage;

    [SerializeField] private TextMeshProUGUI LeftText, rightText, OperatorText;

    public TextMeshProUGUI score;

    [SerializeField] private List<float> GeneratedValue;

    [SerializeField] private TextMeshProUGUI[] FourValue;

    [SerializeField] private Animator gameOveranim;

    [SerializeField] private Animator secondPanelAnim;

    [SerializeField] private Animator PausePanelAnim;

    [SerializeField] private TextMeshProUGUI GameOverScore;

    [SerializeField] private Animator SettingPanelAnim;

    AudioManager audioManager;

    bool sliderValue;

    float Value1, Value2, ans;

    float sliderSpeed = 0.15f;

    float selectedField;

    bool flag = false;

    float val;

    public int scoreValue=0;

    bool SliderFlag=false;

    bool LoadingPanelOpen;

    [SerializeField] AudioClip MusicClip, clickClip, GameoverClip, CorrectAnsClip;

    [SerializeField] Button MusicBtn, SoundBtn;
    [SerializeField] Sprite OffSprite, OnSprite;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //audioManager = GameObject.FindObjectOfType<AudioManager>();
        MusicSet();
        SoundSet();
        score.text = 0.ToString();
    }

    void Update()
    {
        if (SliderFlag)
        {

            if (sliderValue)
            {
                if (SliderImage.fillAmount > 0)
                {
                    SliderImage.fillAmount -= sliderSpeed * Time.deltaTime;
                    LoadingPanelOpen = false;
                }
                else
                {
                    flag = true;
                    if (LoadingPanelOpen == false)
                    {
                        loadingOverPanel.SetActive(true);
                    }
                    LoadingPanelOpen = true;
                }

            }
        }
        
    }

    public void BackBtnClickedFirstPanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SceneManager.LoadScene("MainMenu");
    }
    //private float pausedFillAmount;
    //private bool isPaused = false;

    public void PauseBtnSecondPanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        pausePanel.SetActive(true);
        PausePanelAnim.Play("PausePanel");
        SliderImage.fillAmount += Time.deltaTime * 0.1f;
        sliderValue = false;
        //if (!isPaused)
        //{
        //    isPaused = true;
        //    pausedFillAmount = SliderImage.fillAmount;
        //    SliderImage.fillAmount = pausedFillAmount;
        //}
    }
    public void SettingBtnPausePanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        settingPanel.SetActive(true);
        SettingPanelAnim.Play("SettingPanel");
        sliderValue = false;
    }
    public void BackBtnSettingPanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SettingPanelAnim.Play("SettingPanelClose");
        StartCoroutine(WaitForSettingPanelClose());
    }
    IEnumerator WaitForSettingPanelClose()
    {
        yield return new WaitForSeconds(1f);
        settingPanel.SetActive(false);
    }
    public void CloseButtonPausePanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        PausePanelAnim.Play("PausePanelClose");
        StartCoroutine(WaitForPausePanelClose());
        sliderValue = true;
    }
    IEnumerator WaitForPausePanelClose() 
    {
        yield return new WaitForSeconds(1f);
        pausePanel.SetActive(false);
    }
    public void ResumeButtonPausePanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        PausePanelAnim.Play("PausePanelClose");
        StartCoroutine(WaitForPausePanelClose());
        sliderValue = true;
        //pausePanel.SetActive(false);
    }
    public void RestartButtonPausePanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        sliderValue = true;
        //SliderImage.fillAmount += 0.3f;
        SecondPanel();
        score.text = 0.ToString();
        scoreValue = 0;
        pausePanel.SetActive(false);
    }
    public void HomeButtonPausePanel()
    {
        GoogleMobileAdsController.Instance.DestroyBannerAdd();
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        sliderValue = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    public void ArithmeticBtnClicked(int value)
    {
        GoogleMobileAdsController.Instance.ShowBannerAdd();
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        selectedField = value;
        PlayerPrefs.SetInt("ArithmeticValue",value);
        Debug.Log("Arithmetic val = "+val);
        secondPanel.SetActive(true);
        firstPanel.SetActive(false);
        sliderValue = true;
        SecondPanel();
    }
    public void ResumeButtonLoadingOverPanel()
    {
        GoogleMobileAdsController.Instance.DestroyBannerAdd();
        GoogleMobileAdsController.Instance.ShowRewardAd();
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        
    }
    public void RestartBtnLoadingOver()
    {
        GoogleMobileAdsController.Instance.DestroyBannerAdd();
        //GoogleMobileAdsController.Instance.ShowRewardAd();
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        score.text = 0.ToString();
        scoreValue = 0;
        secondPanel.SetActive(false);
        loadingOverPanel.SetActive(false);
        settingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
    public void RestartButton()
    {
        GoogleMobileAdsController.Instance.DestroyBannerAdd();
        GoogleMobileAdsController.Instance.ShowInterstitialAdd();
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SliderFlag = true;
        score.text = 0.ToString();
        scoreValue = 0;
        GameOverPanel.SetActive(false);
        secondPanel.SetActive(true);
    }
    public void rewaredSuccess()
    {
        scoreValue = PlayerPrefs.GetInt("scorePref");
        Debug.Log("after reward score = " + scoreValue);
       score.text = scoreValue.ToString();
        int arithValue = PlayerPrefs.GetInt("ArithmeticValue");
        Debug.Log("after reward arith value = " + arithValue);

        ArithmeticBtnClicked(arithValue);
        secondPanel.SetActive(true);
        loadingOverPanel.SetActive(false);
        settingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
    public void ExitButtonGameOverPanel()
    {
        GoogleMobileAdsController.Instance.DestroyBannerAdd();
        GoogleMobileAdsController.Instance.ShowInterstitialAdd();
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SceneManager.LoadScene("MainMenu");
        score.text = 0.ToString();
        scoreValue = 0;
    }

    public void SecondPanel()
    {
        SliderImage.fillAmount = 1;
        SliderFlag = true;

        switch (selectedField)
        {
            case 1:
                OperatorText.text = "+";
                Value1 = Random.Range(1, 20);
                Value2 = Random.Range(1, 20);
                LeftText.text = Value1.ToString();
                rightText.text = Value2.ToString();
                ans = Value1 + Value2;
                GeneratedAnswers();
                Debug.Log("Ans = " + ans);
                flag = false;

                break;
            case 2:
                OperatorText.text = "-";
                Value1 = Random.Range(1, 20);
                Value2 = Random.Range(1, 20);
                if (Value1 < Value2)
                {
                    float temp = Value1;
                    Value1 = Value2;
                    Value2 = temp;
                }
                LeftText.text = Value1.ToString();
                rightText.text = Value2.ToString();
                ans = Value1 - Value2;
                GeneratedAnswers();
                Debug.Log("Ans = " + ans);
                flag = false;

                break;
            case 3:
                OperatorText.text = "/";
                Value2 = Random.Range(4, 15);//3
                Value1 = Value2*Random.Range(1, 12);//21
                LeftText.text = Value1.ToString();
                rightText.text = Value2.ToString();
                ans = Value1 / Value2;
                Debug.Log("val is = " + val);
                GeneratedAnswers();
                Debug.Log("Ans = " + ans);
                flag = true;

                break;
            case 4:
                OperatorText.text = "x";
                Value1 = Random.Range(1, 10);
                Value2 = Random.Range(1, 10);
                LeftText.text = Value1.ToString();
                rightText.text = Value2.ToString();
                ans = Value1 * Value2;
                GeneratedAnswers();
                Debug.Log("Ans = " + ans);
                flag = false;

                break;
        }
        
    }
    void GeneratedAnswers()
    {
        
        float randomValue;
        GeneratedValue.Clear();
        for (int i = 0; i < 3; i++)
        {
            do
            {
                if (flag)
                {
                    randomValue = Random.Range(ans-5,ans + 5);//-2
                    while(randomValue <= 0)
                    {
                        randomValue = Random.Range(ans-5, ans + 5);//7
                    }
                    float randomval = (int)System.Math.Abs(randomValue);
                    randomValue = randomval;
                }
                else
                {
                    randomValue = Random.Range((int)ans - 5, (int)ans + 5);
                    float ab = System.Math.Abs(randomValue);
                    randomValue = ab;
                }
            } while (GeneratedValue.Contains(randomValue) || ans == randomValue);
            GeneratedValue.Add(randomValue);
        }
        SetFourBtnValue();
    }
    void SetFourBtnValue()
    {
        int value;
        int counter = 0;

        value = Random.Range(0, FourValue.Length);//value=0
        for (int i = 0; i < FourValue.Length; i++)
        {
            if (value == i)//0=0
            {
                if (flag == true)
                {
                    FourValue[i].text = ans.ToString();
                }
                else
                {
                    FourValue[i].text = ans.ToString();
                }
            }
            else
            {
                FourValue[i].text = GeneratedValue[counter].ToString();
                counter++;
            }
        }
    }

   
    
    public void SetAnswer(TextMeshProUGUI text)
    {
        if (text.text == ans.ToString())
        {
            //audioManager.PlaySound(audioManager.CorrectAnsSound);
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(CorrectAnsClip);
            Debug.Log("Answer is correcttt");
            SecondPanel();
            scoreValue++;
            PlayerPrefs.SetInt("scorePref",scoreValue);
            score.text = scoreValue.ToString();
            secondPanelAnim.SetTrigger("ScoreAnim");
        }
        else
        {
            //audioManager.PlaySound(audioManager.gameOverSound);
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(GameoverClip);
            //GameObject.Find("Loading").transform.GetChild(0).gameObject.SetActive(false);
            SliderFlag = false;
            Debug.Log("Wrong Answer");
            GameOverPanel.SetActive(true);
            GameOverScore.text = score.text;
            //gameOveranim.SetBool("gameover",true);
        }
    }
 
    public void SliderOff()
    {
        SliderFlag = false;
    }

    public void MusicManagement()
    {
        if (CommonScript.instance.musicPlaying == true)
        {
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = true;
            MusicBtn.GetComponent<Image>().sprite = OffSprite;
            CommonScript.instance.musicPlaying = false;
        }
        else
        {
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
            MusicBtn.GetComponent<Image>().sprite = OnSprite;
            CommonScript.instance.musicPlaying = true;
        }
    }
    public void MusicSet()
    {
        CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().clip = MusicClip;
        CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
        if (CommonScript.instance.musicPlaying == true)
        {
            CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
            MusicBtn.GetComponent<Image>().sprite = OnSprite;
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
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = true;
            SoundBtn.GetComponent<Image>().sprite = OffSprite;
            CommonScript.instance.soundPlaying = false;
            Debug.Log("Sound True");
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = OnSprite;
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = false;
            CommonScript.instance.soundPlaying = true;
            Debug.Log("Sound false");
        }
    }
    public void SoundSet()
    {
        if (CommonScript.instance.soundPlaying == true)
        {
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = false;
            SoundBtn.GetComponent<Image>().sprite = OnSprite;
        }
        else
        {
            CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = true;
            SoundBtn.GetComponent<Image>().sprite = OffSprite;
        }
    }

}

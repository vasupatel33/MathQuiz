using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
//using static UnityEngine.ParticleSystem;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel, firstPanel, secondPanel, settingPanel, GameOverPanel, loadingOverPanel;

    //[SerializeField] Animator animator;

    [SerializeField] private Image SliderImage;

    //[SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI LeftText, rightText, OperatorText;

    [SerializeField] private TextMeshProUGUI score;

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

    int scoreValue=0;

    bool SliderFlag=false;

    bool LoadingPanelOpen;

    [SerializeField] AudioClip MusicClip, clickClip, GameoverClip, CorrectAnsClip;

    [SerializeField] Button MusicBtn, SoundBtn;
    [SerializeField] Sprite OffSprite, OnSprite;
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
                    Debug.Log("else workingg");
                    if (LoadingPanelOpen == false)
                    {
                        loadingOverPanel.SetActive(true);
                    }
                    LoadingPanelOpen = true;

                    //SliderImage.fillAmount = 0.1f;
                }

            }
        }
        
    }

    public void BackBtnClickedFirstPanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SceneManager.LoadScene("MainMenu");
    }
    //private float pausedFillAmount;
    //private bool isPaused = false;

    public void PauseBtnSecondPanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
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
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        settingPanel.SetActive(true);
        SettingPanelAnim.Play("SettingPanel");
        sliderValue = false;
    }
    public void BackBtnSettingPanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
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
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        PausePanelAnim.Play("PausePanelClose");
        StartCoroutine(WaitForPausePanelClose());
        sliderValue = true;
        //   Time.timeScale = 1;
    }
    IEnumerator WaitForPausePanelClose() 
    {
        yield return new WaitForSeconds(1f);
        pausePanel.SetActive(false);
    }
    public void ResumeButtonPausePanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        PausePanelAnim.Play("PausePanelClose");
        StartCoroutine(WaitForPausePanelClose());
        sliderValue = true;
        //pausePanel.SetActive(false);
    }
    public void RestartButtonPausePanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        sliderValue = true;
        //SliderImage.fillAmount += 0.3f;
        SecondPanel();
        score.text = 0.ToString();
        scoreValue = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pausePanel.SetActive(false);
    }
    public void HomeButtonPausePanel()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        sliderValue = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        //ArithmeticBtnClicked();
        //GeneratedValue.Clear();
    }

    public void ArithmeticBtnClicked(int value)
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        selectedField = value;
        secondPanel.SetActive(true);
        firstPanel.SetActive(false);
        sliderValue = true;
        SecondPanel();
    }
    public void RestartBtnLoadingOver()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        score.text = 0.ToString();
        scoreValue = 0;
        //SliderFlag = true;
        //Time.timeScale = 1;
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        loadingOverPanel.SetActive(false);
        settingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
    public void RestartButton()
    {
        //audioManager.PlaySound(audioManager.clickSound);
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SliderFlag = true;
        //Time.timeScale = 1;
        score.text = 0.ToString();
        scoreValue = 0;
        GameOverPanel.SetActive(false);
        secondPanel.SetActive(true);
    }
    public void ExitButtonGameOverPanel()
    {
        CommonScript.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(clickClip);
        SceneManager.LoadScene("MainMenu");
        //Time.timeScale = 1;
        score.text = 0.ToString();
        scoreValue = 0;
    }

    public void SecondPanel()
    {
        //ScoreAnim = false;
        SliderImage.fillAmount = 1;
        SliderFlag = true;
        //Debug.Log("Panel open = " + selectedField);
        //Debug.Log("Operator = " + OperatorText);

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
                //val = (float)System.Math.Round(ans, 2);
                Debug.Log("val is = " + val);

                //ans = val;
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
                    //flag = false;
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
        //Shuffle();
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

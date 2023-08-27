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

    AudioManager audioManager;
    float sliderValue = 0.3f;
    //float MaxValue;
    bool playBtnClicked;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //MaxValue = slider.maxValue;
    }

    public void PlayBtnClicked()
    {
        audioManager.PlaySound(audioManager.clickSound);
        loadingPanel.SetActive(true);
        firstPanel.SetActive(false);
        playBtnClicked = true;
    }
    public void QuitApplication()
    {
        audioManager.PlaySound(audioManager.clickSound);
        Application.Quit();
    }
    public void SettingBtnClicked()
    {
        audioManager.PlaySound(audioManager.clickSound);
        settingPanel.SetActive(true);
        firstPanelAnim.Play("FirstaPanelFadeIn");
    }
    public void BackBtnSecondPanel()
    {
        audioManager.PlaySound(audioManager.clickSound);
        firstPanelAnim.Play("FirstaPanelFadeOut");
        settingAnim.Play("SettingPanelClose");
        StartCoroutine(SettinPanelEnum());
        //settingAnim.SetBool("SettingPanelOff",true);
    }
    IEnumerator SettinPanelEnum()
    {
        Debug.Log("workkkkkkk");
        yield return new WaitForSeconds(1f);
        settingPanel.SetActive(false);
        firstPanel.SetActive(true);
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

}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    private TMP_Text scoreText, expText, statusText; // displays player score

    private void OnEnable(){
        scoreText = GetUIComponent<TMP_Text>("ScoreNumber");
        expText = GetUIComponent<TMP_Text>("ExpNumber");
        statusText = GetUIComponent<TMP_Text>("StatusNumber");
        statusText.text = "LOST";
    }
    private void Start(){
        (transform as RectTransform).sizeDelta= new Vector2(960,540); 
    }

    public void SetScore(float score){ scoreText.text = score.ToString();}
    public void SetLevel(int level){ expText.text = level.ToString();}

    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "RestartButton":
                GoToChosenScene("MainScene");
                EventMgr.Instance.EventTrigger("StopShowing");
                UIMgr.Instance.HidePanel("GameOverPanel");
                break;
            
            case "BackToMenuButton":
                GoToChosenScene("StartScene");
                EventMgr.Instance.EventTrigger("StopShowing");
                UIMgr.Instance.HidePanel("GameOverPanel");
                break;
            default:
                break;
        }
    }
    private void GoToChosenScene(string sceneName)
    {        
        UIMgr.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_PanelLayer.Top); //show loading panel
        PoolMgr.Instance.Clear(); //empty the pool to prevent null reference
        SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
        {
            EventMgr.Instance.EventTrigger("ProgressBar", 1f);
        }); 
    }
    public override void Show(){
        Time.timeScale = 0;
    }
    public override void Hide(){
        Time.timeScale = 1;
    }
    void OnDestroy(){
    }
}

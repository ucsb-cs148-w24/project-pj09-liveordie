using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private bool showing;
    private float score;
    private int level;
    private void DontShow(){
        showing = false;
    }
    private void StartShow(){
        if(!showing){
            PoolMgr.Instance.Clear();
            AudioMgr.Instance.StopBGM();
            AudioMgr.Instance.PlayAudio("GameOver", false);
            UIMgr.Instance.ShowPanel<GameOverPanel>("GameOverPanel", E_PanelLayer.Top, (panel) =>
            {
                panel.SetScore(score);
                panel.SetLevel(level);
                showing = true;
                PoolMgr.Instance.Clear();
            });
        }
    }

    private void SendScore(float score){
        this.score = score;
    }
    private void SendLevel(int level){
        this.level = level;
    }
    private void Start()
    {
        showing = false;
        EventMgr.Instance.AddEventListener("StopShowing", DontShow);
        EventMgr.Instance.AddEventListener("StartShowing", StartShow);
        EventMgr.Instance.AddEventListener<float>("SendScore", SendScore);
        EventMgr.Instance.AddEventListener<int>("SendLevel", SendLevel);
    }
    void Update(){
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("StopShowing", DontShow);
        EventMgr.Instance.RemoveEventListener("StartShowing", StartShow);
        EventMgr.Instance.RemoveEventListener<float>("SendScore", SendScore);
        EventMgr.Instance.RemoveEventListener<int>("SendLevel", SendLevel);
    }
}

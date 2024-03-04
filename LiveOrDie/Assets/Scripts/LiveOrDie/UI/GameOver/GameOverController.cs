using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private bool showing;
    private float score, exp;
    private void DontShow(){
        showing = false;
    }
    private void StartShow(){
        if(!showing){
            PoolMgr.Instance.Clear();
            UIMgr.Instance.ShowPanel<GameOverPanel>("GameOverPanel", E_PanelLayer.Top, (panel) =>
            {
                panel.SetScore(score);
                panel.SetExp(exp);
                showing = true;
                PoolMgr.Instance.Clear();
            });
        }
    }

    private void SendScore(float score){
        this.score = score;
    }
    private void SendExp(float exp){
        this.exp = exp;
    }
    private void Start()
    {
        showing = false;
        EventMgr.Instance.AddEventListener("StopShowing", DontShow);
        EventMgr.Instance.AddEventListener("StartShowing", StartShow);
        EventMgr.Instance.AddEventListener<float>("SendScore", SendScore);
        EventMgr.Instance.AddEventListener<float>("SendExp", SendExp);
    }
    void Update(){
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("StopShowing", DontShow);
        EventMgr.Instance.RemoveEventListener("StartShowing", StartShow);
        EventMgr.Instance.RemoveEventListener<float>("SendScore", SendScore);
        EventMgr.Instance.RemoveEventListener<float>("SendExp", SendExp);
    }
}

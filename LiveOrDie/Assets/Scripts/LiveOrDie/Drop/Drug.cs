using UnityEngine.UI;

public class Drug : Drop
{
    protected override void TriggerEffect()
    {
        EventMgr.Instance.EventTrigger("DrugPicked");
        AudioMgr.Instance.PlayAudio("OnClick", false);
    }
    protected override void DestroySelf()
    {
        PoolMgr.Instance.PushObj("Prefabs/Drug", this.gameObject); //hide this object
        // Destroy(gameObject);
    }
}

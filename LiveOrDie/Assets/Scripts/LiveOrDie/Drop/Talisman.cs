using UnityEngine.UI;

public class Talisman : Drop
{
    protected override void TriggerEffect()
    {
        EventMgr.Instance.EventTrigger("DrugPicked");
    }
    protected override void DestroySelf()
    {
        PoolMgr.Instance.PushObj("Prefabs/Talisman", this.gameObject); //hide this object
    }
}

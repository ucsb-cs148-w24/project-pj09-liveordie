

using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    private TMP_Text text;

    public void Initialize(float damage)
    {
        text = GetComponent<TMP_Text>();
        text.text = damage.ToString();
    }

    public void DestroyParent()
    {
        PoolMgr.Instance.PushObj("Prefabs/DamageIndicator", transform.parent.gameObject);
    }
    
}
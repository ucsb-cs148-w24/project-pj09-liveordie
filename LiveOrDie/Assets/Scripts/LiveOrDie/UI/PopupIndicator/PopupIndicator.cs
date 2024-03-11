

using TMPro;
using UnityEngine;

public class PopupIndicator : MonoBehaviour
{
    private TMP_Text text;

    public void Initialize(string popupText)
    {
        text = GetComponent<TMP_Text>();
        text.text = popupText;
    }

    public void DestroyParent()
    {
        PoolMgr.Instance.PushObj("Prefabs/PopupIndicator", transform.parent.gameObject);
    }
    
}
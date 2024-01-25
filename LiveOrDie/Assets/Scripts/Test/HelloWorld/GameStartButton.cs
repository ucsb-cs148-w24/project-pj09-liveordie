using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartBuoon : MonoBehaviour
{
    public GameObject helloUI;
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            helloUI.SetActive(true);
        });
    }


}

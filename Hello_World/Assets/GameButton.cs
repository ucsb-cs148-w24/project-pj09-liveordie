using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    public GameObject helloUI;

    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(
            () => {
            gameObject.SetActive(false);
            helloUI.SetActive(true);
        }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    // public void func(){
    //     print("hello");
    
}

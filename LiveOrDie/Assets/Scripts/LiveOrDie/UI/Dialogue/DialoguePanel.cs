using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public struct DialogueImage
{
    public Sprite image;
    public int dialog;
};

public class DialoguePanel : BasePanel
{
    public TMP_Text textComponent;
    public string[] lines;
    public float textSpeed;
    [SerializeField]
    public DialogueImage[] dialogueImages;
    public Image imageUI;

    private int index;
    int imageIndex = 0;


    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        imageUI = GetUIComponent<Image>("DialogueImage");
        imageUI.sprite = dialogueImages[imageIndex].image;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
                //imageIndex += 1;
                //imageUI.sprite = dialogueImages[imageIndex].image;
                if (imageIndex < 8)
                {
                    
                    imageUI.sprite = dialogueImages[imageIndex].image;
                    imageIndex += 1;
                }
                else
                {
                    GoToScene("StartScene");
                }
                
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnClick(string buttonName)
    {
        switch(buttonName)
        {
            case "SkipButton":
                UIMgr.Instance.HidePanel("DialoguePanel");
                GoToScene("StartScene");
                break;
            default:
                break;
        }
    }

    private void GoToScene(string sceneName)
    {
        UIMgr.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_PanelLayer.Top); //show loading panel
        PoolMgr.Instance.Clear(); //empty the pool to prevent null reference

        SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
        {
            EventMgr.Instance.EventTrigger("ProgressBar", 1f);
            EventMgr.Instance.EventTrigger("Load" + sceneName + "Completed"); //for later usage 


        });
    }


}



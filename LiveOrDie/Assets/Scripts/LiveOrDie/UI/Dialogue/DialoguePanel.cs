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
                if (imageIndex < dialogueImages.Length - 1)
                {
                    imageIndex += 1;
                    imageUI.sprite = dialogueImages[imageIndex].image;
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
            GoToScene("StartScene");
            UIMgr.Instance.HidePanel("DialoguePanel");
        }
    }

    protected override void OnClick(string buttonName)
    {
        switch(buttonName)
        {
            case "SkipButton":
                GoToScene("StartScene");
                UIMgr.Instance.HidePanel("DialoguePanel");
                break;
            default:
                break;
        }
    }

    private void GoToScene(string sceneName)
    {
       UIMgr.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_PanelLayer.Top); 
       SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
       {
           EventMgr.Instance.EventTrigger("ProgressBar", 1f);
       });
    }


}



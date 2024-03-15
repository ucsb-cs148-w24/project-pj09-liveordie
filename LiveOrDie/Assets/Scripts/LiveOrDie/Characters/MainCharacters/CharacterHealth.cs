using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    // public/hidden variables
    [HideInInspector]
    public Image healthbar;
    [HideInInspector]
    public Player playerModel;
    [HideInInspector]
    public Transform playerPosition;
    public Material originalMat;
    public Material highlightMat;
    private PopupIndicatorFactory damageIndicatorFactory = new PopupIndicatorFactory();
    private Coroutine highlightCoroutine;

    // private variables
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    private bool sensitiveState; // if true, damaages x 2
    public void setSensitiveState(bool state){ sensitiveState = state; }
    public void SelfDestruct() { Destroy(gameObject);}
    public void DecreaseHealth(float amount){
        if(sensitiveState) playerModel.healthModifier.value -= amount*2;
        playerModel.healthModifier.value -= amount;
        playerModel.characterHealth.AddModifier("Health", playerModel.healthModifier);
        healthbar.fillAmount = playerModel.characterHealth.Value / playerModel.maxHealth.Value; 

        damageIndicatorFactory.CreateAsync(transform.position, (obj) => {
            obj.transform.SetParent(transform);
            obj.GetComponentInChildren<PopupIndicator>().Initialize(amount.ToString());
        });
        DamageHighlight();
    }

    protected virtual void DamageHighlight() 
    {
        if (highlightCoroutine != null) StopCoroutine(highlightCoroutine);
        if (gameObject.activeSelf) highlightCoroutine = StartCoroutine(DamageHighlightCoroutine());
    }

    protected virtual IEnumerator DamageHighlightCoroutine()
    {
        playerModel.render.material = highlightMat;
        yield return new WaitForSeconds(0.05f);
        playerModel.render.material = originalMat;
    }

    public void IncreaseHealth(float amount){
        if(playerModel.characterHealth.Value + amount > playerModel.maxHealth.Value)
            playerModel.healthModifier.value = playerModel.maxHealth.Value - playerModel.characterHealth.Value;
        else 
            playerModel.healthModifier.value += amount;
        playerModel.characterHealth.AddModifier("Health",playerModel.healthModifier);
        healthbar.fillAmount = playerModel.characterHealth.Value/playerModel.maxHealth.Value; 
        damageIndicatorFactory.CreateAsync(transform.position, (obj) => {
            obj.transform.SetParent(transform);
            obj.GetComponentInChildren<PopupIndicator>().Initialize("+"+amount.ToString());
        });
    }

    public void RefreshHealthUI()
    {
        healthbar.fillAmount = playerModel.characterHealth.Value/playerModel.maxHealth.Value; 
    }
    
    void OnEnable(){
        foreach(var image in gameObject.GetComponentsInChildren<Image>())
            if(image.name == "FillBlock") healthbar = image;  
        healthbar.color = healthy;
    }
    
    void Start() { 
        sensitiveState = false; 
        playerModel = GetComponentInParent<Player>();
        playerPosition = playerModel.transform;
        this.transform.position = 
            new Vector3(playerPosition.localPosition.x, 
                        playerPosition.localPosition.y + 2.0f, 0);
        this.transform.rotation = playerPosition.rotation;
    }
    void Update() //need to be moved inside refresh function
    {
        if (playerModel.characterHealth.Value <= 0){
            EventMgr.Instance.EventTrigger("PlayerDeath");
            EventMgr.Instance.EventTrigger("StartShowing");
        }
        else if(playerModel.characterHealth.Value > (0.5f*playerModel.maxHealth.Value)) {
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    public void OnDestroy(){
        if(gameObject) Destroy(gameObject);
    }
    public void OnDisable(){ if(gameObject) Destroy(gameObject); }
}

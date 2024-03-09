using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    [HideInInspector]
    public Image healthbar;

    [HideInInspector]
    public Player playerModel;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    [HideInInspector]
    public Transform playerPosition;

    private bool sensitiveState; // if true, damaages x 2
    public void setSensitiveState(bool state){
        sensitiveState = state;
    }
    public void SelfDestruct() { Destroy(gameObject);}

    public void DecreaseHealth(int amount){
        if(sensitiveState){
            amount *= 2;
        }
        playerModel.characterHealth -= amount;
        healthbar.fillAmount = playerModel.characterHealth/playerModel.maxHealth; 
    }
    
    public void IncreaseHealth(int amount){
        if(playerModel.characterHealth + amount > playerModel.maxHealth) playerModel.characterHealth = playerModel.maxHealth;
        else playerModel.characterHealth += amount;
        healthbar.fillAmount = playerModel.characterHealth/playerModel.maxHealth; 
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
    void Update()
    {
        if (playerModel.characterHealth <= 0){
            EventMgr.Instance.EventTrigger("PlayerDeath");
            EventMgr.Instance.EventTrigger("StartShowing");
        }
        else if(playerModel.characterHealth > (0.5f*playerModel.maxHealth)) {
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    public void OnDestroy(){
        if(gameObject) Destroy(gameObject);
    }
    public void OnDisable(){ if(gameObject) Destroy(gameObject); }
}

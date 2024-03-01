using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    [HideInInspector]
    public Image healthbar;

    // [HideInInspector]
    public float characterHealth = 50f; // can be changed
    public float maxHealth = 50f;
    [HideInInspector]
    public Player player;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    [HideInInspector]
    public Transform playerPosition;

    public void SelfDestruct() { Destroy(gameObject);}
    public void DecreaseHealth(int amount){
        characterHealth -= amount;
        healthbar.fillAmount = characterHealth/maxHealth; 
    }
    public void IncreaseHealth(int amount){
        if(characterHealth + amount > maxHealth) characterHealth = maxHealth;
        else characterHealth += amount;
        healthbar.fillAmount = characterHealth/maxHealth; 
    }
    void OnEnable(){
        foreach(var image in gameObject.GetComponentsInChildren<Image>())
            if(image.name == "FillBlock") healthbar = image;  
        healthbar.color = healthy;
    }
    void Start() { 
        player = GetComponentInParent<Player>();
        playerPosition = player.transform;
        this.transform.position = 
            new Vector3(playerPosition.localPosition.x, 
                        playerPosition.localPosition.y + 2.0f, 0);
        this.transform.rotation = playerPosition.rotation;
    }
    void Update()
    {
        if (characterHealth <= 0){
            EventMgr.Instance.EventTrigger("PlayerDeath");
        }
        else if(characterHealth > (0.5f*maxHealth)) {
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    public void OnDestroy(){
        if(gameObject) Destroy(gameObject);
    }
    public void OnDisable(){ if(gameObject) Destroy(gameObject); }
}

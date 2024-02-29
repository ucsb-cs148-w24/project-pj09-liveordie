using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    [HideInInspector]
    public Image healthbar;

    // [HideInInspector]
    public float characterHealth = 50f; // can be changed
    [HideInInspector]
    public Player player;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    [HideInInspector]
    public Transform playerPosition;

    public void SelfDestruct() { Destroy(gameObject);}
    public void DecreaseHealth(){
        characterHealth--;
        healthbar.fillAmount = characterHealth/50; 
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
        else if(characterHealth > 20){
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    public void OnDestroy(){
        if(gameObject) Destroy(gameObject);
    }
    public void OnDisable(){ if(gameObject) Destroy(gameObject); }
}

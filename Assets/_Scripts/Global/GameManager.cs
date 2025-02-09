using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //player default preset if there is no saving data.
    [SerializeField] private PlayerDefaultPreset playerDefaultPreset;
    [SerializeField] public ElementMultiplierTable elementMultiplierTable;

    // global variables
    public float currentMoney = 0;
    void OnEnable(){
        if(instance == null){
            instance = this;
        }else{
            Debug.LogWarning("Game Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerDefaultPreset != null){
            loadDefaultPreset();
        }
    }

    void loadDefaultPreset(){
        this.currentMoney = playerDefaultPreset.playerStartingMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

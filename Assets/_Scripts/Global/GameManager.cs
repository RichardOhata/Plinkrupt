using Esper.ESave;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool outOfBalls = false;

    //TODO: May refactor to a different class
    [HideInInspector] public BonusScoring bonusScoring = new BonusScoring();

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            
        }else{
            Debug.LogWarning("Game Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
        
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        if(outOfBalls && GameObject.FindGameObjectsWithTag("ball").Length == 0)
        {
            outOfBalls = false;
            TransitionManager.instance.OpenEndOfRoundMenu();
        }
    }


    


}

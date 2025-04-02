using UnityEngine;
using UnityEngine.EventSystems;

public class DropButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler 
{

    GameManager _gameManager;
    public void OnPointerDown(PointerEventData eventData)
    {
        _gameManager.SpawnBall();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _gameManager.EndBallDropping();
    }
    void OnEnable()
    {
        _gameManager = GameManager.Instance;        
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseBalls", menuName = "Scriptable Objects/Oracle Actions/IncreaseBalls")]
public class IncreaseBalls : OraclePack
{
    private int ballIncAmt = 2;
    public override void PerformAction()
    {
        GameManager.Instance.ballSpawner.SetNumBalls(GameManager.Instance.ballSpawner.numBalls + ballIncAmt);
    }

    public override string Description => $"Increase your total ball amount by 2 \n(Current Amount: {GameManager.Instance.ballSpawner.numBalls})";
}

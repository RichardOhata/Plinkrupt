using UnityEngine;

public class TimelineManager : MonoBehaviour
{

    public GameObject timeline;
    public GameObject UI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        disableTimeline();
    }

    public void enableTimeline()
    {
        timeline.SetActive(true);
        UI.SetActive(false);
    }

    public void disableTimeline()
    {
        timeline.SetActive(false);
        UI.SetActive(true);
    }
}

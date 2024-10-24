using Normal.Realtime;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Realtime realtime;

    public int score = 0;
    private int consecutiveShots = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        realtime = GetComponent<Realtime>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnSuccesfulShot()
    {
        consecutiveShots++;

        score += 1;

        if(consecutiveShots % 3 == 0)
        {
            score += 1;
            Debug.Log($"Player {realtime.clientID} hit 3 in a row! Bonus Point!");
        }
    }

    public void OnMissedShot()
    {
        consecutiveShots = 0;
    }

    public void ResetShots()
    {
        score = 0;
        consecutiveShots = 0;
    }
}

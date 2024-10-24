using Normal.Realtime;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score = 0;
    private int consecutiveShots = 0;
    private int playerID;

    private RealtimeView realtimeView;


    private void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
    }

    private void Start()
    {
        playerID = realtimeView.ownerIDInHierarchy;
    }

    public void OnSuccessfulShot()
    {
        if (!realtimeView.isOwnedLocallyInHierarchy) return;

        Debug.Log($"Player {playerID} scored!");

        consecutiveShots++;

        score += 1;

        if(consecutiveShots % 3 == 0)
        {
            score += 1;
            Debug.Log($"Player {playerID} hit 3 in a row! Bonus Point!");
        }

        ScoreManager.Instance.UpdatePlayerScore(playerID, score);
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

    public int GetPlayerID()
    {
        return playerID;
    }
}

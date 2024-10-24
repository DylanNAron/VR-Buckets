using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Dictionary<int, int> playerScores = new Dictionary<int, int>();

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerScore(int playerId, int newScore)
    {
        if (playerScores.ContainsKey(playerId))
        {
            playerScores[playerId] = newScore;
        }
        else
        {
            playerScores.Add(playerId, newScore);
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        string scoresToDisplay = "";
        foreach(var playerScore in playerScores) 
        {
            scoresToDisplay += $"Player {playerScore.Key} - Score: {playerScore.Value}\n";
        }

        scoreText.text = scoresToDisplay;
    }

    public int GetPlayerScore(int playerId)
    {
        if (playerScores.ContainsKey(playerId))
        {
            return playerScores[playerId];
        }
        return 0;
    }
    public static Player FindPlayerByNormcoreId(int normcorePlayerId)
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (player.GetPlayerID() == normcorePlayerId)
            {
                return player;
            }
        }
        return null;
    }
}

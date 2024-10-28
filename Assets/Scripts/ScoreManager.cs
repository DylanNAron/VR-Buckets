using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : RealtimeComponent<ScoreSyncModel>
{
    //private Dictionary<int, int> playerScores = new Dictionary<int, int>();

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

    private void Update()
    {
        UpdateScoreUI();
    }

    protected override void OnRealtimeModelReplaced(ScoreSyncModel previousModel, ScoreSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.playerScores.modelAdded -= OnPlayerScoresChanged;
        }

        if (currentModel != null)
        {
            currentModel.playerScores.modelAdded += OnPlayerScoresChanged;
        }
    }

    private void OnPlayerScoresChanged(RealtimeDictionary<PlayerScoreModel> dictionary, uint key, PlayerScoreModel model, bool remote)
    {
        UpdateScoreUI();
    }

    public void UpdatePlayerScore(uint playerId, int newScore)
    {

        PlayerScoreModel playerScoreModel;

        if (model.playerScores.TryGetValue(playerId, out playerScoreModel))
        {
            playerScoreModel.score = newScore;
        }
        else
        {
            playerScoreModel = new PlayerScoreModel
            {
                playerID = (int)playerId,
                score = newScore
            };
            model.playerScores.Add(playerId, playerScoreModel);
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        string scoresToDisplay = "";
        foreach(var playerScore in model.playerScores) 
        {
            PlayerScoreModel playerScoreModel = playerScore.Value;
            scoresToDisplay += $"Player {playerScoreModel.playerID} - Score: {playerScoreModel.score}\n";
        }

        scoreText.text = scoresToDisplay;
    }

    public int GetPlayerScore(uint playerId)
    {
        PlayerScoreModel playerScoreModel;

        if (model.playerScores.TryGetValue(playerId, out playerScoreModel))
        {
            return playerScoreModel.score;
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

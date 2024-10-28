using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class ScoreSyncModel
{
    [RealtimeProperty(1, true, true, true)]
    private RealtimeDictionary<PlayerScoreModel> _playerScores;
}

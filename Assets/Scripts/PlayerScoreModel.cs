using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class PlayerScoreModel
{
    [RealtimeProperty(1, true, true)]
    private int _playerID;

    [RealtimeProperty(2, true, true)]
    private int _score;
}

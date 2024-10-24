using Normal.Realtime;
using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BasketballController ball = other.GetComponent<BasketballController>();
        if (ball != null)
        {
            int playerId = ball.GetLastPlayerId();
            if (playerId != -1)
            {
                Player player = ScoreManager.FindPlayerByNormcoreId(playerId);
                if (player != null)
                {
                    player.OnSuccessfulShot();
                }
            }
        }

    }
}

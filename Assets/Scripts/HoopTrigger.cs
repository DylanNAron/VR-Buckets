using Normal.Realtime;
using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

            if(audioSource != null) 
            {
                audioSource.Play();
            }

            ball.ResetLastPlayerId();
        }
    }
}

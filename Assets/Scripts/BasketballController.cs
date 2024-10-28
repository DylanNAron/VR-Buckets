using Normal.Realtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BasketballController : MonoBehaviour
{
    private RealtimeTransform realtimeTransform;
    private XRGrabInteractable xrGrabInteractable;

    [SerializeField]
    private int lastPlayerId = -1;

    void Start()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
        xrGrabInteractable = GetComponent<XRGrabInteractable>();

        xrGrabInteractable.selectEntered.AddListener(OnGrabBall);
    }

    void OnGrabBall(SelectEnterEventArgs args)
    {
        realtimeTransform.RequestOwnership();
        lastPlayerId = realtimeTransform.ownerIDSelf;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (lastPlayerId != -1)
            {
                Player player = ScoreManager.FindPlayerByNormcoreId(lastPlayerId);
                if (player != null)
                {
                    player.OnMissedShot();
                    ResetLastPlayerId();
                }
            }
        }
    }

    public int GetLastPlayerId()
    {
        return lastPlayerId;
    }

    public void ResetLastPlayerId()
    {
        lastPlayerId = -1;
    }

    void OnDestroy()
    {
        if (xrGrabInteractable != null)
        {
            xrGrabInteractable.selectEntered.RemoveListener(OnGrabBall);
        }
    }
}

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
        xrGrabInteractable.selectExited.AddListener(OnReleaseBall);
    }

    void OnGrabBall(SelectEnterEventArgs args)
    {
        realtimeTransform.RequestOwnership();
        lastPlayerId = realtimeTransform.ownerIDSelf;
    }

    void OnReleaseBall(SelectExitEventArgs args)
    {
    }

    public int GetLastPlayerId()
    {
        return lastPlayerId;
    }

    void OnDestroy()
    {
        if (xrGrabInteractable != null)
        {
            xrGrabInteractable.selectEntered.RemoveListener(OnGrabBall);
        }
    }
}

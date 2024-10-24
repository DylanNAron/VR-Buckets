using Normal.Realtime;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BasketballController : MonoBehaviour
{
    private RealtimeTransform realtimeTransform;
    private XRGrabInteractable xrGrabInteractable;
    private Rigidbody rigidBody;

    private int lastPlayerId = -1;

    void Start()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();

        xrGrabInteractable.selectEntered.AddListener(OnGrabBall);
        xrGrabInteractable.selectExited.AddListener(OnReleaseBall);
    }

    void Update()
    {
    }

    void OnGrabBall(SelectEnterEventArgs args)
    {
        realtimeTransform.RequestOwnership();
        lastPlayerId = realtimeTransform.ownerIDSelf;
    }

    void OnReleaseBall(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            rigidBody.linearVelocity = interactor.GetComponent<Rigidbody>().linearVelocity;
            //rigidBody.angularVelocity = interactor.GetComponent<Rigidbody>().angularVelocity;
        }
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

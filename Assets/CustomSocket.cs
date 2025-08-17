using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CustomSocket : XRSocketInteractor
{
    // Called when an interactable is detached from the socket
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Get the GameObject that was detached
        var interactableObject = args.interactableObject.transform.gameObject;

        // Try to get the Rigidbody and set isKinematic to false
        var rb = interactableObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}

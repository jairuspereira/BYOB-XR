using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public class WirePlug : MonoBehaviour
{
    public string colorTag = "Red"; // or "Black"
    public Transform tip;
    [HideInInspector] public Wire parentWire;

    Rigidbody rb;
    XRGrabInteractable grab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    // Called by Terminal when this plug is snapped in/out
    public void OnPluggedInto(Terminal t)  => parentWire?.SetConnection(this, t);
    public void OnUnpluggedFrom(Terminal t)=> parentWire?.ClearConnection(this, t);

    // Safety: ensure physics re-enable when leaving a socket or release
    void OnEnable()
    {
        grab.selectExited.AddListener(_ =>
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        });
    }
}
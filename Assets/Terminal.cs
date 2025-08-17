using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public enum DeviceType { Battery, Motor }
public enum TerminalKind { Positive, Negative }

[RequireComponent(typeof(XRSocketInteractor))]
public class Terminal : MonoBehaviour
{
    public DeviceType device;
    public TerminalKind kind;
    public CircuitGraph circuit;

    XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
        socket.selectExited.AddListener(OnSelectExited);
    }
    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
        socket.selectExited.RemoveListener(OnSelectExited);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        var plug = args.interactableObject.transform.GetComponentInParent<WirePlug>();
        if (plug) plug.OnPluggedInto(this);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        var plug = args.interactableObject.transform.GetComponentInParent<WirePlug>();
        if (plug) plug.OnUnpluggedFrom(this);
    }
}
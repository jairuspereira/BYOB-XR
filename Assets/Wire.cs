using UnityEngine;

public class Wire : MonoBehaviour
{
    public WirePlug endA;
    public WirePlug endB;
    public CircuitGraph circuit;

    public Terminal terminalA { get; private set; }
    public Terminal terminalB { get; private set; }

    void OnEnable()
    {
        endA.parentWire = this;
        endB.parentWire = this;
    }

    public void SetConnection(WirePlug plug, Terminal terminal)
    {
        if (plug == endA) terminalA = terminal;
        else if (plug == endB) terminalB = terminal;
        circuit?.OnConnectionsChanged();
    }

    public void ClearConnection(WirePlug plug, Terminal terminal)
    {
        if (plug == endA && terminalA == terminal) terminalA = null;
        else if (plug == endB && terminalB == terminal) terminalB = null;
        circuit?.OnConnectionsChanged();
    }

    public bool Connects(Terminal a, Terminal b)
    {
        return (terminalA == a && terminalB == b) || (terminalA == b && terminalB == a);
    }
}
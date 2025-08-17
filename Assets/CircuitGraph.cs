using System.Linq;
using UnityEngine;

public class CircuitGraph : MonoBehaviour
{
    public Wire[] wires;                 // assign both Red & Black
    public MotorController motor;        // assign your motor

    public void OnConnectionsChanged()
    {
        // Find which wire connects which pair of terminals
        var allTerminals = FindObjectsByType<Terminal>(FindObjectsSortMode.None); // fine for small scenes
        var batPlus = allTerminals.FirstOrDefault(t => t.device == DeviceType.Battery && t.kind == TerminalKind.Positive);
        var batMinus= allTerminals.FirstOrDefault(t => t.device == DeviceType.Battery && t.kind == TerminalKind.Negative);
        var motPlus = allTerminals.FirstOrDefault(t => t.device == DeviceType.Motor && t.kind == TerminalKind.Positive);
        var motMinus= allTerminals.FirstOrDefault(t => t.device == DeviceType.Motor && t.kind == TerminalKind.Negative);

        if (!batPlus || !batMinus || !motPlus || !motMinus) { motor.SetPower(false, 0); return; }

        // Which wires connect a Battery terminal to a Motor terminal?
        Wire branch1 = wires.FirstOrDefault(w =>
            (w.terminalA && w.terminalB) &&
            (w.terminalA.device != w.terminalB.device)); // one Battery, one Motor

        Wire branch2 = wires.LastOrDefault(w =>
            w != branch1 &&
            (w.terminalA && w.terminalB) &&
            (w.terminalA.device != w.terminalB.device));

        if (branch1 == null || branch2 == null) { motor.SetPower(false, 0); return; }

        // Now check polarity mapping to decide direction
        bool plusToPlus   = branch1.Connects(batPlus, motPlus) || branch2.Connects(batPlus, motPlus);
        bool minusToMinus = branch1.Connects(batMinus, motMinus) || branch2.Connects(batMinus, motMinus);
        bool plusToMinus  = branch1.Connects(batPlus, motMinus) || branch2.Connects(batPlus, motMinus);
        bool minusToPlus  = branch1.Connects(batMinus, motPlus) || branch2.Connects(batMinus, motPlus);

        if ((plusToPlus && minusToMinus) || (plusToMinus && minusToPlus))
        {
            int dir = (plusToPlus && minusToMinus) ? +1 : -1;
            motor.SetPower(true, dir);
        }
        else
        {
            motor.SetPower(false, 0);
        }
    }
}
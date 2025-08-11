using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

public class ChangeColor : XRBaseInputInteractor
{
    // This script is attached to an interactable object to change its color when hovered or grabbed

    private Renderer objectRenderer;
    private Color originalColor;

    [Header("Color Settings")]
    public Color hoverColor = new Color(1f, 1f, 0f, 1f); // Assign in Inspector, includes alpha

    [Range(0f, 1f)]
    public float hoverAlpha = 1f; // Option for alpha value

    [Tooltip("Assign the Renderer whose color should be changed. If left empty, will use this object's Renderer.")]
    public Renderer targetRenderer;

    protected override void Awake()
    {
        base.Awake();
        // Use targetRenderer if assigned, otherwise get Renderer from this object
        objectRenderer = targetRenderer != null ? targetRenderer : GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        // Apply alpha value from hoverAlpha
        Color colorWithAlpha = hoverColor;
        colorWithAlpha.a = hoverAlpha;
        ChangeObjectColor(colorWithAlpha);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        ChangeObjectColor(originalColor);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        var interactor = args.interactorObject.transform;

        string hand = interactor.name.ToLower().Contains("left") ? "Left Hand"
                     : interactor.name.ToLower().Contains("right") ? "Right Hand"
                     : "Unknown Controller";

        Debug.Log($"Object '{gameObject.name}' picked up by: {hand}");
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ChangeObjectColor(originalColor); // Restore original color when released
    }

    private void ChangeObjectColor(Color newColor)
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = newColor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseScreenRay))]
[RequireComponent(typeof(InteractionTargetSelector))]
public class InteractionController : MonoBehaviour
{
    public static InteractionController Instance;

    private MouseScreenRay _mouseScreenRay;
    private InteractionTargetSelector _interactionTargetSelector;
    private HighlightController _highlightController;

    private Transform _currentSelection;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _mouseScreenRay = GetComponent<MouseScreenRay>();
        _interactionTargetSelector = GetComponent<InteractionTargetSelector>();
        _highlightController = HighlightController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSelection != null)
        {
            _highlightController.ClearHighlight(_currentSelection);
        }
        _interactionTargetSelector.CheckInteractionTarget(_mouseScreenRay.CreateRay());

        _currentSelection = _interactionTargetSelector.GetInteractionTarget();
        if (_currentSelection != null)
        {
            _highlightController.SetHighlight(_currentSelection);
        }
    }
}

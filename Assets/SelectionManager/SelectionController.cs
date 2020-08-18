using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    private IRayProvider _rayProvider;
    private ISelector _selector;
    private ISelectionResponse _selectionResponse;

    private Transform _currentSelection;

    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<ISelector>();
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSelection != null)
        {
            _selectionResponse.OnDeselect(_currentSelection);
        }

        _selector.Check(_rayProvider.CreateRay());

        _currentSelection = _selector.GetSelection();

        if (_currentSelection != null)
        {
            _selectionResponse.OnSelect(_currentSelection);
        }
    }
}

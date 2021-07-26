using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;

    [SerializeField] private GameObject _interactionTargetInfoUI;

    [SerializeField] private GameObject _sliderHealthBar;
    [SerializeField] private GameObject _txtHealthBarValue;
    [SerializeField] private GameObject _txtName;
    [SerializeField] private GameObject _txtDescription;

    private InteractionTargetSelector _interactionTargetSelector;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _interactionTargetSelector = InteractionTargetSelector.Instance;
        _interactionTargetSelector.onInteractionTargetChanged += OnInteractionTargetChanged;

        ClearInteractionTargetInfo();
        _interactionTargetInfoUI.SetActive(false);
    }

    void OnInteractionTargetChanged(Transform interactionTarget)
    {
        if (_interactionTargetInfoUI != null) {

            ClearInteractionTargetInfo();
            if (interactionTarget == null)
            {
                _interactionTargetInfoUI.SetActive(false);
            } else
            {
                if (LayerUtilities.IsObjectInLayerByName("Collectable", interactionTarget.gameObject))
                {
                    SetCollectableInfo(interactionTarget.gameObject);
                } else if (LayerUtilities.IsObjectInLayerByName("Creature", interactionTarget.gameObject))
                {
                    SetCreatureInfo(interactionTarget.gameObject);
                }
                _interactionTargetInfoUI.SetActive(true);
            }
        }
    }

    private void SetCollectableInfo(GameObject collectable)
    {
        ItemInstance itemInstance = collectable.GetComponent<ItemInstance>();
        if (itemInstance != null)
        {
            ItemData itemData = itemInstance.Item;
            UiElementUtilities.SetTextMeshProValue(_txtName, itemData.Name);
            UiElementUtilities.SetTextMeshProValue(_txtDescription, itemData.Description);
        }

        _txtName.SetActive(true);
        _txtDescription.SetActive(true);
    }

    private void SetCreatureInfo(GameObject creature)
    {
        CreatureStats creatureStats = creature.GetComponent<CreatureStats>();
        if (creatureStats != null)
        {
            UiElementUtilities.SetTextMeshProValue(_txtName, creatureStats.Name);

            float sliderValue = Mathf.Ceil((creatureStats.Health / creatureStats.MaxHealth.GetValue()) * 100);
            _sliderHealthBar.GetComponent<Slider>().value = sliderValue;

            string healthText = $"{creatureStats.Health} / {creatureStats.MaxHealth.GetValue()}";
            UiElementUtilities.SetTextMeshProValue(_txtHealthBarValue, healthText);
        }

        _txtName.SetActive(true);
        _sliderHealthBar.SetActive(true);
    }

    private void ClearInteractionTargetInfo()
    {
        _sliderHealthBar.SetActive(false);
        _txtName.SetActive(false);
        _txtDescription.SetActive(false);
    }
}

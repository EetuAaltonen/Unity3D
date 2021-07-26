using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionTargetSelector : MonoBehaviour
{
    public static InteractionTargetSelector Instance;

    public delegate void OnInteractionTargetChanged(Transform interactionTarget);
    public OnInteractionTargetChanged onInteractionTargetChanged;

    public List<InteractionLayer> InteractionLayers;
    public float Treshold;

    private Transform _interactionTarget;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckInteractionTarget(Ray ray)
    {
        Transform newInteractionTarget = null;

        GameObject[] gameObjectArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        Transform playerTransform = CharacterUtilities.FindCharacterPlayer().transform;
        Vector3 vector1 = ray.direction;
        float closest = 0f;

        foreach (GameObject gameObject in gameObjectArray)
        {
            InteractionLayer interactionLayer = InteractionLayers.Find(x => LayerUtilities.IsObjectInLayer(x.Layer.value, gameObject));
            if (interactionLayer != null) {
                if (IsValidTarget(gameObject)) {
                    if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= interactionLayer.MaxDistance) {
                        Vector3 vector2 = gameObject.transform.position - ray.origin;
                        float lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);

                        if (lookPercentage > Treshold && lookPercentage > closest)
                        {
                            closest = lookPercentage;
                            newInteractionTarget = gameObject.transform;
                        }
                    }
                }
            }
        }

        if (_interactionTarget != newInteractionTarget)
        {
            _interactionTarget = newInteractionTarget;
            // Trigger onInteractionTargetChanged
            if (onInteractionTargetChanged != null)
            {
                onInteractionTargetChanged.Invoke(_interactionTarget);
            }
        }
    }

    public bool IsValidTarget(GameObject target)
    {
        bool isValidTarget = true;

        // Filter out objects without collision detection
        Collider collider = target.GetComponent<Collider>();
        if (collider != null)
        {
            if (!collider.enabled) { isValidTarget = false; }
        }

        // Filter out dead targets
        CreatureStats creatureStats = target.GetComponent<CreatureStats>();
        if (creatureStats != null)
        {
            if (creatureStats.IsDead()) { isValidTarget = false; }
        }

        return isValidTarget;
    }

    public Transform GetInteractionTarget()
    {
        return _interactionTarget;
    }
}

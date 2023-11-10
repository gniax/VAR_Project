using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrouserObjectiveChecker : MonoBehaviour
{
    private XRBaseInteractable _interactable;
    public Collider TargetCollider;
    private Collider _thisCollider;
    private bool _hasBeenDropped;

    void Awake()
    {
        _hasBeenDropped = false;
        _interactable = GetComponent<XRBaseInteractable>();
        if (_interactable == null)
        {
            Debug.LogError("This script must have an XR Base Interactable!");
            this.enabled = false;
            return;
        }

        _interactable.selectExited.AddListener(OnReleased);

        _thisCollider = this.GetComponent<Collider>();

        if (TargetCollider == null || _thisCollider == null)
        {
            Debug.LogError("Please link the target collider & add a collider to this object in order to make this script work.");
            this.enabled = false;
            return;
        }
    }

    private void OnCollisionEnter(Collision pCollision)
    {
        Collider lCollider = pCollision.collider;
        // pour valider l'objectif, il faut que:
        // - aucun objet ne soit dans une des mains
        // - l'objet cible rencontre this
        if (_thisCollider == lCollider || ObjectivesManager.Instance.IsValidated(6))
        {
            return;
        }

        if (_hasBeenDropped)
        {
            ObjectivesManager.Instance.ValidateObjective_7();
        }
    }

    private void OnReleased(SelectExitEventArgs arg)
    {
        _hasBeenDropped = true;
    }

    void OnDestroy()
    {
        _interactable.selectExited.RemoveListener(OnReleased);
    }
}

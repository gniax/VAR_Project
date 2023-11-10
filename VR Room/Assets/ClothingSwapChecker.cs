using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClothingSwapChecker : MonoBehaviour
{
    public XRSocketInteractor JacketSocket;
    public XRSocketInteractor TshirtSocket;
    public XRBaseInteractable Jacket;
    public XRBaseInteractable Tshirt;

    public void Awake()
    {
        if (! (JacketSocket && TshirtSocket && Jacket && Tshirt))
        {
            Debug.LogError("Please put your references to make this script work.");
            return;
        }
    }

    private void Update()
    {
        if (! ObjectivesManager.Instance.IsValidated(7))
        {
            CheckSwap();
        }
    }

    private void CheckSwap()
    {
        var currentJacketSocketItem = JacketSocket.firstInteractableSelected;
        var currentTShirtSocketItem = TshirtSocket.firstInteractableSelected;

        bool isJacketOnTShirtSocket = currentTShirtSocketItem == Jacket;
        bool isTShirtOnJacketSocket = currentJacketSocketItem == Tshirt;

        if (isJacketOnTShirtSocket && isTShirtOnJacketSocket)
        {
            ObjectivesManager.Instance.ValidateObjective_8();
        }
    }
}
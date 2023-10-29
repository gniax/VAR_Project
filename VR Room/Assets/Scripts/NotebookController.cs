using UnityEngine;
using UnityEngine.InputSystem;

public class NotebookController : MonoBehaviour
{
    public GameObject NotebookCover;
    public float OpenSpeed = 200.0f;
    private Vector3 OpenRotation = new Vector3(0, 0, 190); 
    private Vector3 ClosedRotation;
    private bool isOpen = false;
    private bool shouldAnimate = false;
    private Keyboard keyboard;

    void Start()
    {
        ClosedRotation = NotebookCover.transform.localEulerAngles;
        keyboard = InputSystem.GetDevice<Keyboard>();
    }

    void Update()
    {
        if (keyboard.oKey.wasPressedThisFrame)
        {
            isOpen = !isOpen;
            shouldAnimate = true;
        }

        if (shouldAnimate)
        {
            Quaternion targetRotation = isOpen ? Quaternion.Euler(ClosedRotation) : Quaternion.Euler(OpenRotation);  // Reversed
            NotebookCover.transform.localRotation = Quaternion.RotateTowards(
                NotebookCover.transform.localRotation,
                targetRotation,
                OpenSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(NotebookCover.transform.localRotation, targetRotation) < 1f)
            {
                shouldAnimate = false;
            }
        }
    }
}

using UnityEngine;

public class NotebookController : MonoBehaviour
{
    public GameObject NotebookCover;
    public float OpenSpeed = 200.0f;
    private Vector3 OpenRotation = new Vector3(0, 0, 190);
    private Vector3 ClosedRotation;
    private bool isOpen = false;
    private bool shouldAnimate = false;

    void Start()
    {
        ClosedRotation = NotebookCover.transform.localEulerAngles;
    }

    void Update()
    {
        if (shouldAnimate)
        {
            Quaternion targetRotation = isOpen ? Quaternion.Euler(OpenRotation) : Quaternion.Euler(ClosedRotation);
            NotebookCover.transform.localRotation = Quaternion.RotateTowards(
                NotebookCover.transform.localRotation,
                targetRotation,
                OpenSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(NotebookCover.transform.localRotation, targetRotation) < 1f)
            {
                shouldAnimate = false;
                isOpen = !isOpen;
            }
        }
    }

    public void OpenBook()
    {
        if (!shouldAnimate) // Only trigger animation if not already animating
        {
            isOpen = !isOpen;
            shouldAnimate = true;
        }
    }
}

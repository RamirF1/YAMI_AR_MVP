using UnityEngine;

public class CaptureController : MonoBehaviour
{
    public Camera arCamera;
    public LayerMask spiritLayer;

    private float captureWindow;

    private void Start()
    {
        captureWindow = DifficultyManager.Instance.captureWindow;
    }

    private void Update()
    {
        // Capture on tap or space (debug)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            TryCapture();
        }
    }

    private void TryCapture()
    {
        // Always cast from the center of the AR view
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = arCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, spiritLayer))
        {
            SpiritController spirit = hit.collider.GetComponent<SpiritController>();

            if (spirit != null)
            {
                spirit.Capture();
            }
        }
    }
}

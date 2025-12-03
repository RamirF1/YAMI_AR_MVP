using System.Collections;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Reveal,
        Aggro,
        Captured,
        Escaped
    }

    [Header("State Machine")]
    public State currentState = State.Idle;

    // Enables FSM transition system (only used for FSM ghost)
    public bool useFSM = false;

    [Header("Spirit Data (Lore, Name, Reward Points)")]
    public SpiritData spiritData;

    [Header("Behaviour Settings")]
    public float moveSpeed;
    public float flickerChance;

    private Renderer spiritRenderer;
    private Vector3 startPos;
    private bool isVisible = false;
    private float revealTimer;

    private void Awake()
    {
        spiritRenderer = GetComponent<Renderer>();
        startPos = transform.position;
        spiritRenderer.enabled = false;   // Start invisible
    }

    private void Start()
    {
        // Get difficulty-based values
        moveSpeed = DifficultyManager.Instance.ghostSpeed;
        flickerChance = DifficultyManager.Instance.flickerChance;

        // FSM: automatic state transitions
        if (useFSM)
        {
            Invoke(nameof(EnterReveal), 2f);     // After 2 seconds → Reveal
            Invoke(nameof(EnterAggro), 5f);      // After 5 seconds → Aggro
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;

            case State.Reveal:
                HandleReveal();
                break;

            case State.Aggro:
                HandleAggro();
                break;

            case State.Captured:
                // Do nothing, handled by Capture()
                break;

            case State.Escaped:
                // Escape behaviour is optional
                break;
        }
    }

    // ------------ STATE HANDLERS ------------

    private void HandleIdle()
    {
        // Static ghost does NOTHING until CaptureController hits it
        if (!isVisible)
        {
            // Idle stays invisible until player looks at it
            TryRevealOnPlayerLook();
        }
    }

    private void HandleReveal()
    {
        // Floating fade-in effect
        if (!isVisible)
        {
            spiritRenderer.enabled = true;
            isVisible = true;
        }

        // Floating motion (slight up/down)
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * 2f) * 0.05f, 0);
    }

    private void HandleAggro()
    {
        // Ensure ghost is visible
        if (!isVisible)
        {
            spiritRenderer.enabled = true;
            isVisible = true;
        }

        // Side-to-side movement
        transform.position = startPos + new Vector3(Mathf.Sin(Time.time * moveSpeed) * 0.3f, 0, 0);

        // Flicker
        TryFlicker();
    }

    // ------------ STATE TRANSITIONS ------------

    public void EnterReveal()
    {
        currentState = State.Reveal;
    }

    public void EnterAggro()
    {
        currentState = State.Aggro;
    }

    // ------------ FLICKER SYSTEM ------------

    private void TryFlicker()
    {
        if (Random.value < flickerChance * Time.deltaTime)
        {
            spiritRenderer.enabled = !spiritRenderer.enabled;
        }
    }

    // ------------ VISIBILITY CHECK ------------

    private void TryRevealOnPlayerLook()
    {
        // Simple check → becomes visible when game wants (optional)
        if (!isVisible)
        {
            spiritRenderer.enabled = true;
            isVisible = true;
        }
    }

    // ------------ CAPTURE ------------

    public void Capture()
    {
        if (currentState == State.Captured)
            return;

        currentState = State.Captured;

        // Give player points
        GameManager.Instance.AddSpiritPoints(spiritData.rewardPoints);

        // Show lore text in UI
        UIManager.Instance.ShowLore(spiritData.loreDescription);

        // Disable renderer
        if (spiritRenderer != null)
            spiritRenderer.enabled = false;

        // Destroy ghost object
        Destroy(gameObject, 0.2f);
    }

    // ------------ ESCAPE (OPTIONAL) ------------

    public void Escape()
    {
        currentState = State.Escaped;

        if (spiritRenderer != null)
            spiritRenderer.enabled = false;

        Destroy(gameObject, 0.2f);
    }
}


using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.Collections.Generic;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Prefabs")]
    public GameObject staticGhostPrefab;
    public GameObject movingGhostPrefab;
    public GameObject fsmGhostPrefab;

    [Header("AR References")]
    public Transform arCamera;
    public ARRaycastManager raycastManager;

    private GameObject currentGhost;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    private IEnumerator SpawnAfterDelay()
    {
        // Wait for AR Foundation to find surfaces
        yield return new WaitForSeconds(0.8f);

        SpawnGhost();
    }

    public void SpawnGhost()
    {
        if (currentGhost != null)
            Destroy(currentGhost);

        GameObject prefabToSpawn = GetGhostForDifficulty();

        // Raycast from the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            // Plane hit, get the pose
            Pose pose = hits[0].pose;

            // Spawn ghost at plane position
            currentGhost = Instantiate(prefabToSpawn, pose.position, pose.rotation);

            // Add AR anchor so ghost never drifts or moves with finger
            currentGhost.AddComponent<ARAnchor>();
        }
        else
        {
            // No plane found: fallback in front of camera, still anchor
            Vector3 spawnPos = arCamera.position + arCamera.forward * 2f;
            currentGhost = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            currentGhost.AddComponent<ARAnchor>();
        }
    }

    private GameObject GetGhostForDifficulty()
    {
        var diff = DifficultyManager.Instance.difficulty;

        switch (diff)
        {
            case DifficultyManager.Difficulty.Sane:
                return staticGhostPrefab;

            case DifficultyManager.Difficulty.Insane:
                return movingGhostPrefab;

            case DifficultyManager.Difficulty.Madhouse:
                return fsmGhostPrefab;

            default:
                return staticGhostPrefab;
        }
    }
}


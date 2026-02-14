using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Setup")]
    public GameObject segmentPrefab;
    public int maxSegments = 10;
    public int health = 60;
    public int maxHealth = 100;
    public float spacing = 0.1f; // distance between segments

    private List<GameObject> segments = new List<GameObject>();

    void Awake()
    {
        BuildSegments();
        SetHealth(health, maxHealth);
    }

    private void BuildSegments()
    {
        // Clear old segments (useful if maxSegments changes in editor)
        foreach (var seg in segments)
        {
            Destroy(seg);
        }

        segments.Clear();

        // Create new segments
        for (int i = 0; i < maxSegments; i++)
        {
            var seg = Instantiate(segmentPrefab, transform);
            seg.transform.localPosition = new Vector3(i * spacing, 0f, 0f);
            segments.Add(seg);
        }
    }

    public void SetHealth(float current, float max)
    {
        float ratio = Mathf.Clamp01(current / max);
        int activeSegments = Mathf.RoundToInt(ratio * maxSegments);
        Debug.Log("Setting health: " + current + "/" + max + " (" + activeSegments + " segments active)");
        for (int i = 0; i < segments.Count; i++)
        {
            var renderer = segments[i].GetComponent<SpriteRenderer>();
            renderer.color = i < activeSegments ? UnityEngine.Color.green : UnityEngine.Color.red;
        }
    }
}

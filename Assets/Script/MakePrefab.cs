using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakePrefab : MonoBehaviour
{
    public GameObject[] round1Variants;
    public float[] round1YPositions = new float[] { -3.7f, -3.0f };
    public GameObject[] round2Variants;
    public float[] round2YPositions = new float[] { -2.5f, -1.8f };
    public GameObject[] round3Variants;
    public float[] round3YPositions = new float[] { 1.0f, 2.0f };
    public GameObject[] round4Variants;
    public float[] round4YPositions = new float[] { 1.5f, 2.5f };
    public GameObject[] round5Variants;
    public float[] round5YPositions = new float[] { 1f, 1.5f };
    public GameObject[] round6Variants;
    public float[] round6YPositions = new float[] { 2f, 2.5f };

    public GameObject[] randomObstaclePrefabs;
    public float randomObstacleY = 0f;

    public Text scoreText;

    private float timer = 0f;
    public Vector3 spawnPosition = new Vector3(10f, 0f, 0f);

    public float[] roundSpeeds = new float[] { 2f, 2.5f, 5f, 3.5f, 4f, 4.5f };
    public float[] roundSpawnIntervals = new float[] { 5f, 5f, 2f, 4f, 3f, 3f };

    public List<GameObject> activePrefabs = new List<GameObject>();

    void Start()
    {
        var startGameScript = gameObject.GetComponent<StartGame>();
        if (startGameScript != null)
            startGameScript.enabled = true;
    }

    void Update()
    {
        if (StopGameScript.IsPaused || !this.enabled) return;

        int score = ParseScoreFromText();
        int roundIndex = GetRoundIndex(score);

        if (roundIndex < 0 || roundIndex >= roundSpawnIntervals.Length)
            return;

        float currentInterval = roundSpawnIntervals[roundIndex];
        float speed = roundSpeeds[roundIndex];

        timer += Time.deltaTime;
        if (timer < currentInterval)
            return;

        timer -= currentInterval;

        Debug.Log($"[Spawn] Score: {score}, RoundIndex: {roundIndex}, Interval: {currentInterval}, Speed: {speed}");

        GameObject prefabToSpawn = null;
        float yForSpawn = 0f;
        GameObject[] variants = null;
        float[] variantYs = null;

        switch (roundIndex)
        {
            case 0:
                variants = round1Variants;
                variantYs = round1YPositions;
                break;
            case 1:
                variants = round2Variants;
                variantYs = round2YPositions;
                break;
            case 2:
                variants = round3Variants;
                variantYs = round3YPositions;
                break;
            case 3:
                variants = round4Variants;
                variantYs = round4YPositions;
                break;
            case 4:
                variants = round5Variants;
                variantYs = round5YPositions;
                break;
            case 5:
                variants = round6Variants;
                variantYs = round6YPositions;
                break;
        }

        if (variants != null && variants.Length > 0)
        {
            int randomIndex = Random.Range(0, variants.Length);
            prefabToSpawn = variants[randomIndex];
            if (variantYs != null && randomIndex < variantYs.Length)
                yForSpawn = variantYs[randomIndex];
        }

        if (prefabToSpawn != null)
            SpawnPrefab(prefabToSpawn, yForSpawn, speed);

        if (score >= 200 && randomObstaclePrefabs != null && randomObstaclePrefabs.Length > 0)
        {
            if (Random.value < 0.5f)
            {
                GameObject obstacle = GetRandomPrefabFromArray(randomObstaclePrefabs);
                if (obstacle != null)
                    SpawnObstacle(obstacle);
            }
        }
    }

    void SpawnPrefab(GameObject prefab, float baseY, float speed)
    {
        if (StopGameScript.IsPaused) return;

        Vector3 spawnPos = new Vector3(spawnPosition.x, baseY, 0f);
        GameObject clone = Instantiate(prefab, spawnPos, Quaternion.identity);
        activePrefabs.Add(clone);
        Destroy(clone, 12f);

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.left * speed;
    }

    void SpawnObstacle(GameObject prefab)
    {
        if (StopGameScript.IsPaused) return;

        Vector3 spawnPos = new Vector3(spawnPosition.x, randomObstacleY, spawnPosition.z);
        GameObject clone = Instantiate(prefab, spawnPos, Quaternion.identity);
        activePrefabs.Add(clone);
        Destroy(clone, 12f);

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.left * 3.5f;
    }

    int GetRoundIndex(int score)
    {
        if (score >= 500) return 5;
        else if (score >= 400) return 4;
        else if (score >= 300) return 3;
        else if (score >= 200) return 2;
        else if (score >= 100) return 1;
        else return 0;
    }

    GameObject GetRandomPrefabFromArray(GameObject[] arr)
    {
        if (arr == null || arr.Length == 0)
            return null;

        List<GameObject> validPrefabs = new List<GameObject>();
        foreach (var go in arr)
            if (go != null)
                validPrefabs.Add(go);

        if (validPrefabs.Count == 0)
            return null;

        int index = Random.Range(0, validPrefabs.Count);
        return validPrefabs[index];
    }

    int ParseScoreFromText()
    {
        if (scoreText == null)
            return 0;

        string text = scoreText.text.Trim();

        if (text.Contains(":"))
        {
            string[] parts = text.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int parsedScore))
                return parsedScore;
        }
        else if (int.TryParse(text, out int parsedDirectScore))
        {
            return parsedDirectScore;
        }

        Debug.LogWarning($"[MakePrefab] 점수 파싱 실패: '{text}'");
        return 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject NormalEnemyPrefab;
    public GameObject FastEnemyPrefab;
    public GameObject StrongEnemyPrefab;
    public Transform SpawnPoint;
    private TextMeshProUGUI WaveCounterText;
    private TextMeshProUGUI NextWaveCountdownText;
    private GameObject Wave11Screen;
    public int WaveNumber { get; set; }
    private int NormalEnemiesToSpawn { get; set; }
    private int FastEnemiesToSpawn { get; set; }
    private int StrongEnemiesToSpawn { get; set; }
    private float SpawnSpeed { get; set; }
    private bool Restarting { get; set; }
    public int EnemiesRemaining { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WaveCounterText = GameObject.Find("Game overlay").GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "Wave count");
        NextWaveCountdownText = GameObject.Find("Game overlay").GetComponentsInChildren<TextMeshProUGUI>(true).First(x => x.name == "Next wave countdown");
        Wave11Screen = GameObject.Find("Hidden").GetComponentsInChildren<Canvas>(true).First(x => x.name == "Wave 11 panel").gameObject;
        WaveNumber = 10;
        LoadWave();
    }

    private void LoadWave()
    {
        // If we are past wave 10, just load wave 10.
        Debug.Log("Wave Number: " + WaveNumber);
        WaveCounterText.text = $"Wave {WaveNumber}";
        int waveToLoad = WaveNumber;
        if (WaveNumber > 10)
            waveToLoad = 10;

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load($@"{Application.streamingAssetsPath}\Waves.xml");

        XmlNodeList waveNodes = xmlDoc.GetElementsByTagName("wave");
        XmlNode waveNode = waveNodes[waveToLoad - 1];
        XmlNodeList childNodes = waveNode.ChildNodes;
        NormalEnemiesToSpawn = int.Parse(childNodes[0].InnerText);
        FastEnemiesToSpawn = int.Parse(childNodes[1].InnerText);
        StrongEnemiesToSpawn = int.Parse(childNodes[2].InnerText);
        SpawnSpeed = float.Parse(childNodes[3].InnerText);
        EnemiesRemaining = NormalEnemiesToSpawn + FastEnemiesToSpawn + StrongEnemiesToSpawn;

        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        // Generates ordered list of all enemies to spawn for the selected wave.
        List<GameObject> orderedEnemyObjects = new List<GameObject>();
        for (int i = 0; i < NormalEnemiesToSpawn; i++)
        {
            orderedEnemyObjects.Add(NormalEnemyPrefab);
        }
        for (int i = 0; i < FastEnemiesToSpawn; i++)
        {
            orderedEnemyObjects.Add(FastEnemyPrefab);
        }
        for (int i = 0; i < StrongEnemiesToSpawn; i++)
        {
            orderedEnemyObjects.Add(StrongEnemyPrefab);
        }

        EnemiesRemaining = orderedEnemyObjects.Count;

        // Creates a shuffled list, to make the wave seem more random.
        List<GameObject> shuffledEnemyObjects = Shuffle(orderedEnemyObjects);
        foreach (GameObject enemyObject in shuffledEnemyObjects)
        {
            if (Restarting)
                break;

            GameObject instantiatedGameObject = Instantiate(enemyObject, SpawnPoint.position, SpawnPoint.localRotation);

            if (WaveNumber > 10)
            {
                int originalEnemyHealth = instantiatedGameObject.GetComponent<Enemy>().Health;
                // Gets percentage based on wave number.  Then square percentage and add to total health.
                //Example, Wave 11, Enemy Health 100
                // Wave based percentage is 1.1%, which is 1.1.
                // Square this value, to create exponential growth as rounds continue.
                // Divide by 250 to get correct percentage
                double percentageBasedOnWave = WaveNumber / 250.0;
                double baseHealthIncrease = originalEnemyHealth * percentageBasedOnWave;
                instantiatedGameObject.GetComponent<Enemy>().Health += Convert.ToInt32(Math.Pow(baseHealthIncrease, 3));
            }

            yield return new WaitForSeconds(SpawnSpeed);
        }
    }

    private static System.Random rand = new System.Random();
    public static List<GameObject> Shuffle(List<GameObject> values)
    {
        for (int i = values.Count - 1; i > 0; i--)
        {
            GameObject tempObject;
            int k = rand.Next(i + 1);
            tempObject = values[k];
            values[k] = values[i];
            values[i] = tempObject;
        }
        return values;
    }

    public void UpdateEnemiesRemaining()
    {
        EnemiesRemaining--;

        // When all enemies are destroyed, wait 10 seconds before loading next wave.
        if (EnemiesRemaining <= 0)
        {
            if (WaveNumber == 10)
                ShowWave11Screen();
            else
                StartCoroutine(WaitBeforeLoadingWave());
        }
    }

    private IEnumerator WaitBeforeLoadingWave()
    {
        //yield return new WaitForSeconds(10);
        NextWaveCountdownText.gameObject.SetActive(true);
        for (int i = 10; i >= 0; i--)
        {
            NextWaveCountdownText.text = $"Next wave in {i}";
            yield return new WaitForSeconds(1);
        }
        Restarting = false;
        NextWaveCountdownText.gameObject.SetActive(false);

        WaveNumber++;
        LoadWave();
    }

    public void Restart()
    {
        Restarting = true;
        WaveNumber = 0;
        var aliveEnemies = FindObjectsOfType<Enemy>();
        EnemiesRemaining = aliveEnemies.Length;
        foreach (var enemy in aliveEnemies)
            enemy.DestroyEnemy();
    }

    public void ContinueButtonPressed()
    {
        Time.timeScale = 1;
        Wave11Screen.SetActive(false);
        StartCoroutine(WaitBeforeLoadingWave());
    }

    private void ShowWave11Screen()
    {
        Time.timeScale = 0;
        Wave11Screen.SetActive(true);
    }
}

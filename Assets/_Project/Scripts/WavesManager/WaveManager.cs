using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject NormalEnemyPrefab;
    public GameObject FastEnemyPrefab;
    public GameObject StrongEnemyPrefab;
    public Transform SpawnPoint;
    private int WaveNumber { get; set; }
    private int NormalEnemiesToSpawn { get; set; }
    private int FastEnemiesToSpawn { get; set; }
    private int StrongEnemiesToSpawn { get; set; }
    private float SpawnSpeed { get; set; }
    public int EnemiesRemaining { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WaveNumber = 4;
        LoadWave();
    }

    private void LoadWave()
    {
        XmlDocument xmlDoc = new XmlDocument();

        Debug.Log("Loading XML doc.");
        xmlDoc.Load(@"Assets\_Project\Scripts\WavesManager\Waves.xml");

        XmlNodeList waveNodes = xmlDoc.GetElementsByTagName("wave");
        XmlNode waveNode = waveNodes[WaveNumber - 1];
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

        // Creates a shuffled list, to make the wave seem more random.
        List<GameObject> shuffledEnemyObjects = Shuffle(orderedEnemyObjects);
        foreach (GameObject enemyObject in shuffledEnemyObjects)
        {
            Instantiate(enemyObject, SpawnPoint.position, SpawnPoint.localRotation);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

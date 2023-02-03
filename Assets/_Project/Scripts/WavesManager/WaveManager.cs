using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int WaveNumber { get; set; }
    private int NormalEnemiesToSpawn { get; set; }
    private int FastEnemiesToSpawn { get; set; }
    private int StrongEnemiesToSpawn { get; set; }
    private int EnemiesRemaining { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WaveNumber = 1;
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

        Debug.Log(NormalEnemiesToSpawn);
        Debug.Log(FastEnemiesToSpawn);
        Debug.Log(StrongEnemiesToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

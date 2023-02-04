using System.Linq;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public int Currency { get; set; } = 0;
    public int ExperienceLevel { get; set; } = 1;
    public int Health { get; set; }

    private GameObject GameOverPanel;

    public WaveManager WaveManager;
    private int MaxHealth { get; } = 2;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        GameOverPanel = GameObject.Find("Canvas").GetComponentsInChildren<RectTransform>(true).First(x => x.name == "Game over panel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int receivingDamage)
    {
        if (Health > 0 && receivingDamage > 0)
        {
            Health -= receivingDamage;
            UpdateBaseColour();
            if (Health <= 0)
            {
                Health = 0; //in case health is 1 and takes 2 damage, won't display minus health
                PlayerDead();
            }
        }
    }

    private void UpdateBaseColour()
    {
        float healthPercentage = ((float)Health / (float)MaxHealth);
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(1, healthPercentage, healthPercentage);
    }

    public void PlayerDead()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        var waveText = GameOverPanel.GetComponentsInChildren<TextMeshProUGUI>()
            .First(x => x.gameObject.name == "Wave count text");
        waveText.text = $"You survived {WaveManager.WaveNumber - 1} rounds";

        var camController = GameObject.Find("VirtualCamera").GetComponent<CameraController>();
        camController.allowDragging = false;
    }

    public void GainExperienceLevel(int receivedExperienceLevel)
    {
        ExperienceLevel += receivedExperienceLevel;
    }


    public void GainCurrency(int receivedCurrency)
    {
        Currency += receivedCurrency;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.AttackDamage);
            enemy.DestroyEnemy();
        }
    }

    public void RestartButtonPressed()
    {
        Debug.Log("Restarting...");
        Health = MaxHealth;
        Currency = 0;
        ExperienceLevel = 1;

        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        var camController = GameObject.Find("VirtualCamera").GetComponent<CameraController>();
        camController.allowDragging = true;

        WaveManager.Restart();
    }

    public void MainMenuButtonPressed()
    {
        Debug.Log("Loading main menu scene...");
    }

}

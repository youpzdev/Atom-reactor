using UnityEngine;
using UnityEngine.UI;

public class Game
{
    private readonly ResourceButton[] resources;
    private readonly SurvivalTimer timer;
    private readonly TweenPanel lossPanel;
    private readonly Text lossReason;
    private bool running;

    public Game(ResourceButton[] resources, SurvivalTimer timer, TweenPanel lossPanel, Text lossReason)
    {
        this.resources = resources;
        this.timer = timer;
        this.lossPanel = lossPanel;
        this.lossReason = lossReason;
    }

    public void Start()
    {
        running = true;
        lossPanel.gameObject.SetActive(false);
        timer.Begin();
        foreach (var resource in resources) resource.Initialize(this);
    }

    public void Lose(ResourceType type)
    {
        if (!running) return;

        running = false;
        timer.Stop();
        foreach (var resource in resources) resource.Stop();
        lossReason.text = $"Распад ресурса: {type.DisplayName()}";
        lossPanel.gameObject.SetActive(true);
        lossPanel.Show();
    }
}



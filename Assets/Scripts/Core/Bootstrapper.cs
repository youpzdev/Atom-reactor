using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private ResourceIcons icons;
    [SerializeField] private ResourceTimes times;
    [SerializeField] private ResourceButton[] resources;
    [SerializeField] private SurvivalTimer timer;
    [SerializeField] private TweenPanel lossPanel;
    [SerializeField] private Text lossReason;

    private Game game;

    private void Start()
    {
        IconService.Instance.Initialize(icons);
        ResourceService.Instance.Initialize(times);
        game = new Game(resources, timer, lossPanel, lossReason);
        game.Start();
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }
}


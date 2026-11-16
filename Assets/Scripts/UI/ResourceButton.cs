using UnityEngine;
using UnityEngine.UI;

public class ResourceButton : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Image progress;
    [SerializeField] private Text state;
    [SerializeField] private ResourceEffects effects;

    private Game game;
    private ResourceTime times;
    private TimerHandle timer;
    private float duration;
    private Color resourceColor;

    public void Initialize(Game owner)
    {
        game = owner;
        resourceColor = progress.color;
        times = ResourceService.Instance.Get(type);
        button.onClick.AddListener(Enrich);
        Ready();
    }

    private void Ready()
    {
        if (timer != null) effects.Ready(resourceColor);
        button.interactable = true;
        state.text = "До распада";
        progress.color = resourceColor;
        IconService.Instance.ShowReady(type, icon);
        duration = times.Decay;
        timer = ReactorTimers.Instance.Start(duration, Decay, this);
        Update();
    }

    private void Enrich()
    {
        if (!button.interactable) return;
        if (timer.Remaining <= 0)
        {
            Decay();
            return;
        }

        timer.Cancel();
        effects.Enrich();
        button.interactable = false;
        state.text = "Обогащение";
        progress.color = new Color32(112, 156, 169, 255);
        IconService.Instance.ShowEnriching(type, icon);
        duration = times.Enrichment;
        timer = ReactorTimers.Instance.Start(duration, Ready, this);
        Update();
    }

    private void Decay()
    {
        effects.Decay();
        SetProgress(0);
        state.text = "Распался";
        game.Lose(type);
    }

    public void Stop()
    {
        timer.Cancel();
        button.interactable = false;
    }

    private void Update()
    {
        if (timer == null || !timer.IsActive) return;

        float remaining = Mathf.Clamp01(timer.Remaining / duration);
        SetProgress(remaining);

        if (button.interactable)
        {
            float urgency = Mathf.InverseLerp(0.3f, 0.05f, remaining);
            progress.color = Color.Lerp(resourceColor, new Color32(250, 139, 115, 255), urgency);
        }
    }

    private void SetProgress(float fraction)
    {
        progress.rectTransform.anchorMax = new Vector2(fraction, 1);
        progress.enabled = fraction > 0;
    }
}


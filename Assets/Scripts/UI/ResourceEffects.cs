using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class ResourceEffects : Graphic
{
    [SerializeField] private RectTransform icon;
    [SerializeField] private TweenPreset pulsePreset;

    private Tween pulse;
    private Tween burst;

    private float age = 1;
    private int count;
    private bool inward;
    private bool falling;

    public void Enrich() => Play(new Color32(129, 217, 236, 255), 10, 0.34f, true, false);
    public void Ready(Color resourceColor) => Play(resourceColor, 8, 0.36f, false, false);
    public void Decay() => Play(new Color32(255, 150, 98, 255), 22, 0.7f, false, true);

    private void Play(Color tint, int particles, float duration, bool collect, bool drop)
    {
        pulse?.Kill();
        icon.localScale = Vector3.one;
        pulse = GoofyTween.Play(icon, pulsePreset);

        burst?.Kill();
        color = tint;
        count = particles;
        inward = collect;
        falling = drop;
        age = 0;
        SetVerticesDirty();
        burst = DOTween.To(() => age, Draw, 1, duration).SetEase(Ease.Linear).SetLink(gameObject);
    }

    private void Draw(float time)
    {
        age = time;
        SetVerticesDirty();
    }

    protected override void OnDisable()
    {
        pulse?.Kill();
        burst?.Kill();
        if (icon != null) icon.localScale = Vector3.one;
        age = 1;
        base.OnDisable();
    }

    protected override void OnPopulateMesh(VertexHelper mesh)
    {
        mesh.Clear();
        if (age >= 1) return;

        for (int i = 0; i < count; i++)
        {
            float variation = Mathf.Repeat(i * 0.618034f, 1);
            // Золотой угол, иначе искры ложатся ровными спицами
            float angle = i * 2.399963f;
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float travel = age * (2 - age);
            float reach = Mathf.Lerp(29, falling ? 105 : 62, variation);
            float distance = inward ? Mathf.Lerp(reach + 28, 12, travel) : 17 + reach * travel;
            Vector2 center = direction * distance;
            if (falling) center.y -= 65 * age * age;

            float fade = Mathf.Min(1, age * 14) * (1 - age);
            Color tint = Color.Lerp(color, Color.white, 0.28f * variation);
            tint.a = fade;
            Vector2 along = direction * Mathf.Lerp(3, falling ? 11 : 7, variation) * (1 - 0.45f * age);
            Vector2 across = new Vector2(-direction.y, direction.x) * Mathf.Lerp(0.9f, 2.1f, variation);
            AddSpark(mesh, center, along, across, tint);
        }
    }

    private void AddSpark(VertexHelper mesh, Vector2 center, Vector2 along, Vector2 across, Color32 tint)
    {
        int first = mesh.currentVertCount;
        mesh.AddVert(center - along, tint, Vector2.zero);
        mesh.AddVert(center + across, tint, Vector2.zero);
        mesh.AddVert(center + along, tint, Vector2.zero);
        mesh.AddVert(center - across, tint, Vector2.zero);
        mesh.AddTriangle(first, first + 1, first + 2);
        mesh.AddTriangle(first, first + 2, first + 3);
    }
}

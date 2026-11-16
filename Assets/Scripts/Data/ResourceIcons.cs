using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactor/Resource Icons")]
public class ResourceIcons : ScriptableObject
{
    [SerializeField] private ResourceIcon[] resources;

    public ResourceIcon Get(ResourceType type)
    {
        foreach (var resource in resources)
            if (resource.Type == type) return resource;

        throw new InvalidOperationException($"В {name} нет спрайтов для {type}");
    }
}

[Serializable]
public class ResourceIcon
{
    [SerializeField] private ResourceType type;
    [SerializeField] private Sprite enriching;
    [SerializeField] private Sprite ready;

    public ResourceType Type { get { return type; } }
    public Sprite Enriching { get { return enriching; } }
    public Sprite Ready { get { return ready; } }
}

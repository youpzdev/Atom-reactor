using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Reactor/Resource Times")]
public class ResourceTimes : ScriptableObject
{
    [SerializeField] private ResourceTime[] resources;

    public ResourceTime Get(ResourceType type)
    {
        foreach (var resource in resources)
            if (resource.Type == type) return resource;

        throw new InvalidOperationException($"В {name} нет времени для {type}");
    }
}

[Serializable]
public class ResourceTime
{
    [SerializeField] private ResourceType type;
    [SerializeField] [Min(0.1f)] private float enrichment;
    [SerializeField] [Min(0.1f)] private float decay;

    public ResourceType Type { get { return type; } }
    public float Enrichment { get { return enrichment; } }
    public float Decay { get { return decay; } }
}

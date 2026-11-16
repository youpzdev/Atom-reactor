public enum ResourceType
{
    Uranium,
    Plutonium,
    Thorium,
    Neptunium
}

public static class ResourceNames
{
    public static string DisplayName(this ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Uranium: return "Уран";
            case ResourceType.Plutonium: return "Плутоний";
            case ResourceType.Thorium: return "Торий";
            case ResourceType.Neptunium: return "Нептуний";
            default: throw new System.ArgumentOutOfRangeException(nameof(type), type, "Неизвестный ресурс");
        }
    }
}

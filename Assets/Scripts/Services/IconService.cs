using UnityEngine.UI;

public class IconService
{
    private static IconService instance;
    private ResourceIcons icons;

    public static IconService Instance
    {
        get
        {
            if (instance == null) instance = new IconService();

            return instance;
        }
    }

    private IconService() {}

    public void Initialize(ResourceIcons data) => icons = data;
    public void ShowReady(ResourceType type, Image image) => image.sprite = icons.Get(type).Ready;
    public void ShowEnriching(ResourceType type, Image image) => image.sprite = icons.Get(type).Enriching;
}

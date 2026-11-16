public class ResourceService
{
    private static ResourceService instance;
    private ResourceTimes times;

    public static ResourceService Instance
    {
        get
        {
            if (instance == null) instance = new ResourceService();

            return instance;
        }
    }

    private ResourceService() {}

    public void Initialize(ResourceTimes data) => times = data;
    public ResourceTime Get(ResourceType type) => times.Get(type);
}

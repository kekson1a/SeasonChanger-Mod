using SeasonChanger.UI;

namespace SeasonChanger
{
    [ConfigureSingleton(SingletonFlags.PersistAutoInstance | SingletonFlags.DestroyDuplicates)]
    public class ModComponent : MonoSingleton<DateMenu>
    {
    }
}

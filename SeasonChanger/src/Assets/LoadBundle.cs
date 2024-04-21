using System.Collections;
using System.Reflection;

using UnityEngine;

namespace SeasonChanger.Assets
{
    [ConfigureSingleton(SingletonFlags.PersistAutoInstance | SingletonFlags.DestroyDuplicates)]
    public class LoadBundle : MonoSingleton<LoadBundle>
    {
        private AssetBundle _assetBundle;

        public delegate void BundleLoaded(AssetBundle bundle);
        public event BundleLoaded OnBundleLoaded;

        private void Start()
        {
            StartCoroutine(GetBundleAsync());
        }

        private IEnumerator GetBundleAsync()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SeasonChanger.resources.season_menu");
            var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(stream);

            yield return bundleLoadRequest;

            var loadedBundle = bundleLoadRequest.assetBundle;

            if (loadedBundle != null) _assetBundle = loadedBundle;
            else 
            {
                Plugin.Instance.PrintError("Failed to load the asset bundle!");
                yield break;
            }
            var prefabRequest = loadedBundle.LoadAssetAsync("SeasonMenu");
            yield return prefabRequest;

            OnBundleLoaded?.Invoke(_assetBundle);
        }

    }
}

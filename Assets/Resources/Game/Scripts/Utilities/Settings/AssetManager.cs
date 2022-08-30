using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;
using System.Threading.Tasks;
[System.Serializable]
public class Asset
{

    #region Fields

    // Unity Inspector.
    [HideInInspector] public bool hideSection;

    // Asset Info.
    public string name;
    public GameObject prefab;

    // Size and randomness controller.
    public Vector2 scaleX = new Vector2(0.8f, 1.2f);
    public Vector2 scaleY = new Vector2(0.8f, 1.2f);

    // Life span once instantiated.
    public bool infiniteLife;
    public float lifeDuration = 2.0f;

    // Pooling variables.
    public Transform parent;
    public Dictionary<GameObject, bool> objectPool;

    #endregion




    /// <summary>
    /// Creates a new asset.
    /// </summary>

    public Asset() => name = "New Asset";

    /// <summary>
    /// Create a copy of an existing asset.
    /// </summary>

    public Asset(Asset copy)
    {
        name = copy.name;
        prefab = copy.prefab;
        scaleX = copy.scaleX;
        scaleY = copy.scaleY;
        infiniteLife = copy.infiniteLife;
        lifeDuration = copy.lifeDuration;
    }


}





public class AssetManager : MonoBehaviourPunCallbacks
{
    #region Fields

    // Asset array allows to choose how many assets we want to display,
    // also disable warning saying it is unused.
#pragma warning disable 0649
    [SerializeField] public List<Asset> assets = new List<Asset>();
#pragma warning restore 0649
    #endregion
    public static AssetManager Instance;
    private void Awake() 
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #region Functions
    public Asset GetAsset(string assetName)
    {
        if (assetName == "None") return null;

        foreach (var asset in assets.Where(asset => asset.name == assetName))
            return asset;

        Debug.LogWarning("Asset Manager: Asset not found in list. (" + assetName + ")");
        return null;
    }

    public GameObject SpawnObject(string assetName, Vector3 position, Quaternion rotation)
    {
        // Cari asset
        if (assetName == "None") return null;

        var asset = GetAsset(assetName);

        if (asset == null)
        {
            Debug.LogWarning("Asset Manager: Asset not found in list. (" + assetName + ")");
            return null;
        }

        // Instansiasi asset
        var objectToUse = Instantiate(asset.prefab, position, rotation);

        // Penjadwalan menghancurkan objek
        RunDestroyTimer(objectToUse, asset.lifeDuration);

        return objectToUse;
    }
    public async void RunDestroyTimer(GameObject element, float lifeDuration)
    {
        float time = 0f;
        while (time < lifeDuration)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        Destroy(element);
    }

    #endregion

}
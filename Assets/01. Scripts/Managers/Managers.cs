using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    TileManager _tile = new TileManager();
    BlockManager _block = new BlockManager();
    // DataManager _data = new DataManager();
    // SceneManagerEx _scene = new SceneManagerEx();
    // SoundManager _sound = new SoundManager();
    // UIManager _ui = new UIManager();

    GameManager _game;

    public static PoolManager Pool => Instance._pool;
    public static ResourceManager Resource => Instance._resource;
    public static TileManager Tile => Instance._tile;
    public static BlockManager Block => Instance._block;
    // public static DataManager Data => Instance._data;
    // public static SceneManagerEx Scene => Instance._scene;
    // public static SoundManager Sound => Instance._sound;
    // public static UIManager UI => Instance._ui;


    public static GameManager Game => Instance._game;



    ///<summary>가장 처음 매니저 만들때 한번 Init</summary>
    static void Init()
    {
        if (_instance == null)
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            UnityEngine.Input.multiTouchEnabled = false;

            GameObject go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            _instance._pool.Init();
            _instance._resource.Init();
            _instance._tile.Init();
            _instance._block.Init();

            _instance._game = go.AddComponent<GameManager>();
        }
    }

    public static void GameInit()
    {
        Instance._game.Init();
    }

    ///<summary>새로운 씬으로 갈때마다 클리어</summary>
    public static void Clear()
    {
        Pool.Clear();
        Resource.Clear();
    }
}

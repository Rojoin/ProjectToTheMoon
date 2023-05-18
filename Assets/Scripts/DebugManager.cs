using UnityEngine;

/// <summary>
/// Class for the DebugManager
/// </summary>
public class DebugManager : MonoBehaviour
{
    PlayerMovement player;
    public static DebugManager _instance;
    public static DebugManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    [SerializeField] bool isPlayerActive;
    [SerializeField] bool isCameraActive;
    [SerializeField] bool isDebugActive;
    [SerializeField] bool isLogActive;
    [SerializeField] bool isLogWarningActive;
    [SerializeField] bool isLogErrorActive;
    [SerializeField] bool isRayActive;
    [SerializeField] string[] activeTags;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// LogWarning method controller
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="text"></param>
    public void LogWarning(string tag, string text)
    {
        if (!isDebugActive) return;
        if (!isLogWarningActive) return;
        switch (tag)
        {
            case "Player":
                if (isPlayerActive)
                {
                    Debug.LogWarning(text);
                }
                break;
            case "MainCamera":
                if (isCameraActive)
                {
                    Debug.LogWarning(text);
                }
                break;
            default:
                Debug.LogError("Invalid Tag");
                break;
        }
    }
    /// <summary>
    /// LogError method controller
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="text"></param>
    public void LogError(string tag, string text)
    {

        if (!isDebugActive) return;
        if (!isLogErrorActive) return;
        switch (tag)
        {
            case "Player":
                if (isPlayerActive)
                {
                    Debug.LogError(text);
                }
                break;
            case "MainCamera":
                if (isCameraActive)
                {
                    Debug.LogError(text);
                }
                break;
            default:
                Debug.LogError("Invalid Tag");
                break;
        }
    }
    /// <summary>
    /// Log method controller
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="text"></param>
    public void Log(string tag, string text)
    {
        if (!isDebugActive) return;
        if (!isLogActive) return;     
        switch (tag)
        {
            case "Player":
                if (isPlayerActive)
                {
                    Debug.Log(text);
                }
                break;
            case "MainCamera":
                if (isCameraActive)
                {
                    Debug.Log(text);
                }
                break;
            default:
                Debug.LogError("Invalid Tag");
                break;
        }
    }

}

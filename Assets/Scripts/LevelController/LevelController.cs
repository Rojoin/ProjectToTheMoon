using Cinemachine;
using UnityEngine;

/// <summary>
/// Class for the LevelController
/// </summary>
public class LevelController : MonoBehaviour
{
    [SerializeField] private AudioClip inGameMusic;
    /// <summary>
    /// Enum for the level state
    /// </summary>
    public enum LevelState
    {
        playing, 
        failed, 
        Complete
    }
    public static LevelState levelStatus
    {
        get => LevelStatus;
        private set => LevelStatus = value;
    }

    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyBaseStats enemy;
    [SerializeField] private CinemachineDollyCart levelDolly;
    [SerializeField] private CinemachinePathBase path;
    [SerializeField] private SlideMenu end;
    [SerializeField] private SlideMenu gameOver;
    private static LevelState LevelStatus;

    private void Awake()
    {
        levelStatus = LevelState.playing;
        levelDolly = GetComponent<CinemachineDollyCart>();
        player = GetComponentInChildren<PlayerController>();
   
    }
    private void Start()
    {
        SoundManager.Instance.GetMusicSource().Stop();
        var soundManager = SoundManager.Instance.GetMusicSource();
        soundManager.clip = inGameMusic;
        soundManager.Play();
    }
 
    void Update()
    {
        LevelCompletionLogic();
    }
    /// <summary>
    /// Checks if the Level should end because of loseTime or condition
    /// </summary>
    private void LevelCompletionLogic()
    {
        if (!player.IsAlive)
        {
            LevelController.levelStatus = LevelController.LevelState.failed;
            levelDolly.m_Speed = 0;
            gameOver.OpenSlide();
        }
        else if (enemy && !enemy.IsAlive())
        {
            LevelController.levelStatus = LevelController.LevelState.Complete;
            end.OpenSlide();
        }

        if (!(levelDolly.m_Position >= path.MaxPos &&
              LevelController.levelStatus == LevelController.LevelState.playing)) return;
        LevelController.levelStatus = LevelController.LevelState.Complete;
        end.OpenSlide();
    }
}
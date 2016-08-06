using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Player Player { get; private set; }
    public CameraController Camera { get; private set; }
    public TimeSpan RunningTime { get { return DateTime.UtcNow - _started; } }
    public int CurrentTimeBounce
    {
        get
        {
            var secondDifference = (int)(BonusCutoffSeconds - RunningTime.TotalSeconds);
            return Mathf.Max(0, secondDifference) * BonusSecondMultipler;
        }
    }

    private List<CheckPoint> _checkpoints;
    private int _currentCheckpointIndex;
    private DateTime _started;
    private int _savedPoints;

    public CheckPoint DebugSpawn;
    public int BonusCutoffSeconds;
    public int BonusSecondMultipler;


    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        _checkpoints = FindObjectsOfType<CheckPoint>().OrderBy(t => t.transform.position.x).ToList();
        _currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

        Player = FindObjectOfType<Player>();
        Camera = FindObjectOfType<CameraController>();

        _started = DateTime.UtcNow;

        var listners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();
        foreach (var listener in listners)
        {
            //loop in backwards to ensure positive distance and assign the points to the first check point
            for (var i = _checkpoints.Count - 1; i >= 0; i--)
            {
                var distance = ((MonoBehaviour)listener).transform.position.x - _checkpoints[i].transform.position.x;
                if (distance < 0)
                {
                    continue;
                }
                _checkpoints[i].AssignObjectToCheckpoint(listener);
                break;
            }
        }

#if UNITY_EDITOR
        if (DebugSpawn != null)
            DebugSpawn.SpawnPlayer(Player);
        
        else if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#else
        if (_currentCheckpointIndex !=-1)
	{
         _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
	}
#endif

    }
    public void Update()
    {
        var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
        if (isAtLastCheckpoint)
        {
            return;
        }
        var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;
        if (distanceToNextCheckpoint >=0)
        {
            return;
        }
        _checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();
        _currentCheckpointIndex++;
        _checkpoints[_currentCheckpointIndex].PlayerHitCheckPoint();

        GameManager.Instance.AddPoints(CurrentTimeBounce);
        _savedPoints = GameManager.Instance.Points;
        _started = DateTime.UtcNow;

        //loop through all object IplayerRespawnListener
        //then loop all check points backwards if  distance is positive then check point must assign to check points

       

    }
    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());   
    }
    private IEnumerator KillPlayerCo()
    {
        Player.Kill();
        Camera.IsFollowing = false;
        yield return new WaitForSeconds(2f);


        Camera.IsFollowing = true;
        if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
        //points
        _started = DateTime.UtcNow;
        GameManager.Instance.ResetPoints(_savedPoints);
       
    }
}


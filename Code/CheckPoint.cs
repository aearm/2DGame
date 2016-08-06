using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    private List<IPlayerRespawnListener> _listners;
    public void Awake()
    {
        _listners = new List<IPlayerRespawnListener>();


    }
    public void Start()
    {
    }
    public void PlayerHitCheckPoint()
    {

    }
    private IEnumerator PlayerHitCheckpointCo(int bouns)
    {
        yield break;
    }
    public void PlayerLeftCheckpoint()
    {

    }
    public void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform);
        foreach(var listener in _listners)
        {
            listener.OnPlayerRespawnInThisCheckpoint(this, player);
        }

    }
    public void AssignObjectToCheckpoint(IPlayerRespawnListener listener)
    {
        _listners.Add(listener);
    }
}


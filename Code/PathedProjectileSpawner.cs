﻿using UnityEngine;

public class PathedProjectileSpawner :MonoBehaviour
{
    public Transform Destination;
    public PathedProjectile Projectile;

    public GameObject SpawnEffect;
    public float Speed;
    public float FireRate;


    private float _nextShotInSeconds;
    
    public void Start()
    {
        _nextShotInSeconds = FireRate;

    }
    public void Update()
    {
        if ((_nextShotInSeconds -=Time.deltaTime )>0)
             return;

        _nextShotInSeconds = FireRate;
        var projecttile = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);
        projecttile.Initalize(Destination, Speed);

        if (SpawnEffect!=null)
        {
            Instantiate(SpawnEffect, transform.position, transform.rotation);
        }
                
    }

    public void onDrawGizmos()
    {
        if (Destination==null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position);

    }
}
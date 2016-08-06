﻿using UnityEngine;
public class SimpleProjectile : Projectile
{
    public int Damage;
    public GameObject DestroyedEffect;
    public int PointsToGiveToPlayer;
    public float TimeToLive;

    public void Update()
    {
        //every update move the projectile in the direction plus initial speed then scale it to speed and time left
        if ((TimeToLive -= Time.deltaTime) <= 0)
        {
            DestroyProjectile();
            return;
        }
        //transform.Translate((Direction + new Vector2(InitialVelocity.x, 0)) * Speed * Time.deltaTime, Space.World);
        transform.Translate(Direction * (Mathf.Abs(InitialVelocity.x + Speed) * Time.deltaTime), Space.World);
        Debug.Log(Direction);
       

    }
    public void TakeDamage(int damage, GameObject instigator)
    {
        if (PointsToGiveToPlayer != 0)
        {
            //check if the instigator of the projectile owned by the player 
            var porjectile = instigator.GetComponent<Projectile>();

            if (porjectile != null && porjectile.Owner.GetComponent<Player>()!=null)
            {
                GameManager.Instance.AddPoints(PointsToGiveToPlayer);
                FloatingText.Show(string.Format("+{0}!", PointsToGiveToPlayer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));

            }
        }
        DestroyProjectile();
    }
    protected override void OnCollideOther(Collider2D other)
    {
       
        DestroyProjectile();
    }
    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        takeDamage.TakeDamage(Damage, gameObject);
        DestroyProjectile();

    }
    private void DestroyProjectile()
    {
        if (DestroyedEffect!=null)
        {
            Instantiate(DestroyedEffect, transform.position, transform.rotation);
           

        }
        Destroy(gameObject);
    }
}
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed = 8;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelereationInAir = 5f;
    public int MaxHealth = 100;
    //game object will instanciate when player loss health
    public GameObject OuchEffect;

    public Projectile Projectile;
    public float FireRate;
    public Transform ProjetileFireLocation;
    public AudioClip PlayerHitSound;
    public AudioClip PlayerShootSound;

    

    private float _canFireIn;

    public int Health { get; private set; }

    public bool IsDead { get; private set; }

    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;
        Health = MaxHealth;
    }
    public void Update()
    {
        _canFireIn -= Time.deltaTime;


        if(!IsDead)
            HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelereationInAir;
        if (IsDead)
        {
            _controller.SetHorizontalForce(0);
        }
        else
        {
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
        }
        

    }
    public void Stopplayer()
    {
       
        collider2D.enabled = false;
        IsDead = true;


    }
    public void Startplayer()
    {
        IsDead = false;
        _controller.HandleCollisions = true;
        collider2D.enabled = true;

    }
    public void Kill()
    {
        
        _controller.HandleCollisions = false;
        collider2D.enabled = false;
        IsDead = true;
        //to make health bar display zero
        Health = 0;

        _controller.SetForce(new Vector2(0, 20));
    }
    public void RespawnAt(Transform spawnPoint)
    {
        if (!_isFacingRight)
        {
            Flip();
        }
        IsDead = false;
        collider2D.enabled = true;
        _controller.HandleCollisions = true;
        Health = MaxHealth;
        transform.position = spawnPoint.position;
    }
    public void TakeDamage(int damage)
    {
        AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);

        FloatingText.Show(string.Format("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 10f));

        Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;
        if (Health <= 0)
        {
            LevelManager.Instance.KillPlayer();
        }
    }
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
            {
                Flip();

            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();

        }
        else
        {
            _normalizedHorizontalSpeed = 0;

        }
        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))

            {
                _controller.Jump();
            }

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();

        }
    }
    private void FireProjectile()
    {
        //determine if we can fire 
        //give the direction for the projectile
        //instansiate the projectile 

        if (_canFireIn >0)
        {
            return;
        }
        var direction = _isFacingRight ? Vector2.right : -Vector2.right;
        var projectile = (Projectile)Instantiate(Projectile, ProjetileFireLocation.position, ProjetileFireLocation.rotation);
        projectile.Initialize(gameObject, direction, _controller.Velocity);

        

        _canFireIn = FireRate;
        AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);


    }
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;

    }
}

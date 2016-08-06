
using UnityEngine;
public class SimpleEnemyAi:MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{   //implement the Itakedamage to responce when hit player
    //implement the Iplayerrespan to responce when the player reinstantiate

    //some properties for positon , speed ,etc 

    public float Speed;
    public float FireRate = 1;
    public Projectile Projectile;
    public GameObject DestroyedEffect;

    private CharacterController2D _controller;
    private Vector2 _direction;
    private Vector2 _startPosition;
    private float _canFireIn;


    public void Start()
    {
        //alice the countroller 
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
        _startPosition = transform.position;


    }
   public void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);
        //reverse the direction while you moving 
        if ((_direction.x < 0 && _controller.State.IsCollidingLeff)|| (_direction.x >0 && _controller.State.IsCollidingRight))
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        }
        //check if he can fire

        if ((_canFireIn -= Time.deltaTime) > 0)
        {
            return;
        }
        // raycast to indicate if player can hit by this object

        var raycast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));
        if (!raycast)
        {
            return;
        }
        var porjectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
        
        porjectile.Initialize(gameObject, _direction, _controller.Velocity);
        _canFireIn = FireRate;


    }
    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        _direction = new Vector2(-1, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = _startPosition;
        gameObject.SetActive(true);
    }

   public void TakeDamage(int damage, GameObject instigator)
    {
        Instantiate(DestroyedEffect);
        gameObject.SetActive(false);

    }
}
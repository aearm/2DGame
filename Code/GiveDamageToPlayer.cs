using UnityEngine;


public class GiveDamageToPlayer : MonoBehaviour {
    public int DamageToGive = 10;

    //track the velocity of the object cuaused damaged in order to knock the player 
    private Vector2
            _lastPosition,
            _velocity;
    public void LateUpdate()
    {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player ==null)
        {
            return;
        }
        player.TakeDamage(DamageToGive);
        //grab the controller from the player ;
        //character controller has the player velocity
        var controller = player.GetComponent<CharacterController2D>();
        var totalVelocity = controller.Velocity + _velocity;
        //formula allow us to perform the knock back in every direction;

        controller.SetForce(new Vector2(-1 * Mathf.Sign(totalVelocity.x) * Mathf.Clamp(Mathf.Abs(totalVelocity.x) * 6, 10, 40),
                                       -1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * 6, 5, 30)));
    }
	
}

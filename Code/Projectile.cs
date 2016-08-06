using UnityEngine;

//this is the base class for Projectile 
public abstract class Projectile : MonoBehaviour
{
    public float Speed;
    public LayerMask CollisonMask;
    public GameObject Owner { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 InitialVelocity { get; private set; }

    public void Initialize(GameObject owner , Vector2 direction, Vector2 initialVelocity)
    {
        transform.right= direction;
        Owner = owner;
        Debug.Log(owner.ToString());
        Direction = direction;
        Debug.Log(Direction.ToString());
        InitialVelocity = initialVelocity;
        OnInitialized();

    }
    //this is optional method which can called by child only if they want 
    protected virtual void OnInitialized()
    {

    }

    //check all the possible state for projectile 
    //if hit with owner or with take damage object 
    //or with none of this type object
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //binary representation for layers 
        //layer 0 = 0000 0001 =1 in decimal 
        //shift the layer number to get it in binary 
       
        if ((CollisonMask.value & (1 << other.gameObject.layer)) == 0)
        {
            
            OnNotCollisionWith(other);
            return;
        }
        var isOwner = other.gameObject == Owner;
        if (isOwner)
        {
          
            OnCollideOwner();
            return;
        }
        var takeDamage = (ITakeDamage)other.GetComponent(typeof(ITakeDamage));
        if (takeDamage !=null)
        {
            
            OnCollideTakeDamage(other, takeDamage);
            return;

        }
      
        OnCollideOther(other);
       
    }
    protected virtual void OnNotCollisionWith(Collider2D other)
    {

    }
    protected virtual void OnCollideOwner()
    {

    }
    protected virtual void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {

    }
    protected virtual void OnCollideOther(Collider2D other)
    {
        
    }
    
}
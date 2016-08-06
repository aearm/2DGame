using UnityEngine;
using System.Collections;

public class PointStar : MonoBehaviour ,IPlayerRespawnListener
{

    //the effect when start is collected;
    public GameObject Effect;
    public int PointsToAdd = 10;
    public AudioClip HisStarSound;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>()==null)
        {
            return;
        }
        if (HisStarSound != null)
            AudioSource.PlayClipAtPoint(HisStarSound, transform.position);


        GameManager.Instance.AddPoints(PointsToAdd);
        //set the effect position and rotaiton
        Instantiate(Effect, transform.position, transform.rotation);

        gameObject.SetActive(false);

        FloatingText.Show(string.Format("+{0}!", PointsToAdd), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
       


    }
    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        gameObject.SetActive(true);
    }
}

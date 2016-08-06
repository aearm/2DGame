using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public Player Player;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRender;
    //translate RGB to Unity Standard 
    public Color MaxHealthColor = new Color(255 / 255f, 63 / 255f, 63 / 255f);
    public Color MinHeahlthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);

	
	// Update is called once per frame
	public void Update () {
        var healthPercent = Player.Health / (float)Player.MaxHealth;
        //Debug.Log(healthPercent.ToString());
        ForegroundSprite.localScale = new Vector3(healthPercent, 1, 1);
        ForegroundRender.color = Color.Lerp(MaxHealthColor, MinHeahlthColor, healthPercent);

	
	}
}

using UnityEngine;
using System.Collections;
public class Tutorial:MonoBehaviour
{
    public string Message;
    public Player Player { get; private set; }
    public bool visisted = false;
    private int counter = 0;

    public void Start()
    {
        
        Player = FindObjectOfType<Player>();

       
   }

    public void OnTriggerEnter2D(Collider2D other)
    {

        counter++;
        Debug.Log(counter.ToString());


    }
   

   
   
    void OnGUI()
    {
       
       
        if (counter==1)
            ShowMessage();

    }
    public void ShowMessage()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        

        Player.Stopplayer();
        GUI.backgroundColor = Color.red;
        if (GUI.Button(new Rect((Screen.width / 2)-50, (Screen.height / 2), 100, 30), "OK"))
        {
            visisted = true;
            Player.Startplayer();
            counter++;
        }
        GUI.Box(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 4), "Tutorial\n\n" + Message);



    }
   
}
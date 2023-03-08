using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /*
    public struct Player
    {
        public string Name;
        public Color Color;

        public Player(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }
    }

    public Player player1 = new( "J1", Color.blue );
    public Player player2 = new( "J2", Color.red );
    */

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

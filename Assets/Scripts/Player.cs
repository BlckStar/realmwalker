using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxLeft = -50;
    public float MaxRight = 0;
    private bool LookingRight = true;

    public float _direction = 0;

    public float MovementSpeed = 4;

    public float Direction
    {
        get { return _direction; }
        set
        {
            if (LookingRight && value < 0)
            {
                LookingRight = false;
                this.transform.Rotate(Vector3.up, 180);
            }

            if (!LookingRight && value > 0)
            {
                LookingRight = true;
                this.transform.Rotate(Vector3.up, 180);
            }

            _direction = value;
        }
    }

    private Player otherPlayer;

    public void Start()
    {
        this.otherPlayer = FindObjectsByType<Player>(FindObjectsSortMode.None).Where((player => player.tag != this.tag))
            .First();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Transform right
        if (_direction != 0)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
            if (this.transform.position.x < MaxLeft)
            {
                this.transform.position = new Vector3(MaxLeft, this.transform.position.y, this.transform.position.z);
            }

          

            if (this.transform.position.x > MaxRight)
            {
                this.transform.position = new Vector3(MaxRight, this.transform.position.y, this.transform.position.z);
            }

            // player encounter
            // if (this.tag == "Player2" && this.transform.position.x < otherPlayer.transform.position.x)
            // {
            //     this.transform.position = new Vector3(otherPlayer.transform.position.x + 0.1f,
            //         this.transform.position.y,
            //         this.transform.position.z);
            // }
            // if (this.tag == "Player1" && this.transform.position.x > otherPlayer.transform.position.x)
            // {
            //     this.transform.position = new Vector3(otherPlayer.transform.position.x - 0.1f,
            //         this.transform.position.y, this.transform.position.z);
            // }
        }
    }
}
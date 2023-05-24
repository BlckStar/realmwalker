using UnityEngine;

public class Player : MonoBehaviour
{
    private bool LookingRight = true;

    private float _direction = 0;

    public float MovementSpeed = 4;
    public float Direction
    {
        get
        {
            return _direction;
        }
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


    // Update is called once per frame
    void Update()
    {
        // Move Transform right
        if (_direction != 0)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
        }

    }
}

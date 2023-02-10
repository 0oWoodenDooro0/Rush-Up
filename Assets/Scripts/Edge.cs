using UnityEngine;

public class Edge : MonoBehaviour
{
    public float firstSpeed;
    private bool _isStart;


    void Start()
    {
        _isStart = false;
    }


    void Update()
    {
        if (!_isStart)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _isStart = true;
            }
        }
        else if (Player.IsGameOver || Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, -15, 0);
            _isStart = false;
        }
        else
        {
            var speed = firstSpeed + (float)ScoreBoard.Score / 20;

            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, 0);
        }
    }
}
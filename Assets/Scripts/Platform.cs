using UnityEngine;

public class Platform : MonoBehaviour
{
    void Update()
    {
        var edge = GameObject.FindWithTag("Edge");
        if (edge && !Player.IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Destroy(gameObject);
            }
            else if (edge.transform.position.y - transform.position.y > 20)
            {
                Destroy(gameObject);
            }
        }
    }
}
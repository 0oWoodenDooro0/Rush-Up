using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject[] platforms;
    private float _maxHeight;


    private bool ComparePosition(float x1, float x2, int num)
    {
        return x1 + num > x2 - num;
    }

    private void CreateOnePlatform(float height)
    {
        var platformIndex = Random.Range(0, platforms.Length);
        var platformPosition = new Vector3(Random.Range(-16f, 16f), height + Random.Range(-1f, 1f), 0);
        Instantiate(platforms[platformIndex], platformPosition, new Quaternion(0, 0, 0, 0));
    }

    private void CreateTwoPlatform(float height)
    {
        var platformIndex1 = Random.Range(0, platforms.Length);
        var platformIndex2 = Random.Range(0, platforms.Length);
        var platform1PositionX = Random.Range(-16f, 13f);
        var platform2PositionX = Random.Range(-13f, 16f);
        while (ComparePosition(platform1PositionX, platform2PositionX, 3))
        {
            platform1PositionX = Random.Range(-16f, 13f);
            platform2PositionX = Random.Range(-13f, 16f);
        }

        Instantiate(platforms[platformIndex1], new Vector3(platform1PositionX, height + Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
        Instantiate(platforms[platformIndex2], new Vector3(platform2PositionX, height + Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
    }

    private void CreateThreePlatform(float height)
    {
        var platformIndex1 = Random.Range(0, platforms.Length);
        var platformIndex2 = Random.Range(0, platforms.Length);
        var platformIndex3 = Random.Range(0, platforms.Length);
        var platform1PositionX = Random.Range(-16f, 10f);
        var platform2PositionX = Random.Range(-13f, 13f);
        var platform3PositionX = Random.Range(-10f, 16f);
        while (ComparePosition(platform1PositionX, platform2PositionX, 3) || ComparePosition(platform1PositionX, platform3PositionX, 3) ||
               ComparePosition(platform2PositionX, platform3PositionX, 3))
        {
            platform1PositionX = Random.Range(-16f, 10f);
            platform2PositionX = Random.Range(-13f, 13f);
            platform3PositionX = Random.Range(-10f, 16f);
        }

        Instantiate(platforms[platformIndex1], new Vector3(platform1PositionX, height + Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
        Instantiate(platforms[platformIndex2], new Vector3(platform2PositionX, height + Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
        Instantiate(platforms[platformIndex3], new Vector3(platform3PositionX, height + Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
    }

    private void CreatePlatform(float height)
    {
        switch (Random.Range(1, 10))
        {
            case 1:
                CreateOnePlatform(height);
                break;
            case 2:
            case 3:
            case 4:
                CreateTwoPlatform(height);
                break;
            default:
                CreateThreePlatform(height);
                break;
        }
    }

    private void Start()
    {
        _maxHeight = player.transform.position.y;
        for (var i = 0; i < 2; i++)
        {
            _maxHeight += 5f;
            CreatePlatform(_maxHeight);
        }
    }

    private void LateUpdate()
    {
        if (player && !Player.IsGameOver)
        {
            if (transform.position != player.transform.position)
            {
                var targetPosition = player.transform.position;
                if (targetPosition.y + 9 >= _maxHeight)
                {
                    _maxHeight += 5;
                    CreatePlatform(_maxHeight);
                }


                transform.position = Vector3.Lerp(transform.position, targetPosition, 1);
            }

            if (player.transform.position == new Vector3(0, 0, 0))
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, 1);
                _maxHeight = player.transform.position.y;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        UpdateProgress(0, 360);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 screen_RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Rigidbody2D rd2d = GetComponent<Rigidbody2D>();
        Vector2 velocity = rd2d.velocity;
        Vector2 position = transform.position;

        float radius = 0.5f;

        bool toUpdate = false;
        int angleMin = 0;
        int angleMax = 360;

        if ((position.y - radius < screen_LeftBottom.y) && (velocity.y < 0))
        {
            toUpdate = true;
            angleMin = 0;
            angleMax = 180;
        }
        if ((position.x - radius < screen_LeftBottom.x) && (velocity.x < 0))
        {
            toUpdate = true;
            angleMin = -90;
            angleMax = 90;
        }
        if ((screen_RightTop.y < position.y + radius) && (0 < velocity.y))
        {
            toUpdate = true;
            angleMin = -180;
            angleMax = 0;
        }
        if ((screen_RightTop.x < position.x + radius) && (0 < velocity.x))
        {
            toUpdate = true;
            angleMin = 90;
            angleMax = 270;
        }

        // XV
        if (toUpdate) {
            UpdateProgress(angleMin + 10, angleMax - 10);
        }
    }

    public void UpdateProgress(int angleMin, int angleMax)
    {
        float direction = Random.Range(angleMin, angleMax);
        float speed = Random.Range(5, 10);

        Vector2 v;
        v.x = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
        v.y = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;

        Rigidbody2D rd2d = GetComponent<Rigidbody2D>();
        rd2d.velocity = v;
    }
}

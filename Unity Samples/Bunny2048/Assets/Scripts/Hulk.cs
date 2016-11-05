using UnityEngine;
using System.Collections;

public class Hulk : MonoBehaviour
{
    public float m_speed = 50f;
    public bool m_horizontal_mode = true;
    public bool m_vertical_mode = false;
    public float m_distance = 50f;

    Transform tr;
    Vector3 origin;
    float vertical_direction = 1f;
    float horizontal_direction = 1f;
    
    

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>() as Transform;
        origin = tr.position;
    }

    public void SetOrigin()
    {
        tr = GetComponent<Transform>() as Transform;
        origin = tr.position;
    }



    void FixedUpdate()
    {
        if (m_vertical_mode)
        {
            tr.position = tr.position + transform.up * m_speed * vertical_direction * Time.fixedDeltaTime;
            if (origin.y - tr.position.y < -m_distance/2)
            {
                vertical_direction *= -1;
            }
            if (origin.y - tr.position.y > m_distance/2)
            {
                vertical_direction *= -1;
            }
        }

        if (m_horizontal_mode)
        {
            tr.position = tr.position + transform.right * m_speed * horizontal_direction * Time.fixedDeltaTime;
            if (origin.x - tr.position.x < -m_distance / 2)
            {
                horizontal_direction *= -1;
            }
            if (origin.x - tr.position.x > m_distance / 2)
            {
                horizontal_direction *= -1;
            }
        }
    }

}

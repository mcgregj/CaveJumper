using UnityEngine;

public class lock_move : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // distance between object and camera
        float distance = Mathf.Abs(pos.z - cam.transform.position.z);

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
}
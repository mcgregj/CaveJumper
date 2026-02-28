using UnityEngine;

public class flip : MonoBehaviour
{
    private float horizontalInput;
    private bool faceingRight = true;
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        SetupDirectionallyScale();
    }
    private void SetupDirectionallyScale()
    {
        if (horizontalInput > 0 && !faceingRight || horizontalInput < 0 && faceingRight)
        {
            faceingRight = !faceingRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
        
    }
}

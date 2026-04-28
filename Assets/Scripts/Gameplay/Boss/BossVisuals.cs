using UnityEngine;

public class BossVisuals : MonoBehaviour
{
    public void Rotate(bool rotateRight)
    {
        transform.rotation = Quaternion.Euler(0, rotateRight ? 0 : 180, 0);
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Health>().OnHealthChanged += (x) => Debug.Log(x);
    }
}
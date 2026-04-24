using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Health>().HealthChanged += (x) => Debug.Log(x);
        GetComponent<Health>().Died += () => Debug.Log("ENEMY DEAD");
    }
}
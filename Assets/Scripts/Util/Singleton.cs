
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
    public static T Instance => instance;
    public static bool HasInstance => instance != null;

    public static T TryGetInstance() => HasInstance ? instance : null;

    private static T instance;
    protected virtual void Awake() {
        if (Instance == null)
            instance = this as T;
        else
            Destroy(gameObject);
    }
}

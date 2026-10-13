using UnityEngine;

public class PlayerPrefsDebugger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool clearOnAwake = true;
    [SerializeField] private bool destroyAfterClear = true;

    private void Awake()
    {
        if (!clearOnAwake)
            return;

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs cleared.");

        if (destroyAfterClear)
            Destroy(this);
    }

    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs cleared.");
    }
}
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    private void Awake()
    {
        CreateSingleton();
    }
}
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Debug.Log("ExitTrigger ha trovato LevelManager: " + levelManager.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger colpito da: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ha raggiunto l'uscita!");

            if (levelManager == null)
            {
                Debug.LogError("LevelManager è NULL!");
                return;
            }

            Debug.Log("Chiamo NextLevel() su: " + levelManager.name);
            levelManager.NextLevel();
        }
    }
}

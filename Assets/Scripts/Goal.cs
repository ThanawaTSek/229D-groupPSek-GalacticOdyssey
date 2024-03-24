using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!GameManager.Instance.IsWin) return;
        SceneManager.LoadScene("EndCredit");
    }
}

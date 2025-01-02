using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject teleportPos;
    public static Vector3 TeleportPos;

    void Update()
    {
        TeleportPos = teleportPos.transform.position;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
            LayerMask mask = LayerMask.GetMask("Ground");
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                teleportPos.transform.position = hit.point;
                teleportPos.SetActive(true);
                PosData.Pos = hit.point;
            }
        }
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

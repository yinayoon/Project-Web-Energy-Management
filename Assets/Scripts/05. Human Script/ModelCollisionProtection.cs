using UnityEngine;

public class ModelCollisionProtection : MonoBehaviour
{
    public Vector3 originPos;

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Vector3 vec = originPos;
            vec = vec + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
            transform.position = vec;
        }
    }
}

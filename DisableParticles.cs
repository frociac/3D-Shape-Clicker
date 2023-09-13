using UnityEngine;

public class DisableParticles : MonoBehaviour
{
    void OnDisable()
    {
        Destroy(this.gameObject);
    }
}

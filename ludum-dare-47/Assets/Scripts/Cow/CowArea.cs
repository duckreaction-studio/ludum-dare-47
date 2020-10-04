using UnityEngine;

public class CowArea : MonoBehaviour, ICowArea
{
    [SerializeField]
    private float radius;

    public Vector3 GetRandomPosition()
    {
        return transform.position + UnityEngine.Random.onUnitSphere * radius;
    }

    public void OnDrawGizmosSelected()
    {
        Color color = Color.green;
        color.a = 0.3f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}

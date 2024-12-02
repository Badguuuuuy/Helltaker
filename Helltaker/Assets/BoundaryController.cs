using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float gridSize = 1f; // �׸��� ����
    public Color gridColor = Color.green; // �׸��� ����

    private BoxCollider2D boxCollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        // BoxCollider2D ��������
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        // BoxCollider2D�� ũ��� ��ġ
        Vector2 colliderSize = boxCollider.size;
        Vector3 colliderOffset = boxCollider.offset;
        Vector3 position = transform.position + (Vector3)colliderOffset;

        // �׸��� ���� ���
        float halfWidth = colliderSize.x / 2f;
        float halfHeight = colliderSize.y / 2f;

        // ������ �׸���
        for (float x = -halfWidth; x <= halfWidth; x += gridSize)
        {
            Gizmos.DrawLine(
                new Vector3(position.x + x, position.y - halfHeight, position.z),
                new Vector3(position.x + x, position.y + halfHeight, position.z)
            );
        }

        // ���� �׸���
        for (float y = -halfHeight; y <= halfHeight; y += gridSize)
        {
            Gizmos.DrawLine(
                new Vector3(position.x - halfWidth, position.y + y, position.z),
                new Vector3(position.x + halfWidth, position.y + y, position.z)
            );
        }
    }
}

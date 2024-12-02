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
    public float gridSize = 1f; // 그리드 간격
    public Color gridColor = Color.green; // 그리드 색상

    private BoxCollider2D boxCollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        // BoxCollider2D 가져오기
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        // BoxCollider2D의 크기와 위치
        Vector2 colliderSize = boxCollider.size;
        Vector3 colliderOffset = boxCollider.offset;
        Vector3 position = transform.position + (Vector3)colliderOffset;

        // 그리드 범위 계산
        float halfWidth = colliderSize.x / 2f;
        float halfHeight = colliderSize.y / 2f;

        // 수직선 그리기
        for (float x = -halfWidth; x <= halfWidth; x += gridSize)
        {
            Gizmos.DrawLine(
                new Vector3(position.x + x, position.y - halfHeight, position.z),
                new Vector3(position.x + x, position.y + halfHeight, position.z)
            );
        }

        // 수평선 그리기
        for (float y = -halfHeight; y <= halfHeight; y += gridSize)
        {
            Gizmos.DrawLine(
                new Vector3(position.x - halfWidth, position.y + y, position.z),
                new Vector3(position.x + halfWidth, position.y + y, position.z)
            );
        }
    }
}

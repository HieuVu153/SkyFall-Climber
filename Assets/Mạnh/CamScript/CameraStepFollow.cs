using UnityEngine;

public class CameraStepFollow : MonoBehaviour
{
    public Transform player;
    
    [Header("Cấu hình vị trí")]
    [Tooltip("Tỷ lệ nhân vật nằm trong khung hình (0.33 là 1/3 từ dưới lên)")]
    public float playerHeightTarget = 0.33f;

    private float screenHeight;
    private float currentCameraTargetY;

    void Start()
    {
        // Tính chiều cao khung hình
        screenHeight = Camera.main.orthographicSize * 2f;
        
        // Khởi tạo vị trí đầu tiên
        SnapToPlayerLevel();
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Tính toán xem nhân vật đang ở level nào
        // Công thức này giúp xác định level dựa trên chiều cao map
        int level = Mathf.FloorToInt(player.position.y / screenHeight);

        // Vị trí Y lý tưởng của Camera để cạnh dưới khớp với chân map:
        // Cạnh dưới Camera = level * screenHeight
        // Tâm Camera = Cạnh dưới + (screenHeight / 2)
        // Để nhân vật ở 1/3, ta dịch chuyển tâm camera lên/xuống một chút
        
        float baseTargetY = (level * screenHeight) + (screenHeight / 2f);
        
        // Điều chỉnh để nhân vật nằm ở 1/3 (Offset)
        // Nếu muốn nhân vật ở thấp hơn tâm, ta phải đẩy Camera lên cao hơn một chút
        float offset = (0.5f - playerHeightTarget) * screenHeight;
        float finalTargetY = baseTargetY + offset;

        // Cập nhật vị trí Camera (Sử dụng Lerp để mượt hơn nếu muốn, hoặc gán thẳng)
        Vector3 newPos = transform.position;
        if (newPos.y != finalTargetY)
        {
            newPos.y = finalTargetY;
            transform.position = newPos;
        }
    }

    void SnapToPlayerLevel()
    {
        int level = Mathf.FloorToInt(player.position.y / screenHeight);
        float baseTargetY = (level * screenHeight) + (screenHeight / 2f);
        float offset = (0.5f - playerHeightTarget) * screenHeight;
        
        Vector3 newPos = transform.position;
        newPos.y = baseTargetY + offset;
        transform.position = newPos;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraOffsetX();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.instance != null) {
            if(Player.instance.IsAlive()) {
                MoveTheCamera();
            }
        }
    }

    private void MoveTheCamera() {
        Vector3 temp = transform.position;
        temp.x = Player.instance.GetPositionX() + offsetX;
        transform.position = temp;
    }

    private void SetCameraOffsetX() {
        offsetX = (transform.position.x - Player.instance.GetPositionX()) - 1f;
    }
}

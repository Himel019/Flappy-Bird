using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float offsetX;

    // Start is called before the first frame update
    void Start()
    {

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.06f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float rotateSpeed = 0.01f;

    [SerializeField] private float minHeight = 4f;
    [SerializeField] private float maxHeight = 40f;

    Vector2 p1;
    Vector2 p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = 0.06f;
            zoomSpeed = 20f;
        }
        else
        {
            speed = 0.035f;
            zoomSpeed = 10f;
        }


        
        float leftRightMovement = transform.position.y * Input.GetAxis("Horizontal") * speed;
        float frontBackMovement = transform.position.y * Input.GetAxis("Vertical") * speed;
        float scrollSpeed = Mathf.Log(transform.position.y) *  Input.GetAxis("Mouse ScrollWheel") * (-zoomSpeed); 


        if((transform.position.y >= maxHeight) && (scrollSpeed > 0)){
            scrollSpeed = 0;
        }else if((transform.position.y <= minHeight) && (scrollSpeed < 0)){
            scrollSpeed = 0;
        }

        if((transform.position.y + scrollSpeed) > maxHeight){
            scrollSpeed = maxHeight - transform.position.y;
        }else if((transform.position.y + scrollSpeed) < minHeight){
            scrollSpeed = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMove = leftRightMovement * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= frontBackMovement;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        getCameraRotation();
    }

    void getCameraRotation(){
        if(Input.GetMouseButtonDown(2)){
            p1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2)){
            p2 = Input.mousePosition;
            float dx = (p2 - p1).x *rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;
            Debug.Log(dx);
            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));


            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));
        }
    }
}

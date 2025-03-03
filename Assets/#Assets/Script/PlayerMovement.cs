using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]
    float forwardSpeed, horizontalSpeed, zTilt, yTilt;
    [SerializeField]
    private Joystick dyanamicJoyStick;
    private float v;
    Vector3 targetRotation;
    [Header("Max X value where player can move")]
    [Space]
    [SerializeField]
    float maxX;
    private void Update()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
        PlayerHorizontalMovement();
        PlayerBound();

    }
    void PlayerBound()
    {
        if (transform.position.x >= maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,0,0), forwardSpeed * Time.deltaTime);


        }
        else if (transform.position.x <= -maxX)
        {
            transform.position = new Vector3(-maxX, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), forwardSpeed * Time.deltaTime);

        }
    }
    void PlayerHorizontalMovement()
    {

        if (dyanamicJoyStick.Horizontal >= 0.1f || dyanamicJoyStick.Horizontal <= -0.1f)
        {
            //transform.position += (transform.right * dyanamicJoyStick.Horizontal)
            //     .normalized * speed * Time.deltaTime;

            Vector3 move = (transform.right * dyanamicJoyStick.Horizontal)
                 .normalized * horizontalSpeed * Time.deltaTime;
         
            move.y = 0f;
            transform.position += move;
            if (dyanamicJoyStick.Horizontal >= 0.1f)
            {
               
                targetRotation = new Vector3(0, yTilt, -zTilt);
                //transform.localEulerAngles= Vector3.Lerp(transform.localEulerAngles, targetRotation, forwardSpeed *Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), forwardSpeed * Time.deltaTime);


                //  print(targetRotation)
            }

           else if (dyanamicJoyStick.Horizontal <= -0.1f)
            {
                targetRotation=new Vector3(0, -yTilt, zTilt);
                // transform.localEulerAngles =  Vector3.Lerp(transform.localEulerAngles, targetRotation,Time.deltaTime*forwardSpeed);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), forwardSpeed * Time.deltaTime);

               // print(targetRotation+" Else Time");
            }
            // if(transform.position.x)
        }

        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,0,0), forwardSpeed * Time.deltaTime);

            //transform.localEulerAngles =Vector3.Lerp(transform.localEulerAngles, new Vector3(0,0,0), forwardSpeed * Time.deltaTime);
        }

    }

    
}

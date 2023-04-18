using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D weaponRb, moveRb;
    public Weapon weapon;

    private float activeMoveSpeed;
    private float dashSpeed = 15f;

    public float dashLength = .5f, dashCooldown = 3f;
    private float dashCounter;
    private float dashCoolCounter;
    

    Vector2 moveDirection;
    Vector2 mousePosition;

    private void Start() {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    private void Update() {
        
        if(Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
            

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {

            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

            }

        }

        if( dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <=0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }

        }

        if(dashCoolCounter > 0){

            dashCoolCounter -= Time.deltaTime;
        }
    }
    private void FixedUpdate() {


        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        moveRb.velocity  = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
        weaponRb.velocity = moveRb.velocity;
        Aiming();

    }



    private void Aiming(){


        Vector2 aimDirection = mousePosition - moveRb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        weaponRb.rotation = aimAngle;

    }
}

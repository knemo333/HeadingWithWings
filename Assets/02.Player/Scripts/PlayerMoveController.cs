using UnityEngine;
using System.Collections;

public class PlayerMoveController : MonoBehaviour
{
    public Sprite[] sprites = null;

    //private Animator animator;
    private PlayerInfo playerInfo;

	private float horizontal;
	private float vertical;
	//private float rotationDegreePerSecond = 1000;
	private bool isAttacking = false;
	private bool isStop = false;
	public bool IsStop
	{
		get{return isStop;}
	}

    public FloatingJoystick movement;
	Rigidbody2D rb;
	public TrailRenderer trail;
    public Ghost ghost = null;
	private Animator animator;


    void Start()
	{
        //animator = GetComponentInChildren<Animator>();
        playerInfo = GetComponent<PlayerInfo>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
    }

	private void Update() {
		if(rb.velocity.magnitude >= 7.5)
		{
			trail.emitting = true;
            ghost.makeGhost = true;
        }
		else
		{
			trail.emitting = false;
            ghost.makeGhost = false;
        }
	}

	void FixedUpdate()
	{
		if(!isStop)
		{
			//walk
			horizontal = movement.Horizontal;
			vertical = movement.Vertical;

			Vector2 stickDirection = new Vector2(horizontal, vertical);
			float speedOut;

			if (stickDirection.sqrMagnitude > 1) stickDirection.Normalize();

			if (!isAttacking)
				speedOut = stickDirection.sqrMagnitude;
			else
				speedOut = 0;

			// if (stickDirection != Vector3.zero && !isAttacking)
			// {
			//     modelTransform.rotation = Quaternion.RotateTowards(
			//         modelTransform.rotation, 
			//         Quaternion.LookRotation(stickDirection, Vector3.up), 
			//         rotationDegreePerSecond * Time.deltaTime
			//         );
			// }
				
			//rb.velocity = stickDirection * speedOut * GameManager.playerInfo.moveSpeed;// + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
			rb.AddForce(stickDirection * speedOut * playerInfo.moveSpeed);

            if (stickDirection.y < 0)
                animator.SetInteger("Direction", 0);
            else if (stickDirection.y > 0)
                animator.SetInteger("Direction", 1);

			if(stickDirection.x < 0)
				GetComponentInChildren<SpriteRenderer>().flipX = true;
			else if (stickDirection.x > 0)
				GetComponentInChildren<SpriteRenderer>().flipX = false;
			//animator.SetFloat("Speed", speedOut);

			if(horizontal != 0 && vertical != 0)
			{
				playerInfo.headAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
				playerInfo.headVector = stickDirection;
			}
				
		}
	}

	public void StopMove()
	{
		isStop = true;
	}

	public void ResumeMove()
	{
		isStop = false;
	}
}

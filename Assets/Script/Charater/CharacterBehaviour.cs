using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    public float WalkSpeed = 3;
    public float RunSpeed = 7;
    public float TurnSpeed = 90;

    private Vector3 velocity;       // 상대속도 구하는 벡터값
    private bool IsRun = false;     // 달리고 있는 상태인지
    public int attackCount;        // 공격 카운트

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        attackCount = 0;

        FloatingTextController.Initialize();
    }

    void FixedUpdate()
    {
        if (animator == null)
            return;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        KeyControl();
        MoveMent(x, y);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetBool("IsAttack", false);
                attackCount = 0;
            }
        }
    }

    private void MoveMent(float x, float y)
    {
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        velocity = new Vector3(0, 0, y);
        velocity.Normalize();

        velocity = transform.TransformDirection(velocity);

        if(!animator.GetBool("IsAttack"))
        {
            if (animator.GetBool("IsRun"))
                characterController.Move(velocity * Time.deltaTime * RunSpeed + Physics.gravity);
            else
                characterController.Move(velocity * Time.deltaTime * WalkSpeed + Physics.gravity);

            transform.Rotate(0, x * TurnSpeed * Time.deltaTime, 0);
        }
    }

    private void KeyControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            IsRun = !IsRun;
            animator.SetBool("IsRun", IsRun);
        }
        else if(Input.GetMouseButtonUp(0) == true)
        {
            if (animator.GetBool("IsRun"))
                animator.SetBool("IsRun", false);

            animator.SetBool("IsAttack", true);
            attackCount++;
        }
    }

    void AttackInit()
    {
        if(attackCount < 2)
        {
            animator.SetBool("IsAttack", false);
            attackCount = 0;
        }
    }

    void Attack1Start()
    {
        if(attackCount<3)
        {
            animator.SetBool("IsAttack", false);
            attackCount = 0;
        }
    }

    void Attack2Start()
    {
        if (attackCount < 4)
        {
            animator.SetBool("IsAttack", false);
            attackCount = 0;
        }
    }

    void Attack3Start()
    {
        if (attackCount < 5)
        {
            animator.SetBool("IsAttack", false);
            attackCount = 0;
        }
    }
}

using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;  // Reference to the Animator component

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving)
    {
        animator.SetBool("IsMoving", isMoving);
    }
}
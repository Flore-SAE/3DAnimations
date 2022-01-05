using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Coroutine coroutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //animator.SetFloat("Speed", 10);
        //animator.SetLayerWeight(1, 1);
        coroutine = StartCoroutine(AimCoroutine());
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        var inputDirection = ctx.ReadValue<Vector2>();
        animator.SetFloat("Speed", inputDirection.magnitude);
        animator.SetFloat("MoveY", inputDirection.y);
        animator.SetFloat("MoveX", inputDirection.x);
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        //StopCoroutine(coroutine);
        if(ctx.started)
            return;
        if(ctx.performed)
            animator.SetLayerWeight(1, 1);
        if(ctx.canceled)
            animator.SetLayerWeight(1, 0);
    }

    private IEnumerator AimCoroutine()
    {
        var startTime = Time.time;
        var endTime = startTime + 5f;
        while (Time.time < endTime)
        {
            var currentTime = Time.time - startTime;
            var progress = currentTime / 5f;
            var lerp = Mathf.Lerp(0, 1, progress);
            animator.SetLayerWeight(1, lerp);
            yield return null;
        }
    }

    private IEnumerator CoroutineExample()
    {
        var counter = 0;
        while (counter < 100)
        {
            counter++;
            Debug.Log(counter);
            yield return new WaitForSeconds(1);
        }
        /*
        yield return new WaitForSeconds(5);
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        */
    }
}

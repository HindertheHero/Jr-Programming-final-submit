using UnityEngine;

public class chickenwalk : Animal
{
    [SerializeField] protected float jump = 5f;
    [SerializeField] protected float minJumpInterval = 1f;
    [SerializeField] protected float maxJumpInterval = 6f;

    protected float jumpTimer;

    protected override void Start()
    {
        base.Start();
        jumpTimer = Random.Range(minJumpInterval, maxJumpInterval);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleJump();
    }

    private void HandleJump()
    {
        if (rb == null)
            return;

        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0f)
        {
            // apply an impulse upward (no grounded check)
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            jumpTimer = Random.Range(minJumpInterval, maxJumpInterval);
        }
    }
}








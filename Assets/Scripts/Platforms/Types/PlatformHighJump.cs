using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHighJump : Platform
{
    [SerializeField]
    private float _timeInAir;
    [SerializeField]
    private float _maxHeight;

    public override void BounceSphere()
    {
        ResetFreeze();
        if (SphereController.Instance.touchedAntiGravity)
            playerRb.AddForce(Vector3.down * bounceForce, ForceMode.Impulse);
        else
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        GameManager.Instance.CurrentCombo += 1;
        StartCoroutine(PrepareToFreeze());
    }

    private void Update()
    {
        if (!SphereController.Instance.IsPlayerDead && GameManager.Instance.isGameStarted && shouldMove)
            transform.Translate(PlatformManager.Instance.PlatformSpeed * Time.deltaTime * Vector3.left);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.constraints = RigidbodyConstraints.None;

        }
    }

    private IEnumerator FreezeInAir()
    {
        Debug.Log("Coroutine start: HighJump");
        playerRb.constraints = RigidbodyConstraints.FreezePositionY;
        playerRb.velocity = Vector3.zero;

        yield return new WaitForSeconds(_timeInAir);
        playerRb.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(0.2f);
        if (!SphereController.Instance.IsPlayerDead)
            gameObject.SetActive(false);

        Debug.Log("Coroutine end: HighJump");

    }

    private IEnumerator PrepareToFreeze()
    {
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(FreezeInAir());
    }

    private void ResetFreeze()
    {
        StopCoroutine(PrepareToFreeze());
        StopCoroutine(FreezeInAir());
        playerRb.constraints = RigidbodyConstraints.FreezePositionX;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum AnimationState {
    IDLE,
    START_THROW_UP,
    ANIMATING_PULSE_COLOR,
    THROWING_UP,
    RETURNING_IDLE
}

public class BridgeNormal : Bridge {
    [Range(0, 1)]
    public float chanceToThrow = 0.2f;
    public float moveUpSpeed = 3;
    public float moveDownSpeed = 3;
    public float moveUpHeight = 5;
    private AnimationState currentState = AnimationState.IDLE;
    private readonly static string PLAYER_TAG = "Player";
    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
    }

    private void FixedUpdate() {
        if (currentState == AnimationState.THROWING_UP) {
            transform.position += Vector3.up * Time.fixedDeltaTime * moveUpSpeed;
            if (transform.position.y - initialPosition.y >= moveUpHeight) {
                currentState = AnimationState.RETURNING_IDLE;
            }
        } else if (currentState == AnimationState.RETURNING_IDLE) {
            transform.position -= Vector3.up * Time.fixedDeltaTime * moveDownSpeed;
            if (transform.position.y <= initialPosition.y) {
                transform.position = initialPosition;
                currentState = AnimationState.IDLE;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(PLAYER_TAG) && Random.value <= chanceToThrow && currentState == AnimationState.IDLE) {
            currentState = AnimationState.ANIMATING_PULSE_COLOR;
            GetComponent<Animator>().SetBool("throwUp", true);
            transform.position = transform.position + Vector3.up * 0.01f;
        }
    }

    public void AnimationEnded() {
        GetComponent<Animator>().SetBool("throwUp", false);
        currentState = AnimationState.THROWING_UP;
    }

    public override void SetEndPoints(Vector3 start, Vector3 end) {
        Vector3 pos = (end - start) / 2 + start;
        transform.position = pos;
        Vector3 scale = transform.localScale;
        float distance = (end - start).magnitude;
        transform.localScale = new Vector3(scale.x, scale.y, scale.z * distance + 3f);
    }
}


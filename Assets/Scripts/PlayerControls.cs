using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls: MonoBehaviour
{
    [SerializeField] Rigidbody2D _physBody;
    [SerializeField] Transform _visualOrigin;

    [SerializeField] float _runVelocity;
    [SerializeField] float _jumpVelocity;
    [SerializeField] float _stompVelocity;
    Vector2 _tempVelocity;

    float _inputHorizontal = 0;
    float _inputJump = 0;

    [SerializeField] float _maxSlopeAngle;
    [SerializeField] LayerMask _whatIsGround;
    bool _isGrounded;

    private void Start()
    {
        _physBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _tempVelocity = new Vector2(_physBody.velocity.x * 0.9f ,_physBody.velocity.y);
        if(Mathf.Abs(_inputHorizontal) >= 0.1f)
        {
            _tempVelocity.x = _inputHorizontal * _runVelocity;
        }

        if(_inputJump >= 0.1f && _isGrounded)
        {
            _tempVelocity.y = _jumpVelocity;
        }
        if (_inputJump <= -0.1f && !_isGrounded)
        {
            _tempVelocity.y = -_stompVelocity;
        }

        _physBody.velocity = _tempVelocity;

        if(_physBody.velocity.x < 0)
        {
            _visualOrigin.localScale = new Vector3(-1,1,1);
        }
        if (_physBody.velocity.x > 0)
        {
            _visualOrigin.localScale = new Vector3(1, 1, 1);
        }
    }

    #region GroundDetection

    int _collisionlayer;
    bool _cancellingGrounded;

    Vector2 _normalVector;

    private void OnCollisionStay2D(Collision2D other)
    {
        //Make sure we are only checking for walkable layers
        _collisionlayer = other.gameObject.layer; //get layer of collision
        if (_whatIsGround != (_whatIsGround | (1 << _collisionlayer))) return; //return if collision is unwalkable

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector2 _normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(_normal))
            {
                _isGrounded = true;
                _cancellingGrounded = false;
                _normalVector = _normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float _delay = 3f;
        if (!_cancellingGrounded)
        {
            _cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * _delay);
        }
    }
    private bool IsFloor(Vector2 _v)
    {
        float _angle = Vector2.Angle(Vector2.up, _v);
        return _angle < _maxSlopeAngle;
    }

    private void StopGrounded()
    {
        _isGrounded = false;
    }
    #endregion

    #region Input

    public void OnMove(InputAction.CallbackContext _context)
    {
        _inputHorizontal = _context.ReadValue<float>();
    }
    public void OnJump(InputAction.CallbackContext _context)
    {
        _inputJump = _context.ReadValue<float>();
    }
    #endregion
}

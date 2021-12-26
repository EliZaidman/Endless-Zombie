using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouchController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private List<TouchData> _allTouches = new List<TouchData>();

    [SerializeField]
    private List<RectTransform> _touchIgnore, _touchables;

    [SerializeField]
    
    private Camera _mainCam;
    [SerializeField]
    private Rigidbody2D _playerRb;
    [SerializeField]
    private CharacterController _playerCc;

    [SerializeField]
    private Transform _playerTr, _gunPosTr;

    [SerializeField]
    private RectTransform _canvasTr, _leftJoystickTr, _leftJoystickBgTr, _rightJoystickTr, _rightJoystickBgTr;

    [SerializeField]
    private float _speed = 6f, _mainCamLerp = 2f, _mainCamSpeed = 100f;

    [SerializeField]
    private Fire _fire;
    #endregion

    private Vector2 _leftJoystickStartPos, _rightJoystickStartPos;
    private bool _isMoving;

    private Vector2 moveVelocity;
    private void Start()
    {
        _leftJoystickStartPos = _leftJoystickTr.transform.position;
        _rightJoystickStartPos = _rightJoystickTr.transform.position;
    }

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch currentTouch = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(currentTouch.position);

            if (currentTouch.phase == TouchPhase.Began)
            {
                Debug.Log("touch began");
                _allTouches.Add(new TouchData(currentTouch.fingerId, touchPos));
            }
            else if (currentTouch.phase == TouchPhase.Moved)
            {
                if (currentTouch.position.x < Screen.width / 2)
                {
                    Debug.Log("touch is moving");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == currentTouch.fingerId);

                    //LeftJystickMovement(thisTouch, touchPos);
                }
                else if (currentTouch.position.x > Screen.width / 2)
                {
                    for (int ignoreIndex = 0; ignoreIndex < _touchIgnore.Count; ignoreIndex++)
                    {
                        //if (!(_touchIgnore[ignoreIndex].position.x + _touchIgnore[ignoreIndex].sizeDelta.x / 2 > t.position.x))
                        //{
                        //}

                        Debug.Log("touch is moving");
                        TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == currentTouch.fingerId);

                        RightJystickMovement(thisTouch, touchPos);
                        Shoot(true);
                    }

                }
            }
            else if (currentTouch.phase == TouchPhase.Ended)
            {
                if (currentTouch.position.x < Screen.width / 2)
                {
                    Debug.Log("touch is moving");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == currentTouch.fingerId);

                    ResetLeftJoystick();

                    _allTouches.RemoveAt(_allTouches.IndexOf(thisTouch));
                }
                else if (currentTouch.position.x > Screen.width / 2)
                {
                    Debug.Log("touch ended");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == currentTouch.fingerId);

                    ResetRightJoystick();
                    Shoot(false);

                    _allTouches.RemoveAt(_allTouches.IndexOf(thisTouch));
                }
            }
            ++i;
        }
    }

    #region Methods
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    private void LeftJystickMovement(TouchData touchData, Vector2 touchPos)
    {
        Vector2 offset = touchPos - touchData.StartTouchPos;
        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

        Vector2 uiLeftJoystickPos = _mainCam.WorldToViewportPoint(new Vector2(touchData.StartTouchPos.x + direction.x, touchData.StartTouchPos.y + direction.y));
        Vector2 uiLeftJoystickBgPos = _mainCam.WorldToViewportPoint(new Vector2(touchData.StartTouchPos.x, touchData.StartTouchPos.y));

        Vector2 uiLeftJoystickScreenPosition = new Vector2((uiLeftJoystickPos.x * _mainCam.scaledPixelWidth) - (_mainCam.scaledPixelWidth * 0.5f), (uiLeftJoystickPos.y * _mainCam.scaledPixelHeight) - (_mainCam.scaledPixelHeight * 0.5f));
        Vector2 uiLeftJoystickBgScreenPosition = new Vector2((uiLeftJoystickBgPos.x * _mainCam.scaledPixelWidth) - (_mainCam.scaledPixelWidth * 0.5f), (uiLeftJoystickBgPos.y * _mainCam.scaledPixelHeight) - (_mainCam.scaledPixelHeight * 0.5f));

        _leftJoystickTr.anchoredPosition = uiLeftJoystickScreenPosition;
        _leftJoystickBgTr.anchoredPosition = uiLeftJoystickBgScreenPosition;

        MovePlayer(direction);
        //_mainCam.transform.position = Vector3.Lerp(_mainCam.transform.position, _mainCam.WorldToViewportPoint(touchPos * _mainCamSpeed * Time.deltaTime), _mainCamLerp);
    }

    private void RightJystickMovement(TouchData touchData, Vector2 touchPos)
    {
        Vector2 offset = touchPos - touchData.StartTouchPos;
        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

        Vector2 uiRightJoystickPos = _mainCam.WorldToViewportPoint(new Vector2(touchData.StartTouchPos.x + direction.x, touchData.StartTouchPos.y + direction.y));
        Vector2 uiRightJoystickBgPos = _mainCam.WorldToViewportPoint(new Vector2(touchData.StartTouchPos.x, touchData.StartTouchPos.y));

        Vector2 uiRightJoystickScreenPosition = new Vector2((uiRightJoystickPos.x * _mainCam.scaledPixelWidth) - (_mainCam.scaledPixelWidth * 0.5f), (uiRightJoystickPos.y * _mainCam.scaledPixelHeight) - (_mainCam.scaledPixelHeight * 0.5f));
        Vector2 uiRightJoystickBgScreenPosition = new Vector2((uiRightJoystickBgPos.x * _mainCam.scaledPixelWidth) - (_mainCam.scaledPixelWidth * 0.5f), (uiRightJoystickBgPos.y * _mainCam.scaledPixelHeight) - (_mainCam.scaledPixelHeight * 0.5f));

        _rightJoystickTr.anchoredPosition = uiRightJoystickScreenPosition;
        _rightJoystickBgTr.anchoredPosition = uiRightJoystickBgScreenPosition;

        //look

        Vector2 lookDirection = -(touchData.StartTouchPos - touchPos);
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _gunPosTr.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }

    private void ResetLeftJoystick()
    {
        _leftJoystickTr.transform.position = new Vector2(_leftJoystickStartPos.x, _leftJoystickStartPos.y);
        _leftJoystickBgTr.transform.position = new Vector2(_leftJoystickStartPos.x, _leftJoystickStartPos.y);
    }

    private void ResetRightJoystick()
    {
        _rightJoystickTr.transform.position = new Vector2(_rightJoystickStartPos.x, _rightJoystickStartPos.y);
        _rightJoystickBgTr.transform.position = new Vector2(_rightJoystickStartPos.x, _rightJoystickStartPos.y);
    }
    void MovePlayer(Vector2 direction)
    {
        //_playerTr.transform.position = (direction * _speed * Time.deltaTime);
        //_playerRb.AddRelativeForce(direction * _speed * Time.deltaTime, ForceMode2D.Force);
        //_playerRb.MovePosition(direction);
        //_playerCc.Move(direction * _speed * Time.deltaTime);
        //_playerRb.MovePosition((Vector2)transform.position + (direction * _speed * Time.fixedDeltaTime));
        //_playerTr.Translate(direction * _speed * Time.deltaTime);
        //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //direction.x = moveInput.x;
        //direction.y = moveInput.y;
    }

    //void PlayerLook(TouchData touch)
    //{
    //    Vector2 lookDirection = _mousePos - _playerGunRb.position;
    //    float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
    //    _playerGunRb.rotation = lookAngle;
    //}

    void Shoot(bool onOff)
    {
        //if (onOff)
        //    _fire.IsShooting = true;
        //else
        //    _fire.IsShooting = false;
    }
    #endregion
}
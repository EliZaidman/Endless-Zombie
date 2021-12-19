using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouchController : MonoBehaviour
{
    [SerializeField]
    private List<TouchData> _allTouches = new List<TouchData>();

    [SerializeField]
    private List<RectTransform> _touchIgnore;

    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Transform _playerTr, _gunPosTr;

    [SerializeField]
    private RectTransform _canvasTr, _leftJoystickTr, _leftJoystickBgTr, _rightJoystickTr, _rightJoystickBgTr;

    [SerializeField]
    private float _speed = 6f, _mainCamLerp = 2f, _mainCamSpeed = 100f;

    [SerializeField]
    private Fire _fire;

    private Vector2 _leftJoystickStartPos, _rightJoystickStartPos;
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
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position);

            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("touch began");
                _allTouches.Add(new TouchData(t.fingerId, touchPos));

            }
            else if (t.phase == TouchPhase.Moved)
            {
                if (t.position.x < Screen.width / 2)
                {
                    Debug.Log("touch is moving");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == t.fingerId);

                    LeftJystickMovement(thisTouch, touchPos);
                }
                else if (t.position.x > Screen.width / 2)
                {
                    for (int ignoreIndex = 0; ignoreIndex < _touchIgnore.Count; ignoreIndex++)
                    {
                        //if (!(_touchIgnore[ignoreIndex].position.x + _touchIgnore[ignoreIndex].sizeDelta.x / 2 > t.position.x))
                        //{
                        //}

                        Debug.Log("touch is moving");
                        TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == t.fingerId);

                        RightJystickMovement(thisTouch, touchPos);
                        Shoot(true);
                    }
                    
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (t.position.x < Screen.width / 2)
                {
                    Debug.Log("touch is moving");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == t.fingerId);

                    ResetLeftJoystick();

                    _allTouches.RemoveAt(_allTouches.IndexOf(thisTouch));
                }
                else if (t.position.x > Screen.width / 2)
                {
                    Debug.Log("touch ended");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == t.fingerId);

                    ResetRightJoystick();
                    Shoot(false);

                    _allTouches.RemoveAt(_allTouches.IndexOf(thisTouch));
                }
            }
            ++i;
        }
    }

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
                  
        Vector2 uiLeftJoystickScreenPosition = new Vector2((uiLeftJoystickPos.x * _canvasTr.sizeDelta.x) - (_canvasTr.sizeDelta.x * 0.5f), (uiLeftJoystickPos.y * _canvasTr.sizeDelta.y) - (_canvasTr.sizeDelta.y * 0.5f));
        Vector2 uiLeftJoystickBgScreenPosition = new Vector2((uiLeftJoystickBgPos.x * _canvasTr.sizeDelta.x) - (_canvasTr.sizeDelta.x * 0.5f), (uiLeftJoystickBgPos.y * _canvasTr.sizeDelta.y) - (_canvasTr.sizeDelta.y * 0.5f));
        
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

        Vector2 uiRightJoystickScreenPosition = new Vector2((uiRightJoystickPos.x * _canvasTr.sizeDelta.x) - (_canvasTr.sizeDelta.x * 0.5f), (uiRightJoystickPos.y * _canvasTr.sizeDelta.y) - (_canvasTr.sizeDelta.y * 0.5f));
        Vector2 uiRightJoystickBgScreenPosition = new Vector2((uiRightJoystickBgPos.x * _canvasTr.sizeDelta.x) - (_canvasTr.sizeDelta.x * 0.5f), (uiRightJoystickBgPos.y * _canvasTr.sizeDelta.y) - (_canvasTr.sizeDelta.y * 0.5f));
        
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
        _playerTr.Translate(direction * _speed * Time.deltaTime);
    }

    //void PlayerLook(TouchData touch)
    //{
    //    Vector2 lookDirection = _mousePos - _playerGunRb.position;
    //    float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
    //    _playerGunRb.rotation = lookAngle;
    //}
    void Shoot(bool onOff)
    {
        if (onOff)
            _fire.IsShooting = true;
        else
            _fire.IsShooting = false;
    }
}
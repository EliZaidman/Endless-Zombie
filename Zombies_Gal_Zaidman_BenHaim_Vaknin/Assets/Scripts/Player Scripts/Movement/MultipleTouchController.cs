using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouchController : MonoBehaviour
{
    [SerializeField]
    private List<TouchData> _allTouches = new List<TouchData>();

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Transform _playerTr, _leftJoystickTr, _leftJoystickBgTr, _rightJoystickTr, _rightJoystickBgTr;

    [SerializeField]
    private float _speed = 6f;

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
                    Debug.Log("touch is moving");
                    TouchData thisTouch = _allTouches.Find(touchLocation => touchLocation.TouchId == t.fingerId);

                    RightJystickMovement(thisTouch, touchPos);
                    Shoot(true);
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

    private void LeftJystickMovement(TouchData touchdata, Vector2 touchPos)
    {
        Vector2 offset = touchPos - touchdata.StartTouchPos;
        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

        _leftJoystickTr.transform.position = new Vector2(touchdata.StartTouchPos.x + direction.x, touchdata.StartTouchPos.y + direction.y);
        _leftJoystickBgTr.transform.position = new Vector2(touchdata.StartTouchPos.x, touchdata.StartTouchPos.y);

        MovePlayer(direction);
    }

    private void RightJystickMovement(TouchData touchdata, Vector2 touchPos)
    {
        Vector2 offset = touchPos - touchdata.StartTouchPos;
        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

        _rightJoystickTr.transform.position = new Vector2(touchdata.StartTouchPos.x + direction.x, touchdata.StartTouchPos.y + direction.y);
        _rightJoystickBgTr.transform.position = new Vector2(touchdata.StartTouchPos.x, touchdata.StartTouchPos.y);
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
    void Shoot(bool onOff)
    {
        if (onOff)
            _fire.IsShooting = true;
        else
            _fire.IsShooting = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouchController : MonoBehaviour
{
    [SerializeField]
    private Transform _player, _joystick, _joystickBg;

    [SerializeField]
    private float speed = 6f;

    private Fire _fire = new Fire();

    public GameObject Player;
    public List<TouchLocation> touches = new List<TouchLocation>();

    private Vector2 _startingPoint;

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
                touches.Add(new TouchLocation(t.fingerId, touchPos));
            }
            else if (t.phase == TouchPhase.Moved)
            {
                if (t.position.x < Screen.width / 2)
                {
                    Debug.Log("touch is moving");
                    TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.TouchId == t.fingerId);

                    Vector2 offset = touchPos - thisTouch.StartTouchPos;
                    Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

                    MovePlayer(direction);

                    _joystick.transform.position = new Vector2(_joystickBg.transform.position.x + direction.x, _joystickBg.transform.position.y + direction.y);

                    //thisTouch.Touchable.transform.position = getTouchPosition(t.position);
                }
                else if (t.position.x > Screen.width / 2)
                {
                    Debug.Log("touch is moving");
                    TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.TouchId == t.fingerId);
                    Shoot();

                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
                TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.TouchId == t.fingerId);
                //Destroy(thisTouch.Touchable);
                //touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            ++i;
        }
    }
    GameObject createCircle(Touch t)
    {
        GameObject c = Instantiate(Player) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        return c;
    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    Vector2 GetTouchPos(Touch t)
    {
        Vector2 touchPos = getTouchPosition(t.position);
        return touchPos;
    }

    void MovePlayer(Vector2 direction)
    {
        _player.Translate(direction * speed * Time.deltaTime);
    }
    void Shoot()
    {
        _fire.IsShooting = true;
    }
}
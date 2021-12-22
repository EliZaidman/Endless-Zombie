using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField]
    private Transform _player, _joystick, _joystickBg;

    [SerializeField]
    private float speed = 15.0f;

    private PlayerManager playerManager = new PlayerManager();
    private Vector2 startingPoint;
    private int leftTouch = 99;

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras

            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {

                }
                else
                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                if (t.position.x > Screen.width / 2)
                    shootBullet();
                else
                {
                    Vector2 offset = touchPos - startingPoint;
                    Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

                    moveCharacter(direction);

                    _joystick.transform.position = new Vector2(_joystickBg.transform.position.x + direction.x, _joystickBg.transform.position.y + direction.y);
                }

            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
                _joystick.transform.position = new Vector2(_joystickBg.transform.position.x, _joystickBg.transform.position.y);
            }
            ++i;
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    void moveCharacter(Vector2 direction)
    {
        _player.Translate(direction * speed * Time.deltaTime);
    }
    void shootBullet()
    {
        playerManager.IsShooting = true;
    }
}

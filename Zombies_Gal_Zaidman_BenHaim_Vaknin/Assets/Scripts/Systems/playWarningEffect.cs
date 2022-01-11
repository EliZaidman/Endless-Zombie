using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playWarningEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _top, _left, _right, _bottom;

    [SerializeField]
    private Image _upArrow, _leftArrow, _rightArrow, _downArrow1, _downArrow2;

    private float _topCounter = 0, _leftCounter = 0, _rightCounter = 0, _bottomCounter = 0;
    private bool _isTopCounterRunning = false, _isLeftCounterRunning = false, _isRightCounterRunning = false, _isBottomCounterRunning = false;

    //private void Update()
    //{
    //    if (_isTopCounterRunning)
    //    {
    //        Debug.Log("Top counter is running");

    //        _topCounter += Time.deltaTime;

    //        if (_topCounter < 0.5f)
    //            _upArrow.enabled = true;

    //        else if (_topCounter < 1f)
    //            _upArrow.enabled = false;

    //        else if (_topCounter < 1.5f)
    //            _upArrow.enabled = true;

    //        else if (_topCounter > 2.5f)
    //        {
    //            _upArrow.enabled = false;
    //            _topCounter = 0;
    //            _isTopCounterRunning = false;
    //        }

    //        Debug.Log("Top counter has stopped running");
    //    }

    //    if (_isLeftCounterRunning)
    //    {
    //        Debug.Log("Left counter is running");

    //        _leftCounter += Time.deltaTime;

    //        if (_leftCounter < 0.5f)
    //            _leftArrow.enabled = true;

    //        else if (_leftCounter < 1f)
    //            _leftArrow.enabled = false;

    //        else if (_leftCounter < 1.5f)
    //            _leftArrow.enabled = true;

    //        else if (_leftCounter > 2.5f)
    //        {
    //            _leftArrow.enabled = false;
    //            _leftCounter = 0;
    //            _isLeftCounterRunning = false;
    //        }

    //        Debug.Log("Left counter has stopped running");
    //    }

    //    if (_isRightCounterRunning)
    //    {
    //        Debug.Log("Right counter is running");

    //        _rightCounter += Time.deltaTime;

    //        if (_rightCounter < 0.5f)
    //            _rightArrow.enabled = true;

    //        else if (_rightCounter < 1f)
    //            _rightArrow.enabled = false;

    //        else if (_rightCounter < 1.5f)
    //            _rightArrow.enabled = true;

    //        else if (_rightCounter > 2.5f)
    //        {
    //            _rightArrow.enabled = false;
    //            _rightCounter = 0;
    //            _isRightCounterRunning = false;
    //        }

    //        Debug.Log("Right counter has stopped running");
    //    }

    //    if (_isBottomCounterRunning)
    //    {
    //        Debug.Log("Bottom counter is running");

    //        _bottomCounter += Time.deltaTime;

    //        if (_bottomCounter < 0.5f)
    //        {
    //            _downArrow1.enabled = true;
    //            _downArrow2.enabled = true;
    //        }

    //        else if (_bottomCounter < 1f)
    //        {
    //            _downArrow1.enabled = false;
    //            _downArrow2.enabled = false;
    //        }

    //        else if (_bottomCounter < 1.5f)
    //        {
    //            _downArrow1.enabled = true;
    //            _downArrow2.enabled = true;
    //        }

    //        else if (_bottomCounter > 2.5f)
    //        {
    //            _downArrow1.enabled = false;
    //            _downArrow2.enabled = false;
    //            _bottomCounter = 0;
    //            _isBottomCounterRunning = false;
    //        }

    //        Debug.Log("Bottom counter has stopped running");
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Top"))
        {
            //_isTopCounterRunning = true;
            Instantiate(GameManager.Instance.warningEffect, _top.transform);
            
            Debug.Log("Top Warning");
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Left"))
        {
            //_isLeftCounterRunning = true;
            Instantiate(GameManager.Instance.warningEffect, _left.transform);
            Debug.Log("Left Warning");
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Right"))
        {
            //_isRightCounterRunning = true;
            Instantiate(GameManager.Instance.warningEffect, _right.transform);
            
            Debug.Log("Right Warning");
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Bottom"))
        {
            //_isBottomCounterRunning = true;
            Instantiate(GameManager.Instance.warningEffect, _bottom.transform);
            
            Debug.Log("Bottom Warning");
        }
    }
}

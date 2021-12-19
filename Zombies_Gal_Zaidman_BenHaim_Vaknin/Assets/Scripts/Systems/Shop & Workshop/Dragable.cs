using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
public class Dragable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private GameObject _relatedGOPrefab;

    [SerializeField]
    private RectTransform _tr;

    private Vector2 _startPos;

    [SerializeField]
    NavMeshSurface2d _navmesh2D;

    [SerializeField]
    private Tilemap _groundGO, _wallsGO;

    [SerializeField]
    private TileBase _itemTile;

    private void Awake()
    {
        _startPos = _tr.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        StartCoroutine(ReplaceInShop());

        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _tr.position += (Vector3)eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
        StartCoroutine(PlaceGO());
    }

    IEnumerator ReplaceInShop()
    {
        Instantiate(gameObject, _startPos, Quaternion.identity);
        Debug.Log("Made a copy");
        //_tr.anchoredPosition = _startPos;
        yield return null;
    }

    IEnumerator PlaceGO()
    {
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Instantiate(_relatedGOPrefab, targetPos, Quaternion.identity);
        _wallsGO.SetTile(_groundGO.WorldToCell(targetPos), _itemTile);
        _groundGO.SetTile(_groundGO.WorldToCell(targetPos), _itemTile);
        _tr.anchoredPosition = _startPos;
        //Destroy(gameObject);
        _navmesh2D.BuildNavMesh();

        yield return null;
    }
}

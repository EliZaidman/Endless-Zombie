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
    private RectTransform _tr;

    [SerializeField]
    private NavMeshSurface2d _navmesh2D;

    [SerializeField]
    private Tilemap _itemGO;

    [SerializeField]
    private TileBase _itemTile;

    private Vector2 _startPos;

    public string Type;
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
        if (Shop.Instance.GeneralCoins < 25)
            return;
        else
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
        yield return null;
    }

    IEnumerator PlaceGO()
    {
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _itemGO.SetTile(_itemGO.WorldToCell(targetPos), _itemTile);
        _tr.anchoredPosition = _startPos;
        _navmesh2D.BuildNavMesh();
        Shop.Instance.GeneralCoins -= 25;
        AudioManager.Instance.PlayMusic(Shop.Instance.buySuond);

        yield return null;
    }

}

using UnityEngine;

public class BildCursor : MonoBehaviour
{
    public LayerMask layer;

    private void Start()
    {
        CursorBinding();
    }

    private void Update()
    {
        CursorBinding();
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject.GetComponent<BildCursor>());  
    }

    private void CursorBinding(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))
        {
            Vector3 newPos = hit.point;
            newPos.y += 1f;  // ������� �� 1 ������� �����
            transform.position = newPos;
        }
    }
}




using UnityEngine;

public class BildCursor : MonoBehaviour
{
    public LayerMask layer;
    private float rotationSpeed = 100f;

    private void Start()
    {
        CursorBinding();
    }

    private void Update()
    {
        CursorBinding();
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject.GetComponent<BildCursor>()); 

        if (Input.GetKey(KeyCode.C))
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.Z))
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
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




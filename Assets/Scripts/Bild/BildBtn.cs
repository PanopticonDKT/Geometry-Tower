using UnityEngine;

public class BildBtn : MonoBehaviour
{
    public GameObject Bild;
    public void BildVdBtn()
    {
        Instantiate(Bild, Vector3.zero, Quaternion.identity);
    }
}

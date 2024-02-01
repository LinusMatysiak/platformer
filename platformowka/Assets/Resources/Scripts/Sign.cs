using UnityEngine;
using TMPro;
public class Sign : MonoBehaviour
{
    GameObject signContentObj;
    string signContent;
    private void Start()
    {
        signContentObj = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>().gameObject;
        signContent = signContentObj.GetComponent<TMP_Text>().text;
        //signContentObj.GetComponent<TMP_Text>().text = "";
        Debug.Log(signContent);
    }
    public void SignBehaviour(){

    }
}

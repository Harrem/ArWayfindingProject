using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements : MonoBehaviour
{
    [SerializeField]
    private GameObject placeSelectionPanel;
    private Animator placeAnim;
    [SerializeField]
    private GameObject qrCodeFramePanel;
    private Animator qrAnim;


    // Start is called before the first frame update
    void Start()
    {
        placeSelectionPanel.SetActive(false);
        qrCodeFramePanel.SetActive(false);
        placeAnim = placeSelectionPanel.GetComponentInChildren<Animator>();
        qrAnim = qrCodeFramePanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPlaceSelection(){
        placeSelectionPanel.SetActive(true);
        placeAnim.SetTrigger("open");
    }

    public void closePlaceSelection()
    {
        placeAnim.SetTrigger("close");
        WaitAndClosePlace(1);
    }
    private IEnumerator WaitAndClosePlace(int sec)
    {
        yield return new WaitForSeconds(sec);
        placeSelectionPanel.SetActive(false);

    }

    public void openQrScanner()
    {
        qrCodeFramePanel.SetActive(true);
        qrAnim.SetTrigger("open");
    }

     public void closeQrScanner()
    {
        qrAnim.SetTrigger("close");
        StartCoroutine(WaitAndCloseQR(0.3f));
    }

    private IEnumerator WaitAndCloseQR(float sec)
    {
        yield return new WaitForSeconds(sec);
        qrCodeFramePanel.SetActive(false);

    }

}

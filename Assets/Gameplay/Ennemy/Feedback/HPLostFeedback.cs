using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPLostFeedback : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_lost_HP_Text;

    public void SetLostHPAmount(int a_lostHP)
    {
        m_lost_HP_Text.text = a_lostHP.ToString();
    }

    private void OnEnable()
    {
        StartCoroutine(Hide_Routine());
    }

    private IEnumerator Hide_Routine()
    {
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }
}

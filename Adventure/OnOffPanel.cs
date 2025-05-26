using UnityEngine;

// UI OnOff 기능
public class OnOffPanel : MonoBehaviour
{
    public GameObject panel;
    public void PanelOnOff()
    {
        panel.SetActive(!panel.activeSelf); // 판넬을 현재의 반대 상태로 바꿈
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    //Open screen
    [SerializeField] private GameObject openScreen;
    [SerializeField] private InputField lengthText;
    [SerializeField] private InputField widthText;
    [SerializeField] private InputField heightText;
    [SerializeField] private InputField lengthGapText;
    [SerializeField] private InputField widthGapText;
    [SerializeField] private Text errorText;

    //InSceneScreen
    [SerializeField] private GameObject inSceneScreen;
    [SerializeField] private InfoPanelScript infoPanel;

    private bool onScene;
    public bool GetOnScene() { return onScene; }
    public InfoPanelScript GetInfoPanel() { return infoPanel; }

    private void Start()
    {
        onScene = false;

        inSceneScreen.SetActive(false);
        openScreen.SetActive(true);
    }

    /// <summary>
    /// ������� ������� �� ������ "�������"
    /// </summary>
    public void CreateButtonClick()
    {
        if (!CheckCorrectFields()) return;

        GetComponent<SceneManager>().CreateLevel();

        onScene = true;

        openScreen.SetActive(false);
        inSceneScreen.SetActive(true);
    }

    /// <summary>
    /// �������������� ������� ������� �� ������ "�������"
    /// </summary>
    /// <returns>���������� ��������� �� ������� ������ � �������� �� � SceneManager</returns>
    private bool CheckCorrectFields()
    {
        int length;
        int width;
        int height;
        float lengthGap;
        float widthGap;

        int _i;
        if (!int.TryParse(lengthText.text, out _i))
        {
            errorText.text = "������! ���������� ������������� ����� ����!";
            return false;
        }
        else
            length = _i;

        if (length < 1) 
        {
            errorText.text = "������! ����� ���� ������ ���� ������ 0!";
            return false;
        }

        if (!int.TryParse(widthText.text, out _i))
        {
            errorText.text = "������! ���������� ������������� ������ ����!";
            return false;
        }
        else
            width = _i;

        if (width < 1)
        {
            errorText.text = "������! ������ ���� ������ ���� ������ 0!";
            return false;
        }

        if (!int.TryParse(heightText.text, out _i))
        {
            errorText.text = "������! ���������� ������������� ������ ����!";
            return false;
        }
        else
            height = _i;

        if (height < 1)
        {
            errorText.text = "������! ������ ���� ������ ���� ������ 0!";
            return false;
        }

        float _f;
        if (!float.TryParse(lengthGapText.text, out _f))
        {
            errorText.text = "������! ���������� ������������� ����� �� �����!";
            return false;
        }
        else
            lengthGap = _f;

        if (lengthGap < 0)
        {
            errorText.text = "������! ����� �� ����� ���� ������ 0!";
            return false;
        }

        if (!float.TryParse(widthGapText.text, out _f))
        {
            errorText.text = "������! ���������� ������������� ����� �� ������!";
            return false;
        }
        else
            widthGap = _f;

        if (widthGap < 0)
        {
            errorText.text = "������! ����� �� ������ ���� ������ 0!";
            return false;
        }

        GetComponent<SceneManager>().SetData(length, width, height, lengthGap, widthGap);

        return true;
    }
}

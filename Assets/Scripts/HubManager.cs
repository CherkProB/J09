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
    /// Функция нажатия на кнопку "Создать"
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
    /// Вспомогателная функция нажатия на кнопку "Создать"
    /// </summary>
    /// <returns>Возвращает правильно ли введены данные и передает их в SceneManager</returns>
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
            errorText.text = "Ошибка! Невозможно преобразовать длину поля!";
            return false;
        }
        else
            length = _i;

        if (length < 1) 
        {
            errorText.text = "Ошибка! Длина поля должна быть больше 0!";
            return false;
        }

        if (!int.TryParse(widthText.text, out _i))
        {
            errorText.text = "Ошибка! Невозможно преобразовать ширину поля!";
            return false;
        }
        else
            width = _i;

        if (width < 1)
        {
            errorText.text = "Ошибка! Ширина поля должна быть больше 0!";
            return false;
        }

        if (!int.TryParse(heightText.text, out _i))
        {
            errorText.text = "Ошибка! Невозможно преобразовать высоту поля!";
            return false;
        }
        else
            height = _i;

        if (height < 1)
        {
            errorText.text = "Ошибка! Высота поля должна быть больше 0!";
            return false;
        }

        float _f;
        if (!float.TryParse(lengthGapText.text, out _f))
        {
            errorText.text = "Ошибка! Невозможно преобразовать зазор по длине!";
            return false;
        }
        else
            lengthGap = _f;

        if (lengthGap < 0)
        {
            errorText.text = "Ошибка! Зазор по длине быть больше 0!";
            return false;
        }

        if (!float.TryParse(widthGapText.text, out _f))
        {
            errorText.text = "Ошибка! Невозможно преобразовать зазор по ширине!";
            return false;
        }
        else
            widthGap = _f;

        if (widthGap < 0)
        {
            errorText.text = "Ошибка! Зазор по ширине быть больше 0!";
            return false;
        }

        GetComponent<SceneManager>().SetData(length, width, height, lengthGap, widthGap);

        return true;
    }
}

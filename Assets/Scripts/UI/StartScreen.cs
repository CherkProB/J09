using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private InputField lengthFieldInput;
    [SerializeField] private InputField widthFieldInput;
    [SerializeField] private InputField heightFieldInput;
    [SerializeField] private InputField lengthGapFieldInput;
    [SerializeField] private InputField widthGapFieldInput;

    [SerializeField] private Text errorText;

    private void Start()
    {
        //Сброс всех полей ввода
        //На случай если это не было сделано
        lengthFieldInput.text = string.Empty;
        widthFieldInput.text = string.Empty;
        heightFieldInput.text = string.Empty;
        lengthGapFieldInput.text = string.Empty;
        widthGapFieldInput.text = string.Empty;

        errorText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Проверяет возможно ли преобразовать данные из полей ввода в необходимый тип данных
    /// </summary>
    public bool CheckCorrectData() 
    {
        int _i;
        if(!int.TryParse(lengthFieldInput.text, out _i))
            return false;

        if (_i < 1) return false;

        if (!int.TryParse(widthFieldInput.text, out _i))
            return false;

        if (_i < 1) return false;

        if (!int.TryParse(heightFieldInput.text, out _i))
            return false;

        if (_i < 1) return false;

        float _f;
        if (!float.TryParse(lengthGapFieldInput.text, out _f))
            return false;

        if(_f < 0.1f) return false;

        if (!float.TryParse(widthGapFieldInput.text, out _f))
            return false;

        if (_f < 0.1f) return false;

        return true;
    }

    /// <summary>
    /// Включает отображение ошибки
    /// </summary>
    /// <param name="error">Текст ошибки, который будет выведен</param>
    public void SetError(string error = "Невозможно преобразовать данные из полей данных!") 
    {
        errorText.text = error;
        errorText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Преобразует данные из полей ввода в соответствующие переменные
    /// </summary>
    /// <returns>Возвращает пользовательский тип данных с информацией о поле</returns>
    public FieldData GetFieldSize() 
    {
        if (!int.TryParse(lengthFieldInput.text, out int length))
            return null;

        if (!int.TryParse(widthFieldInput.text, out int width))
            return null;

        if (!int.TryParse(heightFieldInput.text, out int height))
            return null;

        if (!float.TryParse(lengthGapFieldInput.text, out float lengthGap))
            return null;

        if (!float.TryParse(widthGapFieldInput.text, out float widthGap))
            return null;

        return new FieldData(length, width, height, lengthGap, widthGap);
    }
}

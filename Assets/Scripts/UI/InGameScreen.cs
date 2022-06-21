using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScreen : MonoBehaviour
{
    //Информация о панели
    private ShowInformationPanel panel;
    //
    private Text modeText;
    //Свойтсва
    public ShowInformationPanel Panel { get { return panel; } set { panel = value; } }
    public Text ModeText { get { return modeText; } set { modeText = value; } }

    /// <summary>
    /// Измение текста режима
    /// </summary>
    /// <param name="flag">Состояние режима просмотра</param>
    public void ChangeMode(bool flag) => modeText.text = flag ? "Просмотр" : "Создание/Удаление";
}

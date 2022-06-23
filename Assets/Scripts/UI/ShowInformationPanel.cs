using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInformationPanel : MonoBehaviour
{
    //Текста заголовка и описания
    private Text title;
    private Text description;
    //Время появления панели
    private float appearanceTime;

    //Свойства
    public Text Title { set { title = value; } }
    public Text Description { set { description = value; } }
    public float AppearanceTime { set { appearanceTime = value; } }

    /// <summary>
    /// Корутина появления панели информации
    /// </summary>
    /// <param name="title">Заголовок панели информации</param>
    /// <param name="description">Описание панели информации</param>
    /// <param name="mousePos">Позиция, в которй должна располагаться панель</param>
    /// <returns></returns>
    public IEnumerator ShowInfoCor(string title, string description, Vector3 mousePos)
    {
        //Включение панели, а также изменение все необходимой информации
        gameObject.SetActive(true);
        gameObject.transform.position = mousePos + Vector3.down * 50;
        this.title.text = title;
        this.description.text = description;

        //Получения заднего фона панели и ее цвета
        Image panelBg =  gameObject.GetComponent<Image>();
        Color panelColor = panelBg.color;

        //Изменение прозрачности заднего фона
        panelColor.a = 0;
        panelBg.color = panelColor;
        for (float i = 0; i < 1; i += Time.deltaTime / appearanceTime)
        {
            panelColor.a = Mathf.Lerp(0, 1, i);
            panelBg.color = panelColor;
            yield return null;
        }
        panelColor.a = 1;
        panelBg.color = panelColor;
    }

    /// <summary>
    /// Корутина исчезновения панели информации
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideInfoCor() 
    {
        Image panelBg = gameObject.GetComponent<Image>();
        Color panelColor = panelBg.color;

        //panelColor.a = 0;
        panelBg.color = panelColor;

        for (float i = panelColor.a; i > 0; i -= Time.deltaTime / appearanceTime * 2)
        {
            panelColor.a = Mathf.Lerp(0, 1, i);
            panelBg.color = panelColor;
            yield return null;
        }

        panelColor.a = 0;
        panelBg.color = panelColor;

        gameObject.SetActive(false);
    }
}

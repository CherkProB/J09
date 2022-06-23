using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    //Экраны интерфейса
    private StartScreen startScreen;
    private InGameScreen gameScreen;
    //Событие создания уровня
    private UnityEvent<FieldData> createLevelEvent;
    //События панели с информацией
    private UnityEvent<Vector3, Container> showPanelAnimation;
    private UnityEvent hidePanelAnimation;

    private UnityEvent<bool> changeModeText;

    //Свойства
    public StartScreen StartScreen { set { startScreen = value; } }
    public InGameScreen GameScreen { get { return gameScreen; }  set { gameScreen = value; } }
    public UnityEvent<FieldData> CreateLevelEvent { get { return createLevelEvent; } }
    public UnityEvent<Vector3, Container> ShowPanelAnimation { get { return showPanelAnimation; } }
    public UnityEvent HidePanelAnimation { get { return hidePanelAnimation; } }
    public UnityEvent<bool> ChangeModeText { get { return changeModeText; } }


    /// <summary>
    /// Инициализация основных полей
    /// </summary>
    private void Awake()
    {
        createLevelEvent = new UnityEvent<FieldData>();
        showPanelAnimation = new UnityEvent<Vector3, Container>();
        hidePanelAnimation = new UnityEvent();
        changeModeText = new UnityEvent<bool>();
    }

    /// <summary>
    /// Инициализация полей дочерних объектов
    /// Настойка по умолчанию интерфеса сцены
    /// </summary>
    private void Start()
    {
        showPanelAnimation.AddListener(ShowInfo);
        hidePanelAnimation.AddListener(HideInfo);
        changeModeText.AddListener(ChangeModeInfo);

        startScreen.gameObject.SetActive(true);
        gameScreen.gameObject.SetActive(false);

        HideInfo();
    }

    /// <summary>
    /// Функция нажатия на кнопку "Создать"
    /// </summary>
    public void CreateButtonClick()
    {
        if (!startScreen.CheckCorrectData()) 
        {
            startScreen.SetError();
            return;
        }

        createLevelEvent.Invoke(startScreen.GetFieldSize());

        startScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// Вызов анимации появления панели информации
    /// </summary>
    /// <param name="infoPanelPosition">Позиция, в которой должна появится панель</param>
    /// <param name="container">Контейнер, на который остановилась мышь</param>
    public void ShowInfo(Vector3 infoPanelPosition, Container container) 
    {
        if (!container) return;

        string desc = "";
        desc += "Секция: " + (container.Position.z + 1) + '\n';
        desc += "Ряд: " + (container.Position.x + 1) + '\n';
        desc += "Высота: " + (container.Position.y + 1);

        StartCoroutine(gameScreen.Panel.ShowInfoCor("Контейнер", desc, infoPanelPosition));
    }

    /// <summary>
    /// Вызов анимции исчезновения панели информации
    /// </summary>
    public void HideInfo() => StartCoroutine(gameScreen.Panel.HideInfoCor());

    private void ChangeModeInfo(bool flag) => gameScreen.ChangeMode(flag);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //Камера
    private Camera cam;
    //События нажатия на кнопки
    private UnityEvent<Collider> leftMouseButton;
    private UnityEvent<Collider> rightMouseButton;
    private UnityEvent qButton;
    //События появления и исчезновения панели
    private UnityEvent<Vector3, Container> showInfoPanel;
    private UnityEvent hideInfoPanel;
    //Проверка неподвижности мыши
    private Vector3 mousePositionBuffer;
    private float currentIdleTime;
    private bool mouseIdle;
    private float idleTime;

    //Свойства
    public Camera Camera { set { cam = value; } }
    public UnityEvent<Collider> LeftMouseButton { get { return leftMouseButton; } }
    public UnityEvent<Collider> RightMouseButton { get { return rightMouseButton; } }
    public UnityEvent QButton { get { return qButton; } }
    public UnityEvent<Vector3, Container> ShowInfoPanel { set { showInfoPanel = value; } get { return showInfoPanel; } }
    public UnityEvent HideInfoPanel { set { hideInfoPanel = value; } get { return hideInfoPanel; } }
    public float IdleTime { set { idleTime = value; } }

    /// <summary>
    /// Инициализация основных полей
    /// </summary>
    private void Awake()
    {
        leftMouseButton = new UnityEvent<Collider>();
        rightMouseButton = new UnityEvent<Collider>();
        qButton = new UnityEvent();
        showInfoPanel = new UnityEvent<Vector3, Container>();
        hideInfoPanel = new UnityEvent();

        mousePositionBuffer = Vector3.zero;
        currentIdleTime = 0;
        mouseIdle = false;
    }

    /// <summary>
    /// Проверка на нажатие той или иной кнопки
    /// Проверка двигается ли мышь
    /// </summary>
    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
            leftMouseButton.Invoke(hit.collider);

        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
            rightMouseButton.Invoke(hit.collider);

        if (Input.GetKeyDown(KeyCode.Q))
            qButton.Invoke();

        //Проверка двигается ли мышь
        //Сравнивается текущее положение мыши с положением предыдущего кадра
        currentIdleTime = mousePositionBuffer == Input.mousePosition ? currentIdleTime + Time.deltaTime : 0;
        //Если панель показана, а мышь двигается
        //То спрятать панель
        if (mouseIdle && currentIdleTime == 0) 
        {
            hideInfoPanel.Invoke();
            mouseIdle = false;
        }
        
        //Если панель не показана, а мышь не двигается заданное время
        //То показать панель
        if (!mouseIdle && currentIdleTime > idleTime && Physics.Raycast(ray, out hit))
        {
            showInfoPanel.Invoke(Input.mousePosition, hit.collider.GetComponent<Container>());
            mouseIdle = true;
        }

        //Запоминание позиции мыши
        mousePositionBuffer = Input.mousePosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    private List<ContainerRow> containerRows;
    private bool inspectorMode;

    //Дочерние объекты
    private UI ui;
    private InputManager inputManager;
    private CameraController cameraController;
    //Информация о поле
    private FieldData fieldData;
    private Transform spawnPoint;
    //О контейнерах
    private Vector3Int containerSize;
    private Container containerPrefab;
    private ContainerSpot containerSpotPrefab;
    private ContainerRow containerRowPrefab;
    private ContainerFactory containerFactory;
    //Изменение текста о состоянии просмотра
    private UnityEvent<bool> changeModeText;

    //Свойства
    public UI UI { set { ui = value; } }
    public InputManager InputManager { set { inputManager = value; } }
    public ContainerFactory ContainerFactory { set { containerFactory = value; } }
    public Vector3Int ContainerSize { set { containerSize = value; } }
    public Transform SpawnPoint { set { spawnPoint = value; } }
    public CameraController Camera { set { cameraController = value; } }
    public Container ContainerPrefab { set { containerPrefab = value; } }
    public ContainerSpot ContainerSpotPrefab { set { containerSpotPrefab = value; } }
    public ContainerRow ContainerRowPrefab { set { containerRowPrefab = value; } }
    public UnityEvent<bool> ChangeModeText { set { changeModeText = value; } }

    /// <summary>
    /// Инициализация основных полей
    /// </summary>
    private void Awake()
    {
        containerRows = new List<ContainerRow>();
    }

    /// <summary>
    /// Инициализация дочерних объектов
    /// </summary>
    private void Start()
    {
        ui.CreateLevelEvent.AddListener(CreateLevel);

        inputManager.LeftMouseButton.AddListener(LeftMouseButtonClick);
        inputManager.RightMouseButton.AddListener(RightMouseButtonClick);
        inputManager.QButton.AddListener(ToggleInspectorMode);

        inspectorMode = false;
    }


    /// <summary>
    /// Метод создания поля для контейнеров
    /// </summary>
    /// <param name="data">Информация о поле для контейнеров</param>
    private void CreateLevel(FieldData data)
    {
        //Присвоение инормации о размерах поля глобальной переменной
        fieldData = data;
        //Присвоение инормации о размерах поля для фабрики контейнеров
        containerFactory.FieldSize = fieldData;

        //По рядам
        for (int i = 0; i < fieldData.Length; i++)
        {
            //Создание ряда контейнеров
            Vector3 newContainerRowPos = new Vector3((fieldData.LengthGap + containerSize.x) * i, 0, 0);
            ContainerRow newContainerRow = Instantiate(containerRowPrefab, newContainerRowPos, Quaternion.identity, spawnPoint);

            //По секциям
            for (int j = 0; j < fieldData.Width; j++)
            {
                //Создание точки появления контейнера
                Vector3 containerSpotPos = new Vector3((fieldData.LengthGap + containerSize.x) * i, 0.2f, (fieldData.WidthGap + containerSize.z) * j);
                ContainerSpot newContainerSpot = Instantiate(containerSpotPrefab, containerSpotPos, Quaternion.identity, newContainerRow.transform);

                //Присвоение необходимой информации созданой точке
                newContainerSpot.Position = new Vector3Int(i, -1, j);
                newContainerRow.ContainerSpots.Add(newContainerSpot);
            }

            //Добавление созданного ряда в список
            containerRows.Add(newContainerRow);
        }

        //Настройка центра вращения у камеры
        float sceneCenterX = ((float)(fieldData.Length * (containerSize.x + fieldData.LengthGap) - fieldData.LengthGap) - containerSize.x) / 2;
        float sceneCenterY = 1f;
        float sceneCenterZ = ((float)(fieldData.Width * (containerSize.z + fieldData.WidthGap) - fieldData.WidthGap) - containerSize.z) / 2;

        //Присвоение ценра вращения камере
        Vector3 newCenter = new Vector3(sceneCenterX, sceneCenterY, sceneCenterZ);
        cameraController.SetSceneCenter(newCenter);
    }

    /// <summary>
    /// Метод, вызываемый при нажатии левой кнопки мыши
    /// </summary>
    /// <param name="obj">Коллайдер объекта, на который нажали левой кнопкой</param>
    private void LeftMouseButtonClick(Collider obj)
    {
        //Для определения на какой объект нажали
        ContainerSpot spotSender = obj.GetComponent<ContainerSpot>();
        Container containerSender = obj.GetComponent<Container>();

        //Проверка на null
        //Если нажали ни на точку, ни на контейнер
        if (spotSender == containerSender) return;

        //Опустить все ряды
        foreach (ContainerRow row in containerRows)
            row.DropRow();


        if (inspectorMode) //Поднять ряд
        {
            //Получение позиции, на которую нажали
            Vector3Int newContainerPos = Vector3Int.zero;
            if (spotSender)
                newContainerPos = spotSender.Position;
            else if (containerSender)
                newContainerPos = containerSender.Position;
            else
                return; //error

            //Поднятие ряда по найденной позиции
            containerRows[newContainerPos.x].RaiseRow(containerSize, fieldData.Height);
        }
        else //Создание контейнера
        {
            //Получение позиции, на которую нажали
            Vector3Int newContainerPos = Vector3Int.zero;
            if (spotSender)
                newContainerPos = spotSender.Position;
            else if (containerSender)
                newContainerPos = containerSender.Position;
            else
                return; //error

            //Вызов фабрики
            Container newContainer = containerFactory.CreateContainer(containerPrefab, containerRows[newContainerPos.x].ContainerSpots[newContainerPos.z], newContainerPos);

            //Еслм контейнер создан
            if (!newContainer) return;

            //Добавление созданного контйенра в правильную точку по найденной позиции
            containerRows[newContainerPos.x].ContainerSpots[newContainerPos.z].Containers.Add(newContainer);
        }
    }

    /// <summary>
    /// Метод, вызываемый при нажатии правой кнопки мыши
    /// </summary>
    /// <param name="obj">Коллайдер объекта, на который нажали правой кнопкой</param>
    private void RightMouseButtonClick(Collider obj)
    {
        //Для определения на какой объект нажали
        ContainerSpot spotSender = obj.GetComponent<ContainerSpot>();
        Container containerSender = obj.GetComponent<Container>();

        //Проверка на null
        //Если нажали ни на точку, ни на контейнер
        if (spotSender == containerSender) return;

        //Завершить все корутины, которые начали ряды
        //Для избежания бага анимации
        foreach (ContainerRow row in containerRows) 
        {
            row.StopAllCoroutines();
            row.DropRow();
        }

        if (inspectorMode) //Опустить ряд
        {
            //Получение позиции, на которую нажали
            Vector3Int newContainerPos = Vector3Int.zero;
            if (spotSender)
                newContainerPos = spotSender.Position;
            else if (containerSender)
                newContainerPos = containerSender.Position;
            else
                return; //error

            //Опусание ряда по найденной позиции
            containerRows[newContainerPos.x].DropRow();
        }
        else
        {
            //Получение позиции, на которую нажали
            Vector3Int newContainerPos = Vector3Int.zero;
            if (spotSender)
                newContainerPos = spotSender.Position;
            else if (containerSender)
                newContainerPos = containerSender.Position;
            else
                return; //error

            //Удаление контйенера по найденной позиции
            containerFactory.DeleteContainer(containerRows[newContainerPos.x].ContainerSpots[newContainerPos.z]);
        }
    }

    /// <summary>
    /// Переключение режима просмотра
    /// </summary>
    public void ToggleInspectorMode() 
    {
        inspectorMode = !inspectorMode;
        changeModeText.Invoke(inspectorMode);
    }
}

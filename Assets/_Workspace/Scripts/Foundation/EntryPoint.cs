using System.Linq;

public class EntryPoint
{    
    private ResourcesLoader _resourcesLoader;

    private Player _player;
    private MainCamera _mainCamera;
    private MainCanvas _mainCanvas;
    private OrderSystem _orderSystem;
    private Landspace _landspace;

    public EntryPoint(ResourcesLoader resourcesLoader)
    {
        _resourcesLoader = resourcesLoader;

        LoadResources();
        CreateInjector();
        Initialize();
        Subs();
        Start();
    }

    private void LoadResources()
    {
        _player = _resourcesLoader.GetResource<Player>(ResourcesList.Player);
        _mainCamera = _resourcesLoader.GetResource<MainCamera>(ResourcesList.PlayerCamera);
        _mainCanvas = _resourcesLoader.GetResource<MainCanvas>(ResourcesList.MainCanvas);
        _orderSystem = _resourcesLoader.GetResource<OrderSystem>(ResourcesList.OrderSystem);
        _landspace = _resourcesLoader.GetResource<Landspace>(ResourcesList.Landspace);
    }

    private void CreateInjector()
    {
        DIContainer.AddMonoBehaviour(_player.Pallet);
        DIContainer.AddMonoBehaviour(_player);
        DIContainer.AddMonoBehaviour(_mainCamera);
        DIContainer.AddMonoBehaviour(_mainCanvas.GetPanel<GamePanel>().JoyStick);
        DIContainer.AddMonoBehaviour(_mainCanvas.OrderCreator);
        DIContainer.AddMonoBehaviour(_landspace.BagContainer);

    }

    private void Initialize()
    {
        _player.Initialize();
        _mainCamera.Initialize();
        _orderSystem.Initialize();
        _mainCanvas.Initialize();
        _landspace.Initialize();
    }

    private void Subs()
    {
        _mainCanvas.GetButton<StartButton>().OnClick += () => GameStart();
    }

    private void Start()
    {
        for (int s = 0; s < _landspace.Stands.ToList().Count; s++)
        {
            Stand stand = _landspace.Stands.ToList()[s];

            for (int i = 0; i < stand.Capacity; i++)
            {
                Food food = _landspace.ShipmentCar.GetBox(stand.FoodVariant).GetFood();
                stand.SetFood(food);
                food.ResetPosition(false);
            }
        }
    }

    private void GameStart()
    {
        _mainCanvas.SmartphoneAnimation.Close(() => 
        { 
            _mainCanvas.GetPanel<GameContent>().Enable();
            _mainCanvas.GetPanel<MenuContent>().Disable();
            _mainCanvas.GetButton<SmartphoneButton>().Enable();

            _player.Movement.Enable();

            for (int i = 0; i < 9; i++)
            {
                _orderSystem.CreateOrder();
            }
        });
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class TapHandler : MonoBehaviour, IPointerClickHandler
{
    private Camera _camera;
    private RaycastHit _raycastHit;

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _raycastHit, 100f))
        {
            if (_raycastHit.transform.TryGetComponent(out Stand stand))
            {
                if (UpDownButton.Instance.B_Variant == UpDownButton.ButtonVariant.up)
                {
                    Food food = stand.GetFood();
                    Pallet pallet = TapInjector.GetMonoBehaviour<Pallet>();

                    if (pallet.AddItem(food))
                    {
                        food.ResetPosition(true);
                    }
                    else
                    {
                        stand.SetFood(food);

                        if (food != null)
                            food.ResetPosition(false);
                    }
                }
                else
                {
                    Pallet pallet = TapInjector.GetMonoBehaviour<Pallet>();
                    Food food = pallet.GetFood(stand.FoodVariant);

                    if (stand.SetFood(food))
                    {
                        food.ResetPosition(true);
                    }
                    else
                    {
                        pallet.AddItem(food);

                        if (food != null)
                            food.ResetPosition(false);
                    }
                }
            }
            else if (_raycastHit.transform.TryGetComponent(out ShipmentBox shipmentBox))
            {
                if (UpDownButton.Instance.B_Variant == UpDownButton.ButtonVariant.up)
                {
                    Food food = shipmentBox.GetFood();
                    Pallet pallet = TapInjector.GetMonoBehaviour<Pallet>();

                    if (pallet.AddItem(food))
                    {
                        food.ResetPosition(true);
                    }
                    else
                    {
                        food.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Pallet pallet = TapInjector.GetMonoBehaviour<Pallet>();
                    Food food = pallet.GetFood(shipmentBox.FoodVariant);

                    if (food != null)
                        shipmentBox.SetFood(food);
                }
            }
        }
    }

    //public void OnDrawGizmos()
    //{
    //    if (_camera == null)
    //        _camera = Camera.main;
    //    else
    //    {
    //        Gizmos.color = Color.red;
    //        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    //        Gizmos.DrawRay(ray.origin, ray.direction * 100f);
    //    }
    //}
}

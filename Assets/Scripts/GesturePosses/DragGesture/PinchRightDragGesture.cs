using UnityEngine;
using Leap;
using Leap.Unity;

public class PinchRightDragGesture : MonoBehaviour
{
    private Hand _hand;
    private Vector3 _previousPinchPosition;
    private bool _isPinching = false;
    private const float PinchStrengthThreshold = 0.8f;
    private const float DragThreshold = 0.05f;
    public GroupController groupController;

    void Update()
    {
        // Get the first hand detected
        _hand = Hands.Provider.CurrentFrame.Hands[0];

        if (_hand != null)
        {
            // Check if the hand is pinching with sufficient strength
            if (_hand.IsPinching() && _hand.PinchStrength > PinchStrengthThreshold)
            {
                Vector3 currentPinchPosition = _hand.GetPinchPosition();

                if (!_isPinching)
                {
                    // Start pinching
                    _isPinching = true;
                    _previousPinchPosition = currentPinchPosition;
                }
                else
                {
                    // Check for rightward drag with a threshold
                    if (currentPinchPosition.x - _previousPinchPosition.x > DragThreshold)
                    {
                        Debug.Log("Pinch Rightward Drag detected");
                        groupController.ShowGameOverPopup();
                    }

                    // Update previous pinch position
                    _previousPinchPosition = Vector3.Lerp(_previousPinchPosition, currentPinchPosition, Time.deltaTime * 10);
                }
            }
            else
            {
                // Reset pinch state
                _isPinching = false;
            }
        }
    }
}



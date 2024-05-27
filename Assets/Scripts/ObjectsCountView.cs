using UnityEngine;
using TMPro;

public class ObjectsCountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _creatingObjectsCountText;
    [SerializeField] private TMP_Text _activeObjectsCountText;

    private IObjectsCounter _objectsCounter; 

    private void OnDisable()
    {
        _objectsCounter.CreatingObjectsCountChanged -= OnCreatingObjectsCountChange;
        _objectsCounter.ActiveObjectsCountChanged -= OnActiveObjectsCountChange;
    }

    public void Initialize(IObjectsCounter objectsCounter)
    {
        _objectsCounter = objectsCounter;
        _creatingObjectsCountText.text = _objectsCounter.CreatingObjectsCount.ToString();
        _creatingObjectsCountText.text = _objectsCounter.ActiveObjectsCount.ToString();
        _objectsCounter.CreatingObjectsCountChanged += OnCreatingObjectsCountChange;
        _objectsCounter.ActiveObjectsCountChanged += OnActiveObjectsCountChange;
    }

    private void OnActiveObjectsCountChange(int count)
    {
        _activeObjectsCountText.text = count.ToString();
    }

    private void OnCreatingObjectsCountChange(int count)
    {
        _creatingObjectsCountText.text = count.ToString();
    }
}
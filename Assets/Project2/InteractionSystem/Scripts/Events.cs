using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    #region Static Declaration

    public static Events instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    [SerializeField] public UnityEvent onAnimationEnd = new UnityEvent();
}

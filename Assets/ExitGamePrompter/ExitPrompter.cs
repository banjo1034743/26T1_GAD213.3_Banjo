using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitPrompter : MonoBehaviour
{
    #region Variables

    [Header("Input System")]

    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _exitAction;

    [Header("Exit Prompt")]

    [SerializeField] private GameObject _exitPromptWindow;

    #endregion

    #region Methods

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void PromptExit()
    {
        if (_exitAction.IsPressed() == true)
        {
            Debug.Log("registered input");
            _exitPromptWindow.SetActive(true);
        }
    }

    private void InitialiseVariables()
    {
        _exitAction = _inputActions.FindAction("Player/Exit");
        
        if (_exitAction != null)
        {
            Debug.Log("ExitAction is not null");
        }

        _exitPromptWindow.SetActive(false);
    }

    #endregion

    #region Unity Methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseVariables();
    }

    private void Update()
    {
        PromptExit();
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.Disable();
    }

    #endregion
}

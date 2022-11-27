using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuBtnHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isLoginPage;
    public InputField UsernameInput;
    public InputField PasswordInput;
    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

    public void MainLevels()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }
    public void Credits()
    {
        SceneManager.LoadScene("FullCreditsMenu");
    }
    public void Login()
    {
        SceneManager.LoadScene("LoginMenu"); // Not functional
    }
    public void Backtomain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoginButton()
    {
        // NOTE TO SELF, ONLY EXECUTE THIS ON THE SIGNUP PAGE
        if(isLoginPage == true)
        {
            if(UsernameInput.text != "")
            {
                if(PasswordInput.text != "")
                {
                    WWWForm form = new WWWForm();
                    form.AddField("Username", UsernameInput.text);
                    form.AddField("Password", PasswordInput.text);

                    UnityWebRequest www = UnityWebRequest.Post("https://Interdimensional-Deathrun-API.cmbdeveloper.repl.co/Authentication/login.php", form);
                    Debug.Log(www);
                } else
                {
                    Debug.Log("Password field is invalid");
                }
            }
            else
            {
                Debug.Log("Username field is invalid");
            }
        }
    }
}

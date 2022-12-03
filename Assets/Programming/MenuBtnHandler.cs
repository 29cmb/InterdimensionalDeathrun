using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class MenuBtnHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Login Page Data")]
    public bool isLoginPage;
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Text LoginText;
    void Start()
    {
       
    }

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
    async public void LoginButton()
    {
        // NOTE TO SELF, ONLY EXECUTE THIS ON THE SIGNUP PAGE
        if(isLoginPage == true)
        {
            Debug.Log("IsLoginPage");
            if(UsernameInput.text != "")
            {
                if(PasswordInput.text != "")
                {
                    WWWForm form = new WWWForm();
                    form.AddField("Username", UsernameInput.text);
                    form.AddField("Password", PasswordInput.text);

                    using (UnityWebRequest www = UnityWebRequest.Post("https://interdimensional-deathrun-api.cmbdeveloper.repl.co/Authentication/login.php", form))
                    {
                        var r = www.SendWebRequest();
                        Debug.Log(www.result);
                        LoginText.text = "Logging in..";
                        await Task.Delay(1500);
                        if (www.result != UnityWebRequest.Result.Success)
                        {
                            Debug.Log(www.error);
                        }
                        else
                        {
                            string returned = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                            if(returned == "2")
                            {
                                if(UnityEngine.SystemInfo.deviceUniqueIdentifier == "4d26d7861d93f87cd759750076467c388f335a42")
                                {
                                    Debug.Log("User is allowed to log into the developer account.");
                                    LoginText.text = "User is now logged in as Developer";
                                }
                            } else if(returned == "0")
                            {
                                LoginText.text = "Invalid username or password";
                            } else
                            {
                                LoginText.text = "Logging in...";
                                Debug.Log(returned);
                                BinaryFormatter bf = new BinaryFormatter();
                                FileStream file = File.Create(Application.persistentDataPath
                                             + "/IDDataStore.dat");
                                SaveData data = new SaveData();
                                data.Username = UsernameInput.text;
                                data.EncodedPassword = returned;
                                bf.Serialize(file, data);
                                file.Close();
                                LoginText.text = "Successfully logged in as " + UsernameInput.text;


                            }
                        }
                    }
                } else
                {
                    Debug.Log("Password field is invalid");
                }
            }
            else
            {
                Debug.Log("Username field is invalid");
            }
        } else
        {
            Debug.Log("IsNotLoginPage");
        }
    }
    public void Signup()
    {
        Application.OpenURL("https://interdimensional-deathrun-api.cmbdeveloper.repl.co/Signup/index.php");
    }
}

[Serializable]
class SaveData
{
    public string Username;
    public string EncodedPassword;
}

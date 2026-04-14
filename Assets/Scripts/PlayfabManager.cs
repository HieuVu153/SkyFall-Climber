using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public ChangeInput changeInput;

    // ===== REGISTER =====
    public void RegisterButton()
    {
        Debug.Log("CLICK REGISTER");

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Đăng ký thành công! Hãy đăng nhập lại.";

        emailInput.text = "";
        passwordInput.text = "";

        changeInput.ResetInput(); // 👈 gọi ở đây
    }

    // ===== LOGIN =====
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Đăng nhập thành công!";
        Debug.Log("Login OK");

        // chuyển scene sau khi login
        SceneManager.LoadScene("Map"); // tên scene của bạn
    }

    // ===== RESET PASSWORD =====
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = PlayFabSettings.TitleId // dùng TitleId hiện tại
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Đã gửi email reset mật khẩu!";
    }

    // ===== ERROR =====
    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
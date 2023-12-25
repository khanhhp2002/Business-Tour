using Photon.Pun;
using TMPro;
using UnityEngine;

public class NamePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_Text _errorText;
    private string _specialCharacters = "!@#$%^&*()_+{}|:\"<>?~`-=[]\\;',./";


    public void Start()
    {
        _nameInputField.onSubmit.AddListener(OnNameSubmitted);
    }

    private void OnNameSubmitted(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            _errorText.text = "Name cannot be empty.";
            return;
        }
        else if (name.Length > 12 || name.Length < 3)
        {
            _errorText.text = "Name cannot be less than 3 or more than 12 characters.";
            return;
        }
        else if (name.Contains(" "))
        {
            _errorText.text = "Name cannot contain spaces.";
            return;
        }
        foreach (char c in name)
        {
            if (_specialCharacters.Contains(c.ToString()))
            {
                _errorText.text = "Name cannot contain special characters.";
                return;
            }
        }
        PlayerPrefs.SetString("PlayerName", name);
        PhotonNetwork.NickName = name;
        gameObject.SetActive(false);
    }
}

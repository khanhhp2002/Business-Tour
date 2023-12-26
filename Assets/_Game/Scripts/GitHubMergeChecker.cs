using UnityEngine;
using UnityEditor;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine.Networking;
using System.Collections;

public class GitHubMergeChecker : MonoBehaviour
{
    private const string githubToken = "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";
    private const string repositoryOwner = "khanhhp2002";
    private const string repositoryName = "Business-Tour";

    private const float checkIntervalInSeconds = 300f; // Check every 5 minutes

    private void OnEnable()
    {
        StartCoroutine(CheckForNewMergesCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheckForNewMergesCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(CheckForNewMerges());

            // Wait for the next check interval
            yield return new WaitForSeconds(checkIntervalInSeconds);
        }
    }

    private IEnumerator CheckForNewMerges()
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", githubToken);

            // Example API endpoint to get pull requests
            string apiUrl = $"https://api.github.com/repos/{repositoryOwner}/{repositoryName}/pulls";

            UnityWebRequest request = UnityWebRequest.Get(apiUrl);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseData = request.downloadHandler.text;
                // Parse the responseData and check for new merges
                // You may need to implement JSON parsing or use a library like JsonUtility
                Debug.Log(responseData);

                // Display notification if there are new merges
                if (HasNewMerges(responseData))
                {
                    EditorUtility.DisplayDialog("GitHub Notification", "There are new merges. Please sync your work.", "OK");
                }
            }
            else
            {
                Debug.LogError($"GitHub API request failed: {request.result} - {request.error}");
            }
        }
    }

    private bool HasNewMerges(string responseData)
    {
        Debug.Log("HasNewMerges");
        // Implement logic to determine if there are new merges
        // Parse the response data and compare with the previous state
        // Return true if there are new merges, otherwise false
        return true; // Placeholder value
    }


}

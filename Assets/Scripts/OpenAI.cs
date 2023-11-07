using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UnityLibrary
{
    public class OpenAI : MonoBehaviour
    {
        [Tooltip("URL to the OpenAI chat completions API endpoint")]
        const string url = "https://api.openai.com/v1/chat/completions";

        [Tooltip("The language model to use for generating text")]
        public string modelName = "gpt-3.5-turbo";

        [Tooltip("Input field for entering the user message")]
        public InputField inputPrompt;

        [Tooltip("Input field for displaying the assistant's response")]
        public InputField inputResults;

        [Tooltip("Game object for displaying a loading icon")]
        public GameObject loadingIcon;

        [Tooltip("Path to the file where you want to save the API response. Does not save chat calls.")]
        public string FilePath = "C:/Path/to/your/file.txt";

        // The OpenAI API key
        string apiKey = "";

        // A flag indicating if the chat process is running
        bool isRunning = false;

        // Load the API key when the script starts
        void Start()
        {
            LoadAPIKey();
            StartCoroutine(GetModelInformation());
        }

        // Execute a chat with the OpenAI API
        public void Execute()
        {
            // If a chat process is already running, log an error and return
            if (isRunning)
            {
                Debug.LogError("Already running");
                return;
            }
            isRunning = true;
            loadingIcon.SetActive(true);

            // Create a list of initial messages for the chat
            List<Message> messages = new List<Message>()
            {
                new Message { role = "system", content = "You are a helpful assistant."},
                new Message { role = "user", content = inputPrompt.text }
            };

            // Create the data for the API request
            RequestDataChat requestData = new RequestDataChat()
            {
                model = modelName,
                messages = messages.ToArray(),
                temperature = 0.7f,
                max_tokens = 256,
                top_p = 1,
                frequency_penalty = 0,
                presence_penalty = 0
            };

            // Convert the request data to JSON
            string jsonData = JsonUtility.ToJson(requestData);

            // Convert the JSON string to a byte array
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

            // Create a Unity web request with the API URL and JSON data
            UnityWebRequest request = UnityWebRequest.Post(url, jsonData);

            // Set the upload and download handlers and headers of the request
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            // Send the request and handle the result asynchronously
            UnityWebRequestAsyncOperation async = request.SendWebRequest();

            async.completed += (op) =>
            {
                // If there was a connection error, log the error
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError(request.error);
                }
                else
                {
                    // If the request was successful, log the response text and parse it to get the generated text
                    Debug.Log(request.downloadHandler.text);
                    APIResponse responseData = JsonUtility.FromJson<APIResponse>(request.downloadHandler.text);
                    string generatedText = responseData.choices[0].message.content;

                    // Display the generated text in the results input field
                    inputResults.text = generatedText;
                }

                // Hide the loading icon and set the running flag to false
                loadingIcon.SetActive(false);
                isRunning = false;
            };
        } // Execute

        private IEnumerator GetModelInformation()
        {
            string uri = "https://api.openai.com/v1/models";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    WriteToFile(FilePath, webRequest.downloadHandler.text);
                }
            }
        }

        private void WriteToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
                Debug.Log("Successfully written to file: " + filePath);
            }
            catch (IOException e)
            {
                Debug.Log("Failed to write to file: " + e.Message);
            }
        }

        void LoadAPIKey()
        {
            var keypath = Path.Combine(Application.streamingAssetsPath, "secretkey.txt");
            if (File.Exists(keypath) == false)
            {
                Debug.LogError("Apikey missing: " + keypath);
            }
            apiKey = File.ReadAllText(keypath).Trim();
            Debug.Log("API key loaded, len= " + apiKey.Length);
        }
    }
}

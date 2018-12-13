using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TwitchChat : MonoBehaviour
{
    private HandleTextFile textFileHandler;
    private string path;
    private string textFile;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    private string username, password, channelName;

    //public Text chatBox;
    //public Rigidbody player;
    //public int speed;
    //public GameObject content;
    //public List<string> chatMessages;

    // Use this for initialization
    void Start()
    {
        textFileHandler = FindObjectOfType<HandleTextFile>();
        path = "Assets/Resources/profileInformation.txt";
        textFile = textFileHandler.ReadString(path);
        //chatMessages.Insert(0, "Chat:");
        ReadTextFile();
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (!twitchClient.Connected || twitchClient == null)
        {
            Connect();
        }

        ReadChat();
    }

    private void ReadTextFile()
    {
        if (textFile.Contains("username:"))
        {
            var splitPoint = textFile.IndexOf("username:", 1);
            var endPoint = textFile.IndexOf(".", splitPoint);
            username = textFile.Substring(splitPoint + 10, endPoint - (splitPoint + 10));
        }

        if (textFile.Contains("password:"))
        {
            var splitPoint = textFile.IndexOf("password:", 1);
            var endPoint = textFile.IndexOf(".", splitPoint);
            password = textFile.Substring(splitPoint + 10, endPoint - (splitPoint + 10));
        }

        if (textFile.Contains("channelName:"))
        {
            var splitPoint = textFile.IndexOf("channelName:", 1);
            var endPoint = textFile.IndexOf(".", splitPoint);
            channelName = textFile.Substring(splitPoint + 13, endPoint - (splitPoint + 13));
        }
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();
    }

    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                /*chatMessages.Insert(1, String.Format("{0}: {1}", chatName, message));
                if (chatMessages.Count > 8)
                {
                    chatMessages.RemoveAt(8);
                }
                chatBox.text = null;
                for (int i = 0; i < chatMessages.Count; i++)
                {
                    chatBox.text = chatBox.text += chatMessages[i] + "\n";
                }*/
                //chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);

                ChatCommands(message);
            }
        }
    }

    private void ChatCommands(string ChatInputs)
    {
    }
}

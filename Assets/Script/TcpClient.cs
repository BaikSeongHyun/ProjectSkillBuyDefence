﻿using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class TcpClient
{
    class AsyncData
    {
        public Socket clientSock;
        public const int msgMaxLength = 1024;
        public byte[] msg = new byte[msgMaxLength];
        public int msgLength;
    }

    public delegate void OnReceivedEvent(byte[] msg, int size);

    public event OnReceivedEvent OnReceived;

    private Socket m_clientSock = null;
    private AsyncCallback asyncReceiveCallback;
    private string m_strIP;
    private int m_port;

    public TcpClient()
    {
        // create socket
        m_clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        asyncReceiveCallback = new AsyncCallback(HandleAsyncReceive);
    }

    public void Setup(string ip, int port)
    {
        if (m_clientSock.Connected)
            return;

        m_strIP = ip;
        m_port = port;
    }

    public bool Connect()
    {
        if (m_clientSock.Connected)
            return false;

        // connect server
        try
        {
            m_clientSock.Connect(new IPEndPoint(IPAddress.Parse(m_strIP), m_port));
        }
        catch(SocketException e)
        {            
            Debug.Log("TCPClient::Connect() : Connect Fail" + (int)e.SocketErrorCode);
            return false;
        }

        // begin receive
        AsyncData asyncData = new AsyncData();
        asyncData.clientSock = m_clientSock;

        try {
            m_clientSock.BeginReceive(asyncData.msg, 0, AsyncData.msgMaxLength, SocketFlags.None, asyncReceiveCallback, asyncData);
            
        }
        catch {
            Debug.Log("TCPClient::Connect() : BeginReceive 예외 발생");
            m_clientSock.Close();
            return false;
        }

        return true;
    }
    public void DisConnect()
    {
        if (m_clientSock == null)
            return;
        try
        {
            m_clientSock.Close();
        }
        catch
        {

        }
    }
    private void HandleAsyncReceive(IAsyncResult asyncResult)
    {
        AsyncData asyncData = (AsyncData)asyncResult.AsyncState;
        Socket clientSock = asyncData.clientSock;
        
        try
        {
            asyncData.msgLength = clientSock.EndReceive(asyncResult);                               
        }
        catch
        {
            Debug.Log("TCPClient::HandleAsyncReceive() : EndReceive - 예외");
            clientSock.Close();
            return;
        }
        if(OnReceived != null)
        {
            OnReceived(asyncData.msg, asyncData.msgLength);
        }        

        try {
            clientSock.BeginReceive(asyncData.msg, 0, AsyncData.msgMaxLength, SocketFlags.None, asyncReceiveCallback, asyncData);
        }
        catch {
            Debug.Log("TCPClient::HandleAsyncReceive() : BeginReceive - 예외");
            clientSock.Close();
        }
    }

    public int Send(byte[] data, int size)
    {
        try
        {
            return m_clientSock.Send(data, size, SocketFlags.None);
        }
        catch
        {
            Debug.Log("Send() : Send - 예외");
        }
        return -1;
    }
}

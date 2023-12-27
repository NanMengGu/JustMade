using System;
using UnityEngine;
using System.Threading;

public class CreateMessageBoxTrigger : MonoBehaviour
{
    public string caption;
    public string text;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            Thread messageBoxThread = new Thread(() => Win32API.MessageBox(IntPtr.Zero, text, caption, 0));
            messageBoxThread.Start();
        }
    }
}
using System;
using UnityEngine;

namespace Uitility
{
    public static class MyLogger
    {
        public static void Log(string message)
        {
            Debug.Log($"[{DateTime.Now:s}]:{message}");
        }
    }
}
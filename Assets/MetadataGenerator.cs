using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AudicaTools;
using UnityEngine;

public class MetadataGenerator : MonoBehaviour
{
    string targetPath = String.Empty;
    
    void Awake()
    {
        Application.runInBackground = true;
        try
        {
            //Get console arguments
            string[] args = Environment.GetCommandLineArgs();
            //Parse arguments
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-path")
                {
                    if (i + 1 < args.Length)
                    {
                        //Debug.Log("Received a command line argument");
                        //Console.WriteLine("Received a command line argument");
                        targetPath = args[i + 1];
                    }
                }
            }
            Console.WriteLine($"Searching for files in {targetPath}. Found file: {File.Exists(targetPath)}. Audica file hash: {(new Audica(targetPath).GetHashedSongID())}");
            
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            Application.Quit();
        }
    }
}

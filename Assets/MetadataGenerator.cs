using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AudicaTools;
using UnityEngine;

public class MetadataGenerator : MonoBehaviour
{
    List<string> targetPaths = new List<string>();
    
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
                    if (i + 1 < args.Length) //We have a single path
                    {
                        for (int k = 1; i + k < args.Length; k++)
                        {
                            targetPaths.Add(args[i + k]);
                        }
                    }
                }
            }
            foreach (var targetPath in targetPaths)
            {
                if (!File.Exists(targetPath))
                {
                    Debug.LogError($"{Path.GetFileName(targetPath)} does not exist");
                    continue;
                }

                var audica = new Audica(targetPath);
                var ratings = audica.GetDifficultyRatings();
                Console.WriteLine($"{Path.GetFileName(targetPath)}," +
                                  $"{audica.GetHashedSongID()}," +
                                  $"{(ratings.expert == null ? string.Empty : ratings.expert.difficultyRating.ToString())}" +
                                  $"{(ratings.advanced == null ? string.Empty : ratings.advanced.difficultyRating.ToString())}" +
                                  $"{(ratings.standard == null ? string.Empty : ratings.standard.difficultyRating.ToString())}" +
                                  $"{(ratings.beginner == null ? string.Empty : ratings.advanced.difficultyRating.ToString())}" +
                                  $"");
            }
            
            
            
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

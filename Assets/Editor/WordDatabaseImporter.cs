using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class WordDatabaseImporter
{
    [MenuItem("Tools/Import Word Categories")]
    public static void Import()
    {
        string txtFolder = "Assets/Data/WordLists";
        string assetFolder = "Assets/Data/Categories";

        string[] files = Directory.GetFiles(txtFolder, "*.txt");

        foreach (string file in files)
        {
            string categoryName =
                Path.GetFileNameWithoutExtension(file);

            string assetPath =
                $"{assetFolder}/{categoryName}.asset";

            WordCategorySO category =
                AssetDatabase.LoadAssetAtPath<WordCategorySO>(assetPath);

            if (category == null)
            {
                category = ScriptableObject.CreateInstance<WordCategorySO>();

                category.CategoryName = categoryName;

                AssetDatabase.CreateAsset(category, assetPath);
            }

            category.Words.Clear();

            HashSet<string> unique = new();

            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                string word =
                    line.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (!unique.Add(word))
                    continue;

                category.Words.Add(new WordData(word));
            }

            EditorUtility.SetDirty(category);

            Debug.Log($"{categoryName}: Imported {category.Words.Count} words.");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Word import complete!");
    }
}
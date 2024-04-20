using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

using System.Linq;
using System.Collections.Generic;

public class ScriptReferenceFinder : EditorWindow
{
    private string searchText = "";
    private List<string> scripts = new List<string>();
    private Vector2 scrollPosition;
    private int selectedIndex = -1;
    private bool needToUpdateSearchText = false;

    [MenuItem("Tools/Script Reference Finder with Autocomplete")]
    public static void ShowWindow()
    {
        GetWindow<ScriptReferenceFinder>("Script Reference Finder with Autocomplete");
    }

    void OnEnable()
    {
        // Load all scripts in the project
        string[] allScripts = AssetDatabase.GetAllAssetPaths()
            .Where(path => path.EndsWith(".cs"))
            .ToArray();

        foreach (string scriptPath in allScripts)
        {
            string scriptName = System.IO.Path.GetFileNameWithoutExtension(scriptPath);
            if (!scripts.Contains(scriptName))
            {
                scripts.Add(scriptName);
            }
        }

        scripts.Sort();
    }

    void OnGUI()
    {
        GUILayout.Label("Find References in Prefabs and Scenes", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        searchText = EditorGUILayout.TextField("Search Script", searchText);
        if (EditorGUI.EndChangeCheck())
        {
            // Reset selection when user types
            selectedIndex = -1; 
            UpdateFilteredScripts();
        }

        if (selectedIndex >= 0 && selectedIndex < scripts.Count && needToUpdateSearchText)
        {
            searchText = scripts[selectedIndex];
            needToUpdateSearchText = false;

            // Remove focus from the selection grid
            GUI.FocusControl(null);
        }

        if (!string.IsNullOrEmpty(searchText))
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));
            int newSelectedIndex = GUILayout.SelectionGrid(selectedIndex, scripts.ToArray(), 1);
            if (newSelectedIndex != selectedIndex)
            {
                selectedIndex = newSelectedIndex;
                needToUpdateSearchText = true;
            }
            EditorGUILayout.EndScrollView();
        }

        if (GUILayout.Button("Find References in Prefabs"))
        {
            FindReferencesInPrefabs(searchText);
        }

        if (GUILayout.Button("Find References in Scenes"))
        {
            FindReferencesInScenes(searchText);
        }
    }

    private void UpdateFilteredScripts()
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            scripts.Clear();
        }
        else
        {
            scripts = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".cs") && System.IO.Path.GetFileNameWithoutExtension(path).ToLower().Contains(searchText.ToLower()))
                .Select(path => System.IO.Path.GetFileNameWithoutExtension(path))
                .Distinct()
                .ToList();
        }
    }

    private void FindReferencesInPrefabs(string scriptName)
    {
        string[] allPrefabs = AssetDatabase.GetAllAssetPaths();
        List<string> prefabsWithScript = new List<string>();

        foreach (string prefabPath in allPrefabs)
        {
            if (prefabPath.EndsWith(".prefab"))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                if (prefab != null)
                {
                    var components = prefab.GetComponentsInChildren<Component>(true);
                    foreach (var component in components)
                    {
                        if (component != null && component.GetType().ToString().Contains(scriptName))
                        {
                            prefabsWithScript.Add(prefabPath);
                            break;
                        }
                    }
                }
            }
        }

        Debug.Log($"Found {prefabsWithScript.Count} prefabs with {scriptName}:");

        foreach (string path in prefabsWithScript)
        {
            Debug.Log(path);
        }
    }

    private void FindReferencesInScenes(string scriptName)
    {
        List<string> scenesWithScript = new List<string>();
        foreach (string scenePath in EditorBuildSettings.scenes.Select(s => s.path))
        {
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            var gameObjects = scene.GetRootGameObjects();

            foreach (var go in gameObjects)
            {
                var components = go.GetComponentsInChildren<Component>(true);
                foreach (var component in components)
                {
                    if (component != null && component.GetType().ToString().Contains(scriptName))
                    {
                        scenesWithScript.Add(scenePath);
                        break;
                    }
                }
            }
        }

        Debug.Log($"Found {scenesWithScript.Count} scenes with {scriptName}:");

        foreach (string path in scenesWithScript)
        {
            Debug.Log(path);
        }
    }
}

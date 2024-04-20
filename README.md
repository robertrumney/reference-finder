# Reference Finder

Reference Finder is a Unity Editor tool that enables developers to efficiently search for script references across prefabs and scenes, incorporating an autocomplete feature for enhanced usability.

## Installation

To install Reference Finder:

1. **Download the Repository**: Clone or download this repository to your local machine.
2. **Add to Unity**: Copy the script `ScriptReferenceFinder.cs` into your Unity project's `Assets/Editor` directory. Unity will automatically compile and recognize the new editor tool.

## Usage

Once installed, you can access the Reference Finder tool via the Unity Editor menu:

1. Navigate to `Tools -> Script Reference Finder with Autocomplete` in the Unity menu bar.
2. In the opened window, type the name of the script you're searching for in the "Search Script" field. The tool will suggest scripts based on your input.
3. Select a script from the autocomplete suggestions or continue typing.
4. Click on `Find References in Prefabs` to search for references within all prefabs or `Find References in Scenes` to search within all loaded scenes.
5. The tool will display paths to the prefabs or scenes that contain the specified script in the Unity console.

## Features

- **Autocomplete Search**: Quickly find the script you need with autocomplete suggestions as you type.
- **Search in Prefabs**: Locate all prefabs in your project that use a specific script.
- **Search in Scenes**: Find all scenes where a specific script is used, ideal for debugging and refactoring.
- **User-Friendly Interface**: Simple and intuitive interface, making it easy to start using without extensive documentation.

## Potential Uses

- **Refactoring**: Easily find and replace deprecated scripts across your project.
- **Debugging**: Quickly locate components that might be causing issues in specific scenes or prefabs.
- **Optimization**: Identify and analyze script usage to optimize performance.
- **Documentation**: Assist in generating documentation by providing an overview of where scripts are used in the project.

## Contributing

Contributions are welcome. Please feel free to fork this repository, make changes, and submit pull requests. You can also open issues if you find bugs or have feature requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

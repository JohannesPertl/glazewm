# GlazeWM &middot;

GlazeWM is a tiling window manager for Windows inspired by i3 and Polybar.

Why use a tiling window manager? A tiling WM lets you easily organize windows and adjust their layout on the fly. Spend less time moving and resizing windows by using keyboard-driven commands.

Under the hood, the Windows API is used to position windows via P/Invoke. The codebase is organized using a 3-tier architecture and uses some DDD/CQRS patterns.

![demo](https://user-images.githubusercontent.com/34844898/142960922-fb3abd0d-082c-4f92-8613-865c68006bd8.gif)

# Download

The latest runnable binary can be downloaded via [releases](https://github.com/lars-berger/GlazeWM/releases).

Alternatively, to build from source, use the .NET CLI command `dotnet publish ./GlazeWM.Bootstrapper/GlazeWM.Bootstrapper.csproj --configuration=Release --runtime=win-x64 --output=. -p:PublishSingleFile=true` and run `GlazeWM.exe` from the compiled output. Other available runtime identifiers can be found [here](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#windows-rids).

# Roadmap

- Improve handling of fullscreen and maximized windows.
- More bar components.
- Avoid managing windows that can't be moved/resized (eg. windows running with elevated priveleges).
- Change the size of sibling windows when a tiling window is resized with the sizing border.

# Configuration

The configuration file for GlazeWM can be found at `C:\Users\<YOUR_USER>\.glaze-wm\config.yaml`. If this file doesn't exist, it can optionally be generated with some sensible defaults on application launch.

## Keybindings

The available keybindings can be customized via the `keybindings` property in the config file. A keybinding consists of one or more key combinations and one or more commands to run when pressed.

A full list of keys that can be used for keybindings can be found [here](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-5.0#fields). Numbers can be used in keybindings with and without a `D` prefix (eg. either `D1` or `1` works).

It's recommended to use the alt key for keybindings. The windows key is unfortunately a pain to remap, since certain keybindings (eg. `LWin+L`) are reserved by the OS.

```yaml
keybindings:
  # Command to run. Use "commands" to specify an array of commands to run in sequence.
  - command: "focus workspace 1"

    # Key combination to trigger the keybinding. Use "bindings" to provide an array of key
    # combinations that can trigger the keybinding.
    binding: "Alt+1"
```

### Default keybindings

Keybindings with Alt pressed:

![Alt key pressed - with keybindings](https://user-images.githubusercontent.com/34844898/142961268-6892c447-8cfb-4774-916b-1859e168f383.png)

Keybindings with Alt+Shift pressed:

![Alt+shift key pressed - with keybindings](https://user-images.githubusercontent.com/34844898/142961277-04627f5d-b257-4c66-a9b3-fde3f485eded.png)

Apart from the `Alt+Shift+E` binding for exiting GlazeWM, it's also possibly to safely exit via the system tray icon.

## Gap configuration

The gaps between windows can be changed via the `gaps` property in the config file. Inner and outer gaps are set separately.

```yaml
gaps:
  # Gap between adjacent windows.
  inner_gap: 20

  # Gap between windows and the screen edge.
  outer_gap: 20
```

## Workspaces configuration

Workspaces need to be predefined via the `workspaces` property in the config file. A workspace is automatically assigned to each monitor on startup.

```yaml
workspaces:
  # Uniquely identifies the workspace and is used as the label for the workspace in the bar.
  - name: 1
```

## Bar configuration

The appearance of the bar can be changed via the `bar` property in the config file.

```yaml
bar:
  # Height of the bar in pixels.
  height: 30

  # Opacity value between 0.0 and 1.0.
  opacity: 1.0

  # Background color of the bar.
  background: "#101010"

  # Default font color. Can be overriden by setting `foreground` in a component's config.
  foreground: "white"

  # Default font family. Can be overriden by setting `font_family` in a component's config.
  font_family: "Segoe UI"

  # Default font size. Can be overriden by setting `font_size` in a component's config.
  font_size: "13"

  # Horizontal and vertical borders in pixels. Borders are inside the dimensions of the bar and do
  # not affect bar height. See "Shorthand properties" for more info.
  border_width: "0"

  # Color of the border.
  border_color: "blue"

  # Horizontal and vertical spacing between components within the bar and the edges of the bar. See
  # "Shorthand properties" for more info.
  padding: "1 6 1 6"

  # Components to display on the left side of the bar.
  components_left:
    - type: "workspaces"

  # Components to display on the right side of the bar.
  components_right:
    - type: "clock"
```

### Bar component configuration

Bar components have some properties that can be changed regardless of the component type.

```yaml
# Type of component to display. Currently only 2 component types exist:  "workspaces" and "clock".
type: <COMPONENT_TYPE>

# Horizontal and vertical margins. See "Shorthand properties" for more info.
margin: "0 10 0 0"

# Horizontal and vertical padding. See "Shorthand properties" for more info.
padding: "0"

# Opacity value between 0.0 and 1.0.
opacity: 1.0

# Background color of the component.
background: "#101010"

# Font color used within the component.
foreground: "white"

# Font family used within the component.
font_family: "Segoe UI"

# Font family used within the component.
font_size: "13"

# Horizontal and vertical borders in pixels. Borders are inside the dimensions of the component.
# See "Shorthand properties" for more info.
border_width: "0"

# Color of the border.
border_color: "blue"
```

### Shorthand properties

Properties related to the edges of the bar or a component, like `padding`, `margin`, and `border_width`, use a 1-to-4 value syntax. This is the same convention that's common in CSS, for those familiar with that.

Using the example of padding:

- When one value is specified, it applies the same padding to all four sides.
- When two values are specified, the first padding applies to the top and bottom, the second to the left and right.
- When three values are specified, the first padding applies to the top, the second to the right and left, the third to the bottom.
- When four values are specified, the paddings apply to the top, right, bottom, and left in that order (clockwise).

# Known issues

## Blurry buttons in bar window

An app called "Sonic Studio", which is installed by default on ASUS ROG machines can cause rendering issues with WPF apps. This can be resolved by disabling `NahimicService` in Windows Services Manager.
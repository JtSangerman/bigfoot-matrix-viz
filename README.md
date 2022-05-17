# BIGFOOT.viz
An exploratory projected created from the inspiration found in a desire of the ability to program & explore visualizations of the concepts behind our universe's various problem-solving computing algorithms -- with the goal to aid an avenue to deeper understanding of our reality's fundamental and explicit limits of procedural complexity -- through the everyday algorithms built with and bound by these underlying limits.

Although the end product of this project does not presently exist as entirely clear, the project currently serves and will continue to serve as a source of inspiring curiosity of the our universe's complex tasks, and a tool for curating and programming visualizations of such a path as you go.

## Purpose statement
Assuming you've read this far into this brief introduction, my hope is that you've arrived here from acting on any inspirations or curiosities that have found you -- and that `BIGFOOT.viz` is able to present itself as a tool to aid in exploring that interest. Given this project and driving codebase *is currently a passion project that is exclusively worked on with leisure time that is far-and-few between*, many areas of the repo's scope were developed with the aim to require as little time as possible -- and thus areas needing improvement are present and to be expected. 

Feel free to make improvements as you see fit, build new or improve existing features as inspired, or find your stroke of curiousity elsewhere in a way that inspires a purpose -- using this project as a tool or otherwise.

Forks/branches/PRs or alike are all encouraged. Feedback and contributions of any variety are always warmly welcomed. Your interest in this BIGFOOT's source code and the project overall is deeply appreciated!

# BIGFOOT: Repository intro
A repository for the project source code behind a programmable HUB75D LED panel and the dependencies: microcontroller hardware drivers, a supporting hardware development emulator and the various hardware supported algorithm visualizations.

This repo houses the two separate, but related code bases: the firmware source code for the panel's hardware & microcontroller and the source code behind the development emulator application.

# BIGFOOT codebases:

### *The firmware codebase source* 
The panel hardware's firmware and controller source code scope. This setup runs an environment dependent on a Linux box hosted by a rpi as the panel CPU/controller. On boot, the build is configured to indefinitely cycle through the designated alg visuals interfaced using the panel hardware. Within is the C library for driving the addressable panel diodes complete with wrapper bindings to the C# and Python languages.

### *The development emulator codebase source* 
The panel hardware emulator application project is written with .NET supported by Visual Studio -- serving the purpose of allowing for efficient development of algorithm visuals, panel drivers, and anything other code that will be deployed and ran on the hardware and supporting environment. 

The value this emulator application yields is in how it serves to eliminate executing the tedious & required hardware deployment procedure in the event ANY code changes are made. The emulator's value is defined by the way it enables the ability to add code changes, develop new builds, explore ideas, etc... all contained within your everyday development machine. 

This eliminates the need for constant time consuming hardware deployments -- allowing for an efficient testing, debugging and most importantly, a streamed-lined development environment.

# POC Hardware Used:

### Supported low-level circuit interfacing: 
**HUB75 interfaced to rpi GPIO** 

Example circuit diagram below
![HUB75 Example](https://github.com/JtSangerman/BIGFOOT.RGBMatrix/blob/app.dev/assets/hub75_circuit_example_128x64.png "HUB75 example circuit diagram")

### Wiring config:
![HUB75 to GPIO](https://github.com/JtSangerman/BIGFOOT.RGBMatrix/blob/app.dev/assets/wiring_diagram_rpi-40pin.png "HUB75 to GPIO interfacing config")

### Supported panel microcontroller

- rpi running linux or *nix-like
- 3b+ model has been used for development with no issues    
- *nix dependencies: 
    - `sudo` rights
    - installation `screen` (ref: https://www.gnu.org/software/screen/)

### Observed panel power consumption: 

- *5mA @ 5v* per fully-engaged diode
- Derivation & assumptions: 
    - `32 · 32` panel `(m · n)`
    - `D(x, y): ∀(x, y) ∈ (m x n)` where `D(x, y)` represents the diode at panel matrix coordinates `x, y` holds value `rgba(255, 255, 255, 1)`, ie, is set to full blast
    - Rated for *5v* 
    - Observed max load current draw of *5.12a @ 5v*

# Starred directories[^1]

- `/hardware/*`: Rpi controller unit source code. Contains startup scripts (for linux/*nix based microcontroller environments), low-level led controller drivers and algorithm visualizations code.
- `/hardware/lib/*`: Driver code for the LED compute unit.
- `/hardware/bindings/c#/examples/*`: The directory for C# based algorithm visualizations to be ran on the deployed hardware build. *NOTE this directory is named poorly and is not truly reflective of the contents or purpose.

--

- `/src/*`: Source for the panel hardware emulator: drivers, interfacing, etc.
- `/src/Visuals/*`: Algorithm visualizations source. Current convention follows as each visualization existing in its own VS solution project. Each of these VS solution projects exist in this parent directory. Visuals can be efficiently developed here through the emulator and deployed to the target hardware directory once ready.
- `/src/BIGFOOT.RGBMatrix/MatrixTypes/*`: Graphical interfacing for visuals (currently supports DirectX graphics rendering and console based graphic types).
- `/src/BIGFOOT.RGBMatrix/*`: Virtual emulator application entry point.

# Starred resources[^1]

- `/hardware/bindings/c#/examples/Makefile`: Config file responsible for packaging visualization source `*.cs` files to hardware executable `.exe` files. This file will need to compensate for any new visualizations added to the target hardware deploy directory.
- `/hardware/bindings/c#/examples/alg-viz.sh`: Startup executed bash script responsible for indefinitely cycling through configured visualization executables and executing them on the panel hardware. *NOTE ideally this script should exist in a dedicated 'solution-scripts' directory.
- `/hardware/bindings/c#/examples/*.cs|*.python`: existing visualizations, mostly written in C#. There also currently exists a rudimentary Python Snake AI viz in this directory (named as `simple_square.py` due to a lazy hack stemming from a py-compile resource script that explicitly references this file name).


[^1]: If navigating the project's directory structure and underlying resources, please proceed knowing that the directory structure is not organized properly or meaningfully in places.

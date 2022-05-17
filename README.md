# BIGFOOT.viz
An exploratory projected created from the inspiration found in a desire of the ability to program & explore visualizations of the concepts behind our universe's various problem-solving computing algorithms -- with the goal to aid an avenue to deeper understanding of our reality's fundamental and explicit limits of procedural complexity -- through the everyday algorithms built with and bound by these underlying limits.

Although the end product of this project does not presently exist as entirely clear, the project currently serves and will continue to serve as a source of inspiring curiosity of the our universe's complex tasks, and a tool for curating and programming visualizations of such a path as you go.

Assuming you've read this far into this brief introduction, I sincerely hope you've found yourself driven to act on any curiosities and inspiration leads. Given this project and driving codebase *is currently a passion project that is exclusively worked on with leisure time that is far-and-few between*, many areas of the repo's scope were developed with the aim to require as little time as possible -- and thus areas needing improvement are present and to be expected. Feel free to make improvements as you see fit, build new or improve existing features as inspired, or find your stroke of curiousity elsewhere in a way that inspires the desire of it to be explored -- using this project as a tool or otherwise.

# BIGFOOT: Repository intro
A repository for the project source code behind a programmable HUB75D LED panel and the dependencies: microcontroller hardware drivers, a supporting hardware development emulator and the various hardware supported algorithm visualizations.

This repo houses the two separate, but related code bases: the firmware source code for the panel's hardware & microcontroller and the source code behind the development emulator application.

DEVELOPER's NOTE => *Given this is a passion project only worked on with leisure time far-and-few between*, many areas of this repo's scope -- from the project/repo/development environment structure, coding style/patterns/practices, runtime complexities, hardware configurations, etc -- to the codebase's existing algorithm visualizations -- all inherit an endless room for improvement and further development. Feel free to take it upon yourself to find any avenues of interest within the project to drive contributions for further development or any scope that can be elegantly improved in any capacity. Forks/branches/PRs or alike are all encouraged. Feedback and contributions of all kind are valued. Your interest in this BIGFOOT's source code and the project overall is deeply appreciated!

# BIGFOOT codebases:
*The firmware codebase* is dependent on an Linux box hosted by a rpi. On boot, the build is configured to indefinitely cycle through the designated alg visuals interfaced using the panel hardware. Within is the C library for driving the addressable panel diodes complete with wrapper bindings to the C# and Python languages.

*The development emulator source* project is written with .NET supported by Visual Studio -- serving the purpose of allowing for efficient development of algorithm visuals, panel drivers, and anything other code that will be deployed and ran on the hardware and supporting environment. The value of this emulator application is to eliminate executing the tedious & required hardware deployment procedure in the event ANY code changes are made. The emulator's value is defined by the way it enables the ability to add code changes, develop new builds, explore ideas, etc... all contained within your everyday development machine. This eliminates the need for constant time consuming hardware deployments -- allowing for an efficient testing, debugging and most importantly, a streamed-lined development environment.

# POC Hardware:
Supported low-level circuit interfacing: HUB75D

Wiring config: [img src="./assets/wiring_diagram_rpi-40pin.png"]

Supported panel microcontroller: 
    - rpi running linux or *nix-like
    - 3b+ model has been used for development with no issues
    - *nix dependencies: `sudo` rights, `screen` (ref: https://www.gnu.org/software/screen/)

Observed panel power consumption: 
    - *5mA @ 5v* per fully-engaged diode
    - Derivation & assumptions: 
    -- `32·32` panel (`m·n`)
    -- `D(x,y) = rgba(255, 255, 255, 1)` \forall `(x, y) \in (m \times n)` where `D(x, y)` represents `rgba(255, 255, 255, 1)`, ie the diode at panel matrix coordinates `x, y` set to full blast
    -- Rated for *5v* 
    -- Observed max load current draw of *5.12a @ 5v*

# Starred directories
`/hardware/*`: Rpi controller unit source code. Contains startup scripts (for linux/*nix based microcontroller environments), low-level led controller drivers and algorithm visualizations code.
`/hardware/lib/*`: Driver code for the LED compute unit.
`/hardware/bindings/c#/examples/*`: The directory for C# based algorithm visualizations to be ran on the deployed hardware build. *NOTE this directory is named poorly and is not truly reflective of the contents or purpose.

`/src/*`: Source for the panel hardware emulator: drivers, interfacing, etc.
`/src/Visuals/*`: Algorithm visualizations source. Current convention follows as each visualization existing in its own VS solution project. Each of these VS solution projects exist in this parent directory. Visuals can be efficiently developed here through the emulator and deployed to the target hardware directory once ready.
`/src/BIGFOOT.RGBMatrix/*`: Virtual emulator and entry point.

# Starred resources
`/hardware/bindings/c#/examples/Makefile`: Config file responsible for packaging visualization source `*.cs` files to hardware executable `.exe` files. This file will need to compensate for any new visualizations added to the target hardware deploy directory.
`/hardware/bindings/c#/examples/alg-viz.sh`: Startup executed bash script responsible for indefinitely cycling through configured visualization executables and executing them on the panel hardware. *NOTE ideally this script should exist in a dedicated 'solution-scripts' directory.
`/hardware/bindings/c#/examples/*.cs|*.python`: existing visualizations, mostly written in C#. There also currently exists a rudimentary Python Snake AI viz in this directory (named as `simple_square.py` due to a lazy hack stemming from a py-compile resource script that explicitly references this file name).


*NOTE => If navigating the project's directory structure, please proceed knowing that the directory structure is not organized properly or meaningfully in places.
# BIGFOOT.viz
An exploratory projected created from the inspiration found in a desire of the ability to program & explore visualizations of the concepts behind our universe's various problem-solving computing algorithms -- with the goal to aid an avenue to deeper understanding of our reality's fundamental and explicit limits of procedural complexity -- through the everyday algorithms built with and bound by these underlying limits.

`BIGFOOT.viz` provides an abstracted library to intuitively target and control specific LED diodes in a RGB Matrix panel with ease, allowing for the visualization of ideas that can be represented abstactly with a matrix and of course, __time__. Operations are handled asynchronously in real-time, complete with offscreen and V-sync buffer controls.

## Included repository source code summary
A repository for the source code behind this project's engine of a programmable HUB75D LED panel and dependencies: microcontroller, hardware drivers, a supporting hardware development emulator and the various hardware supported algorithm visualizations. A POC/prototype hardware & supporting firmware environment build explanation included below.

Feel free to make improvements as you see fit, build new or improve existing features as inspired, or find your stroke of curiousity elsewhere in a way that inspires a purpose -- using this project as a tool or otherwise.

Forks/branches/PRs or alike are all encouraged. Feedback and contributions of any variety are always warmly welcomed. Your interest in this BIGFOOT's source code and the project overall is deeply appreciated!

# BIGFOOT Repository layout
This repo houses the two separate, but related code bases: 
1. [The firmware source code](/hardware) for the panel's hardware and microcontroller
2. [The emulator and visualizations source code](/src) behind the emulator application and current visuals.


# BIGFOOT.viz Hardware Environment POC/Prototype Example:

### Supported low-level circuit interfacing: 
- **HUB75 interfaced to rpi GPIO**
---------------------------------

Example hub75 control circuit for added context:
![HUB75 Example](https://github.com/JtSangerman/BIGFOOT.RGBMatrix/blob/app.dev/assets/hub75_circuit_example_128x64.png "HUB75 example circuit diagram")

### Wiring config:
![HUB75 to GPIO](https://github.com/JtSangerman/BIGFOOT.RGBMatrix/blob/app.dev/assets/wiring_diagram_rpi-40pin.png "HUB75 to GPIO interfacing config")

### Supported panel microcontroller

- rpi running linux or *nix-like
- 3b+ model has been used for development with no issues    
- *nix dependencies: 
    - `sudo` rights
    - `screen` installed (ref: https://www.gnu.org/software/screen/)

### Observed panel power consumption: 

- *5mA @ 5v* per fully-engaged diode
- Derivation & assumptions: 
    - `32 · 32` panel `(m · n)`
    - `D(x, y): ∀(x, y) ∈ (m x n)` where `D(x, y)` represents the diode at panel matrix coordinates `x, y` holds value `rgba(255, 255, 255, 1)`, ie, is set to full blast
    - Rated for *5v* 
    - Observed max load current draw of *5.12a @ 5v*

## See BIGFOOT.viz hardware prototype in action
https://imgur.com/a/etemK6w

[^1]: If navigating the project's directory structure and underlying resources, please proceed knowing that the directory structure is not organized properly or meaningfully in places.

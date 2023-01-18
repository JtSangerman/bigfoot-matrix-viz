# The firmware codebase source
The panel hardware's firmware and controller source code scope. This setup runs an environment dependent on a Linux box hosted by a rpi as the panel CPU/controller. On boot, the build is configured to indefinitely cycle through the designated alg visuals interfaced using the panel hardware. The emulator's code is designed as an interface layer to emulate this library's interactions with the hardware.

This repo's `/hardware/*` source contains the external C library for driving the addressable panel diodes complete with wrapper bindings to the C# and Python languages: [(c) Henner Zeller's LED panel library <h.zeller@acm.org>](https://github.com/hzeller/rpi-rgb-led-matrix), licensed with 
[GNU General Public License Version 2.0](http://www.gnu.org/licenses/gpl-2.0.txt)
(which means, if you use it in a product somewhere, you need to make the
source and all your modifications available to the receiver of such product so
that they have the freedom to adapt and improve).

## Discourse discussion group

If you'd like help, please do not file a bug, use the discussion board instead:
https://rpi-rgb-led-matrix.discourse.group/


# Starred directories[^1]

- `/hardware/*`: Rpi controller unit source code. Contains startup scripts (for linux/*nix based microcontroller environments), low-level led controller drivers and algorithm visualizations code.
- `/hardware/lib/*`: Driver code for the LED compute unit.
- `/hardware/bindings/c#/examples/*`: The directory for C# based algorithm visualizations to be ran on the deployed hardware build. *NOTE this directory is named poorly and is not truly reflective of the contents or purpose.

[^1]: If navigating the project's directory structure and underlying resources, please proceed knowing that the directory structure is not organized properly or meaningfully in places.


# Starred resources[^1]

- `/hardware/bindings/c#/examples/Makefile`: Config file responsible for packaging visualization source `*.cs` files to hardware executable `.exe` files. This file will need to compensate for any new visualizations added to the target hardware deploy directory.
- `/hardware/bindings/c#/examples/alg-viz.sh`: Startup executed bash script responsible for indefinitely cycling through configured visualization executables and executing them on the panel hardware. *NOTE ideally this script should exist in a dedicated 'solution-scripts' directory.
- `/hardware/bindings/c#/examples/*.cs|*.python`: existing visualizations, mostly written in C#. There also currently exists a rudimentary Python Snake AI viz in this directory (named as `simple_square.py` due to a lazy hack stemming from a py-compile resource script that explicitly references this file name).


# Controlling RGB LED display with Raspberry Pi GPIO
==================================================

A library to control commonly available 64x64, 32x32 or 16x32 RGB LED panels
with the Raspberry Pi. Can support PWM up to 11Bit per channel, providing
true 24bpp color with CIE1931 profile.

Supports 3 chains with many panels each on a regular Pi.
On a Raspberry Pi 2 or 3, you can easily chain 12 panels in that chain
(so 36 panels total), but you can theoretically stretch that to up
to 96-ish panels (32 chain length) and still reach
around 100Hz refresh rate with full 24Bit color (theoretical - never tested
this; there might likely be timing problems with the panels that will creep
up then).



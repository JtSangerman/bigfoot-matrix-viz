# *The development emulator codebase source* 
The panel hardware emulator application project is written with .NET supported by Visual Studio -- serving the purpose of allowing for efficient development of algorithm visuals, panel drivers, and anything other code that will be deployed and ran on the hardware and supporting environment. 

The value this emulator application yields is in how it serves to eliminate executing the tedious & required hardware deployment procedure in the event ANY code changes are made. The emulator's value is defined by the way it enables the ability to add code changes, develop new builds, explore ideas, etc... all contained within your everyday development machine. 

This eliminates the need for constant time consuming hardware deployments -- allowing for an efficient testing, debugging and most importantly, a streamed-lined development environment.

Currently supports a DirectX rendering and rudimentary console graphics interfacing.

# To run:
1. Open in VS
2. Set `\src\BIGFOOT.RGBMatrix` as the startup project
3. Point the `main` entry point at `\src\BIGFOOT.RGBMatrixEntryPoints\Program.cs`
4. Enjoy

Highly encouraged: program and share your own visualizations

# Starred directories[^1]

- `/src/*`: Source for the panel hardware emulator: drivers, interfacing, etc.
- `/src/Visuals/*`: Algorithm visualizations source. Current convention follows as each visualization existing in its own VS solution project. Each of these VS solution projects exist in this parent directory. Visuals can be efficiently developed here through the emulator and deployed to the target hardware directory once ready.
- `/src/BIGFOOT.RGBMatrix/MatrixTypes/*`: Graphical interfacing for visuals (currently supports DirectX graphics rendering and console based graphic types).
- `/src/BIGFOOT.RGBMatrix/*`: Virtual emulator application entry point.

[^1]: If navigating the project's directory structure and underlying resources, please proceed knowing that the directory structure is not organized properly or meaningfully in places.
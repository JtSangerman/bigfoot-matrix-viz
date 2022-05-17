
echo "STARTING BASH SCRIPT"
while :
do
        echo "sudo mono bubble.exe 60"
        sudo mono bubble.exe 60
        
        echo "sudo mono maze.exe 15 15 1"
        sudo mono maze.exe 15 15 1
        
        echo "sudo mono selection.exe 60"
        sudo mono selection.exe 60

        # RUDIMENTARY SNAKE AI 
        echo "sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py"  # can't identify what references the 'simple-square.py' file name
        sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py         # as a result, the snake viz code lives in this erroneously named file
done











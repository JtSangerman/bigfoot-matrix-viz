
echo "STARTING BASH SCRIPT"
while :
do
        echo "sudo mono bubble.exe 60"
        sudo mono bubble.exe 60
        echo "sudo mono maze.exe 15 15 1"
        sudo mono maze.exe 15 15 1
        echo "sudo mono selection.exe 60"
        sudo mono selection.exe 60

        echo "sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py"
        sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py

	echo "sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py"
        sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py

        echo "sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py"
        sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py

#	echo "sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py"
#        sudo /home/pi/Source/rpi-rgb-led-matrix/bindings/python/samples/simple-square.py --snakeversion "1"

done











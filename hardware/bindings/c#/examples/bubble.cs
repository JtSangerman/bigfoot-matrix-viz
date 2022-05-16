using rpi_rgb_led_matrix_sharp;
using System;
using System.Threading;

namespace minimal_example
{
    class Program
    {
      static void bubbleSort(int []arr, int sleepMs)
      {
          var matrix= new RGBLedMatrix(32, 1, 1);           
          var canvas = matrix.CreateOffscreenCanvas();
          int n = arr.Length;
          for (int i = 0; i < n - 1; i++){
              for (int j = 0; j < n - i - 1; j++)
                  if (arr[j] > arr[j + 1])
                  {
                      // swap temp and arr[i]
                      int temp = arr[j];
                      arr[j] = arr[j + 1];
                      arr[j + 1] = temp;

		      canvas.Clear();
                      for (int k = 0; k < n; k++){
                        canvas.DrawLine(k, 0, k, arr[k]-1, k == j+1 ? new Color(123, 0, 0) : k < n - i ? new Color(123, 123, 123) : new Color(0,0,123));
                        Console.WriteLine($"Drawing line ({k}, {arr[k]-1}: x0={k}, y0={0}, x1={k}, y1={arr[k]-1}");
		      }
                      canvas = matrix.SwapOnVsync(canvas);
		     	print(arr);
			Thread.Sleep(sleepMs);		     
                  }
            }
canvas.Clear();
for (int k = 0; k < arr.Length; k++){
                        canvas.DrawLine(k, 0, k, arr[k]-1, new Color(0,0,123));
                      }
                      canvas = matrix.SwapOnVsync(canvas);

      }
	
	static void print(int []arr){
		  foreach(var v in arr){
              	  	 Console.Write($"{v} ");
            	  }
			
                  Console.WriteLine("");
	}

	static void shuffle(int []array){
		Random rng = new Random();
		int n = array.Length;
        	while (n > 1) 
        	{
            		int k = rng.Next(n--);
            		int temp = array[n];
            		array[n] = array[k];
            		array[k] = temp;
        	}
	
	}

       static int Main(string[] args)
        {
	
            var arr = new int[]{1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32};
	    shuffle(arr);
		int sleepMs=100;

	   
	    
 	    if (args.Length > 0)
		sleepMs=Int32.Parse(args[0]);
 
           bubbleSort(arr, sleepMs);
	    Thread.Sleep(7500);

	
            return 0; 
        }
    }
}

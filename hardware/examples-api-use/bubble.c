// C program for implementation of Bubble sort
#include <stdio.h>
#include "../include/led-matrix-c.h"
#include <string.h>
#include <unistd.h>


void swap(int *xp, int *yp)
{
	int temp = *xp;
	*xp = *yp;
	*yp = temp;
}

// A function to implement bubble sort
void bubbleSort(struct LedCanvas *canvas, int arr[], int n)
{
int i, j, k;
for (i = 0; i < n-1; i++)	

	// Last i elements are already in place
	for (j = 0; j < n-i-1; j++)
		if (arr[j] > arr[j+1]){
			swap(&arr[j], &arr[j+1]);
            for (k = 0; k < n; k++){
              draw_line(canvas, k, 0, k, arr[k], 150, 150, 150);
            }
        }
    
    
}

/* Function to print an array */
void printArray(int arr[], int size)
{
	int i;
	for (i=0; i < size; i++)
		printf("%d ", arr[i]);
	printf("\n");
}

// Driver program to test above functions
int main(int argc, char **argv)
{	
  struct RGBLedMatrixOptions options;
  struct RGBLedMatrix *matrix;
  struct LedCanvas *offscreen_canvas;
  int width, height;
  int x, y, i;

  memset(&options, 0, sizeof(options));
  options.rows = 32;
  options.chain_length = 1;

  /* This supports all the led commandline options. Try --led-help */
  matrix = led_matrix_create_from_options(&options, &argc, &argv);
  if (matrix == NULL)
    return 1;

  /* Let's do an example with double-buffering. We create one extra
   * buffer onto which we draw, which is then swapped on each refresh.
   * This is typically a good aproach for animations and such.
   */
  offscreen_canvas = led_matrix_create_offscreen_canvas(matrix);

  led_canvas_get_size(offscreen_canvas, &width, &height);
  int arr[] = {30, 26, 18, 6, 28, 29, 20, 25, 15, 4, 11, 23, 5, 31, 22, 7, 21, 8, 16, 10, 13, 1, 12, 2, 9, 17, 19, 14, 3, 27, 24, 32};
  int n = sizeof(arr)/sizeof(arr[0]);
  bubbleSort(offscreen_canvas, arr, n);
  printf("Sorted array: \n");
  printArray(arr, n);
  return 0;
}

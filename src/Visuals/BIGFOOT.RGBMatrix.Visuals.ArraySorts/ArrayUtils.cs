﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.ArraySorts
{
    public static class ArrayUtils
    {
        /// <summary>method <c>CreateShuffledSequential</c> returns a SHUFFLED array of values [1, 2, ..., inclusiveMax] .</summary>
        public static int[] CreateShuffledSequential(int inclusiveMax)
        {
            var array = Enumerable.Range(1, 32).ToArray();
            Random rng = new Random();

            var n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            return array;
        }
    }
}

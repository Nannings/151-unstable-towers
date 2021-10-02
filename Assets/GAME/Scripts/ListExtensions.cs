using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GAME
{
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffle this list
        /// </summary>
        public static void Shuffle<T>(this List<T> thisList, Random RandomNumberGenerator)
        {
            for (int i = thisList.Count - 1; i >= 0; i--)
            {
                int j = RandomNumberGenerator.Next(0, i);
                T tmp = thisList[i];
                thisList[i] = thisList[j];
                thisList[j] = tmp;
            }
        }

        /// <summary>
        /// Return a shuffled copy of this list (leaves this list as it was)
        /// </summary>
        public static List<T> ShuffleAndCopy<T>(this List<T> thisList, Random RandomNumberGenerator)
        {
            T[] shuffled = new T[thisList.Count];
            thisList.CopyTo(shuffled);
            for (int i = shuffled.Count() - 1; i >= 0; i--)
            {
                int j = RandomNumberGenerator.Next(0, i);
                T tmp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = tmp;
            }

            return shuffled.ToList();
        }

        /// <summary>
        /// Shuffle this list
        /// </summary>
        public static void Shuffle<T>(this List<T> thisList)
        {
            Random RandomNumberGenerator = new Random();

            for (int i = thisList.Count - 1; i >= 0; i--)
            {
                int j = RandomNumberGenerator.Next(0, i);
                T tmp = thisList[i];
                thisList[i] = thisList[j];
                thisList[j] = tmp;
            }

        }

        /// <summary>
        /// Return a shuffled copy of this list (leaves this list as it was)
        /// </summary>
        public static List<T> ShuffleAndCopy<T>(this List<T> thisList)
        {
            Random RandomNumberGenerator = new Random();

            T[] shuffled = new T[thisList.Count];
            thisList.CopyTo(shuffled);
            for (int i = shuffled.Count() - 1; i >= 0; i--)
            {
                int j = RandomNumberGenerator.Next(0, i);
                T tmp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = tmp;
            }

            return shuffled.ToList();
        }


    }

}
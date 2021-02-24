using System;
using static System.Console;

namespace MergeSortDemo
{
	class Program
	{
		static void Main()
		{
			Random rnd = new();

			var size = 10000;
			int[] listOriginal = new int[size];

			WriteLine($"Arrays have {size} elements");

			while (true)
			{
				// Populate the list
				for (int j = 0; j < size; j++)
				{
					listOriginal[j] = rnd.Next(0, int.MaxValue);
				}

				Sort(listOriginal, 1, size);
				WriteLine(CheckSort(listOriginal, size));
				WriteLine();
				WriteLine("Press ENTER to sort a new array.");
				ReadLine();
			}
		}

		/// <summary>
		/// Checks if the array is sorted correctly.
		/// </summary>
		/// <param name="toCheck">The array to check.</param>
		/// <param name="length">The length of the array.</param>
		/// <returns></returns>
		static bool CheckSort(int[] toCheck, int length)
		{
			int[] array = new int[length];
			Array.Copy(toCheck, array, length);

			// Default C# sort.
			Array.Sort(array);

			// Compare sorted arrays.
			for (int i = 0; i < length; i++)
			{
				if (toCheck[i] != array[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Uses 1-based indexing.
		/// </summary>
		/// <param name="A">The array to sort.</param>
		/// <param name="p">Start (inclusive) index of the sort range.</param>
		/// <param name="r">End (inclusive) index of the sort range.</param>
		static void Sort(int[] A, int p, int r)
		{
			if (p < r)
			{
				int q = (p + r) / 2;

				Sort(A, p, q);
				Sort(A, q + 1, r);

				Merge(A, p, q, r);
			}
		}

		/// <summary>
		/// Merges two sorted ranges.
		/// </summary>
		/// <param name="A">The array that is being sorted.</param>
		/// <param name="p">Start (inclusive) index of the left sorted range.</param>
		/// <param name="q">End (inclusive) index of the left sorted range.</param>
		/// <param name="r">End (inclusive) index of the right sorted range.</param>
		static void Merge(int[] A, int p, int q, int r)
		{
			// convert to 0-based indexing.
			p--;
			q--;
			r--;

			// For better performance (3-4% faster, measured with random 10000 and 10000000 items arrays),
			// do 2 items merge like this:
			if (p == q)
			{
				if (A[r] < A[p])
				{
					var temp = A[r];
					A[r] = A[p];
					A[p] = temp;
				}

				return;
			}

			// For more than 2 items, merge using sorted ranges/arrays
			// Left range = [p, q]; right range = [q+1, r].

			int leftLength = q - p + 1;
			int rightLength = r - q;
			int[] leftArray = new int[q - p + 1];
			int[] rightArray = new int[rightLength];

			Array.Copy(A, p, leftArray, 0, leftLength);
			Array.Copy(A, q + 1, rightArray, 0, rightLength);

			int left = 0;
			int right = 0;

			for (int current = p; current <= r; current++)
			{
				if (left == leftLength)
				{
					// Reached the end of the left range.
					// Use the value from the right range.
					A[current] = rightArray[right];
					right++;
				}
				else if (right == rightLength)
				{
					// Reached the end of the right range.
					// Use the value from the left range.
					A[current] = leftArray[left];
					left++;
				}
				else if (leftArray[left] <= rightArray[right])
				{
					A[current] = leftArray[left];
					left++;
				}
				else
				{
					A[current] = rightArray[right];
					right++;
				}
			}
		}
	}
}

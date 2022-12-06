﻿using System.Collections.Generic;

namespace WindowsFormsApp2
{
    public static class Extension
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
        {
            var queue = new Queue<T>();

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (queue.Count == count)
                    {
                        do
                        {
                            yield return queue.Dequeue();
                            queue.Enqueue(e.Current);
                        } while (e.MoveNext());
                    }
                    else
                    {
                        queue.Enqueue(e.Current);
                    }
                }
            }
        }
    }
}

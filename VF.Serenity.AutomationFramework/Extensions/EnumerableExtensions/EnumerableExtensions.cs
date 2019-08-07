using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VF.Serenity.AutomationFramework.Extensions.EnumerableExtensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the first element in a sequence that satisfies a specified condition.
        /// </summary>
        /// 
        /// <returns>
        /// The first element in the sequence that passes the test in the specified predicate function.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable"/> to return an element from.</param><param name="predicate">A function to test each element for a condition.</param><typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception><exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.-or-The source sequence is empty.</exception>
        public static TSource FirstWithException<TSource>(this IEnumerable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            var result = source.FirstOrDefault(predicate.Compile());
            if (!EqualityComparer<TSource>.Default.Equals(result, default(TSource))) return result;
            var bodyExpression = SimplifyBodyExpression.Simplify(predicate).ToString();
            throw new InvalidOperationException($"We cannot find value where {bodyExpression}");
        }

        /// <summary>
        /// Returns the first element in a sequence.
        /// </summary>
        /// 
        /// <returns>
        /// The first element in the sequence.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable"/> to return an element from.</param>
        /// <param name="errorMessage">Error Message which appear when nothing found</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="source"/> </exception><exception cref="T:System.InvalidOperationException">or-The source sequence is empty.</exception>
        public static TSource First<TSource>(this IEnumerable<TSource> source, string errorMessage) =>
            source.First(null, errorMessage);

        /// <summary>
        /// Returns the first element in a sequence that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate,
            string errorMessage)
        {
            var result = predicate == null ? source.FirstOrDefault() : source.FirstOrDefault(predicate);
            if (!EqualityComparer<TSource>.Default.Equals(result, default(TSource))) return result;
            throw new InvalidOperationException($"{errorMessage}");
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) => source == null || !source.Any();

        public static bool IsNotEmpty<TSource>(this IEnumerable<TSource> source) => !IsEmpty(source);

        public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T obj in sequence)
                action(obj);
        }

        public static IList<TOut> Cast<TIn, TOut>(this IEnumerable<TIn> arr) =>
            arr.Select(x => (TOut) Convert.ChangeType(x, typeof(TOut))).ToArray();
    }
}

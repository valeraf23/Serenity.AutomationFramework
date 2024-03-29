using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VF.Serenity.AutomationFramework.Extensions.EnumerableExtensions
{
    /// <summary>
    /// In a Linq Expression body use the value of a variable instead of a reference to it
    /// </summary>
    public static class SimplifyBodyExpression
    {
        /// <summary>
        /// Use the value of a variable instead of a reference to it
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<TIn, TOut>> Simplify<TIn, TOut>(Expression<Func<TIn, TOut>> expression)
        {
            return (Expression<Func<TIn, TOut>>) PartialEval(expression);
        }

        /// <summary>
        /// Performs evaluation & replacement of independent sub-trees
        /// </summary>
        /// <param name='expression'>The root of the expression tree.</param>
        /// <param name='fnCanBeEvaluated'>A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        private static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            return new SubtreeEvaluator(new Nominator(fnCanBeEvaluated).Nominate(expression)).Eval(expression);
        }

        /// <summary>
        /// Performs evaluation & replacement of independent sub-trees
        /// </summary>
        /// <param name='expression'>The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>

        private static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, CanBeEvaluatedLocally);
        }

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        /// <summary>
        /// Evaluates & replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        private class SubtreeEvaluator : ExpressionVisitor
        {
            private readonly HashSet<Expression> _candidates;

            internal SubtreeEvaluator(HashSet<Expression> candidates)
            {
                _candidates = candidates;
            }

            internal Expression Eval(Expression exp)
            {
                return Visit(exp);
            }

            public override Expression Visit(Expression exp)
            {
                if (exp == null)
                {
                    return null;
                }
                return _candidates.Contains(exp) ? Evaluate(exp) : base.Visit(exp);
            }

            private static Expression Evaluate(Expression e)
            {
                if (e.NodeType == ExpressionType.Constant)
                {
                    return e;
                }
                LambdaExpression lambda = Expression.Lambda(e);
                Delegate fn = lambda.Compile();
                return Expression.Constant(fn.DynamicInvoke(null), e.Type);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly be part of an evaluated sub-tree.
        /// </summary>
        private class Nominator : ExpressionVisitor
        {
            private readonly Func<Expression, bool> _fnCanBeEvaluated;
            private HashSet<Expression> _candidates;
            private bool _cannotBeEvaluated;

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                _fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                _candidates = new HashSet<Expression>();
                Visit(expression);
                return _candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression == null) return null;
                bool saveCannotBeEvaluated = _cannotBeEvaluated;

                _cannotBeEvaluated = false;

                base.Visit(expression);

                if (!_cannotBeEvaluated)
                {

                    if (_fnCanBeEvaluated(expression))
                    {
                        _candidates.Add(expression);
                    }

                    else
                    {
                        _cannotBeEvaluated = true;

                    }
                }

                _cannotBeEvaluated |= saveCannotBeEvaluated;

                return expression;
            }
        }
    }
}
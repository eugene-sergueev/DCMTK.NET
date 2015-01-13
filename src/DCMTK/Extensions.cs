using System;
using System.Linq.Expressions;
using DCMTK.Fluent;

namespace DCMTK
{
    public static class Extensions
    {
        public static TCommandBuilder Set<TCommandBuilder, T>(this TCommandBuilder commandBuilder, Expression<Func<TCommandBuilder, T>> expression, T value) where TCommandBuilder : ICommandBuilder
        {
            // re-write in .NET 4.0 as a "set"
            var member = (MemberExpression)expression.Body;
            var param = Expression.Parameter(typeof(T), "value");
            var set = Expression.Lambda<Action<TCommandBuilder, T>>(
                Expression.Assign(member, param), expression.Parameters[0], param);

            // compile it
            var action = set.Compile();
            action(commandBuilder, value);

            return commandBuilder;
        }
    }
}

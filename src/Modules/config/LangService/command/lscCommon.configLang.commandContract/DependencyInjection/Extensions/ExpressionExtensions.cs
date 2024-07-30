using System.Linq.Expressions;
using System.Reflection;

namespace System
{
    /// <summary>
    /// Extensions for "Expression" class
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Get property name as string from expression 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string? GetPropertyName(this Expression expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            if (memberExpression != null)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                return propertyInfo.Name;
            }

            return null;
        }
    }
}

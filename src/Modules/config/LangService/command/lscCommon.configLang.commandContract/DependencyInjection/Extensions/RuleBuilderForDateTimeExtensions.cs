using lscCommon.configLang.commandContract.Shared;
using lscCommon.configLang.commandContract.Validators;
using System.Globalization;

namespace System
{
    public static class RuleBuilderForNumericExtensions
    {
        /// <summary>
        /// Create rule that this property must be greater than an int value
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="value">Value to be compared</param>
        /// <param name="message">Message attached. If not, will use default message</param>
        /// <returns></returns>
        public static RuleBuilder<int> GreaterThan(this RuleBuilder<int> ruleBuilder, int value, string? message = null)
        {
            var property = ruleBuilder.Property;
            var msgArgs = new List<MessageArgs>
            {
                new(Args.PROPERTY_NAME, ruleBuilder.PropertyName),
                new(Args.COMPARISION_VALUE, value.ToString())
            };
            message = message ?? MessConst.GREATER_THAN.FillArgs(msgArgs);
            var rule = new Rule<int>(x => x > value, property, message);
            ruleBuilder.AddRule(rule);
            return ruleBuilder;
        }

        /// <summary>
        /// Create rule that this property must be greater than or equal an int value
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="value">Value to be compared</param>
        /// <param name="message">Message attached. If not, will use default message</param>
        /// <returns></returns>
        public static RuleBuilder<int> GreaterThanOrEqual(this RuleBuilder<int> ruleBuilder,
                                                          int value,
                                                          string? message = null)
        {
            var property = ruleBuilder.Property;
            var msgArgs = new List<MessageArgs>
            {
                new(Args.PROPERTY_NAME, ruleBuilder.PropertyName),
                new(Args.COMPARISION_VALUE, value.ToString())
            };
            message = message ?? MessConst.GREATER_THAN.FillArgs(msgArgs);
            var rule = new Rule<int>(x => x > value, property, message);
            ruleBuilder.AddRule(rule);
            return ruleBuilder;
        }

        /// <summary>
        /// Create rule that this property must be lower than an int value
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="value">Value to be compared</param>
        /// <param name="message">Message attached. If not, will use default message</param>
        /// <returns></returns>
        public static RuleBuilder<int> LowerThan(this RuleBuilder<int> ruleBuilder, int value, string? message = null)
        {
            var property = ruleBuilder.Property;
            var msgArgs = new List<MessageArgs>
            {
                new(Args.PROPERTY_NAME, ruleBuilder.PropertyName),
                new(Args.COMPARISION_VALUE, value.ToString())
            };
            message = message ?? MessConst.GREATER_THAN.FillArgs(msgArgs);
            var rule = new Rule<int>(x => x < value, property, message);
            ruleBuilder.AddRule(rule);
            return ruleBuilder;
        }

        /// <summary>
        /// Create rule that this property must be lower than or equal an int value
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="value">Value to be compared</param>
        /// <param name="message">Message attached. If not, will use default message</param>
        /// <returns></returns>
        public static RuleBuilder<int> LowerThanOrEqual(this RuleBuilder<int> ruleBuilder,
                                                        int value,
                                                        string? message = null)
        {
            var property = ruleBuilder.Property;
            var msgArgs = new List<MessageArgs>
            {
                new(Args.PROPERTY_NAME, ruleBuilder.PropertyName),
                new(Args.COMPARISION_VALUE, value.ToString())
            };
            message = message ?? MessConst.GREATER_THAN.FillArgs(msgArgs);
            var rule = new Rule<int>(x => x <= value, property, message);
            ruleBuilder.AddRule(rule);
            return ruleBuilder;
        }

        /// <summary>
        /// Create rule that this property must be exclusive between 2 int value
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="minValue">First int value</param>
        /// <param name="maxValue">Second int value</param>
        /// <param name="message">Message attached. If not, will use default message</param>
        /// <returns></returns>
        public static RuleBuilder<int> ExclusiveBetween(this RuleBuilder<int> ruleBuilder,
                                                        int minValue,
                                                        int maxValue,
                                                        string? message = null)
        {
            var msgArgs = new List<MessageArgs>
            {
                new(Args.PROPERTY_NAME, ruleBuilder.PropertyName),
                new(Args.MIN_VALUE, minValue.ToString(CultureInfo.InvariantCulture)),
                new(Args.MAX_VALUE, maxValue.ToString(CultureInfo.InvariantCulture))
            };
            message = message ?? MessConst.MUST_EXCLUSIVE_FROM.FillArgs(msgArgs);
            var rule = new Rule<int>(x => x >= maxValue || x <= minValue, ruleBuilder.Property, message);
            ruleBuilder.AddRule(rule);
            return ruleBuilder;
        }

        /// <summary>
        /// Create rule that this property must be inclusive between 2 int value
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="minValue">First int value</param>
        /// <param name="maxValue">Second int value</param>
        /// <param name="message">Message attached. If not, will use default message</param>
        /// <returns></returns>
        public static RuleBuilder<int> InclusiveBetween(this RuleBuilder<int> ruleBuilder,
                                                        int minValue,
                                                        int maxValue,
                                                        string? message = null)
        {
            var msgArgs = new List<MessageArgs>
            {
                new(Args.PROPERTY_NAME, ruleBuilder.PropertyName),
                new(Args.MIN_VALUE, minValue.ToString(CultureInfo.InvariantCulture)),
                new(Args.MAX_VALUE, maxValue.ToString(CultureInfo.InvariantCulture))
            };
            message = message ?? MessConst.MUST_INCLUSIVE_FROM.FillArgs(msgArgs);
            var rule = new Rule<int>(x => x <= maxValue && x >= minValue, ruleBuilder.Property, message);
            ruleBuilder.AddRule(rule);
            return ruleBuilder;
        }
    }
}
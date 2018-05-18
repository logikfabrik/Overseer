// <copyright file="DataBindingActionConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Linq;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="DataBindingActionConfigurator" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/33005816, answered by torvin, https://stackoverflow.com/users/332528/torvin.
    /// </remarks>
    public static class DataBindingActionConfigurator
    {
        /// <summary>
        /// Configures data binding.
        /// </summary>
        public static void Configure()
        {
            var getTargetMethod = ActionMessage.GetTargetMethod;

            ActionMessage.GetTargetMethod = (message, target) =>
            {
                var methodName = GetMethodName(message.MethodName, ref target);

                if (methodName == null)
                {
                    return null;
                }

                var actionMessage = new ActionMessage { MethodName = methodName };

                foreach (var parameter in message.Parameters)
                {
                    actionMessage.Parameters.Add(parameter);
                }

                return getTargetMethod(actionMessage, target);
            };

            var setMethodBinding = ActionMessage.SetMethodBinding;

            ActionMessage.SetMethodBinding = context =>
            {
                setMethodBinding(context);

                var target = context.Target;

                if (target == null)
                {
                    return;
                }

                GetMethodName(context.Message.MethodName, ref target);

                context.Target = target;
            };
        }

        private static string GetMethodName(string methodName, ref object target)
        {
            var parts = methodName.Split('.');
            var model = target;

            foreach (var propertyName in parts.Take(parts.Length - 1))
            {
                if (model == null)
                {
                    return null;
                }

                var property = model.GetType().GetPropertyCaseInsensitive(propertyName);

                if (property == null || !property.CanRead)
                {
                    return null;
                }

                model = property.GetValue(model);
            }

            target = model;

            return parts.Last();
        }
    }
}
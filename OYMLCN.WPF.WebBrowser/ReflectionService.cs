using System;
using System.Reflection;

namespace OYMLCN.WPF
{
    /// <summary>
    /// 反射方法
    /// </summary>
    public static class ReflectionService
    {
        /// <summary>
        /// 绑定标识
        /// </summary>
        public readonly static BindingFlags BindingFlags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy |
            BindingFlags.CreateInstance;
        /// <summary>
        /// 反射属性
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object ReflectGetProperty(this object target, string propertyName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (propertyName.IsNullOrEmpty())
                throw new ArgumentException("propertyName不能为空", "propertyName");

            var propertyInfo = target.GetType().GetProperty(propertyName, BindingFlags);
            if (propertyInfo == null)
                throw new ArgumentException(string.Format("未能从 '{0}' 中找到 '{1}'", propertyName, target.GetType()));
            return propertyInfo.GetValue(target, null);
        }
        /// <summary>
        /// 反射方法
        /// </summary>
        /// <param name="target"></param>
        /// <param name="methodName"></param>
        /// <param name="argTypes"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ReflectInvokeMethod(this object target, string methodName, Type[] argTypes, object[] parameters)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (methodName.IsNullOrEmpty())
                throw new ArgumentException("methodName 不能为空", "methodName");

            var methodInfo = target.GetType().GetMethod(methodName, BindingFlags, null, argTypes, null);
            if (methodInfo == null)
                throw new ArgumentException(string.Format("未能从 '{0}' 中找到 '{1}' 方法", methodName, target.GetType()));
            return methodInfo.Invoke(target, parameters);
        }
    }
}

using System.Collections;
using System.Reflection;

namespace GeneriTree.ClassConverter
{
    public static class Converter
    {
        /// <summary>
        /// Helps to convert any class list to tree-view compatible class list
        /// </summary>
        /// <typeparam name="TSource">Source class list type</typeparam>
        /// <typeparam name="TTarget">Target class list type</typeparam>
        /// <param name="source">List of source objects</param>
        /// <param name="sourceTargetFieldDictionary">Dictionary for properties for source and target class match</param>
        /// <returns></returns>
        public static List<TTarget> Convert<TSource, TTarget>(
                List<TSource> source,
                Dictionary<string, string> sourceTargetFieldDictionary
             ) where TTarget : new()
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var targetProperties = typeof(TTarget).GetProperties().ToDictionary(p => p.Name);

            var generatedList = new List<TTarget>();

            foreach (var item in source)
            {
                var targetObject = new TTarget();

                foreach (var sourceProperty in sourceProperties)
                {
                    PropertyInfo targetProperty;
                    if (sourceTargetFieldDictionary.TryGetValue(sourceProperty.Name, out string targetPropertyName))
                    {
                        targetProperty = targetProperties.ContainsKey(targetPropertyName) ? targetProperties[targetPropertyName] : null;
                    }
                    else
                    {
                        targetProperty = targetProperties.ContainsKey(sourceProperty.Name) ? targetProperties[sourceProperty.Name] : null;
                    }

                    if (targetProperty != null && targetProperty.CanWrite)
                    {
                        targetProperty.SetValue(targetObject, sourceProperty.GetValue(item));
                    }
                }

                generatedList.Add(targetObject);
            }

            return generatedList;
        }
    }
}

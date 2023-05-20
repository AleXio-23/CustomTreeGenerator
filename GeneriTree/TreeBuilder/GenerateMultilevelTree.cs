using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneriTree.TreeBuilder
{
    public static class GenerateMultilevelTree
    {
        public static List<T> Generate<T>(
               List<T> items,
               Func<T, object> idAccessor,
               Func<T, object?> parentIdAccessor,
               Action<T, List<T>> childrenSetter,
               Func<T, int>? sortIndexAccessor = null,
               int maxDepth = 100
           ) where T : class
        {
            var topLevelItems = items.Where(item => parentIdAccessor(item) == null).ToList();
            var result = new List<T>();

            foreach (var topLevelItem in topLevelItems)
            {
                var returnedTree = BuildTree.Build(
                            items,
                            idAccessor(topLevelItem),
                            idAccessor,
                            parentIdAccessor,
                            childrenSetter,
                            sortIndexAccessor,
                            maxDepth: maxDepth
                        );
                if (returnedTree != null)
                {
                    result.Add(returnedTree);
                }
            }

            return result;
        }
    }
}

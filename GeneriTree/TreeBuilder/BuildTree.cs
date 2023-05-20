using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneriTree.TreeBuilder
{
    internal static class BuildTree
    {
        internal static T? Build<T>(
               List<T> items,
               object? parentId,
               Func<T, object> idAccessor,
               Func<T, object?> parentIdAccessor,
               Action<T, List<T>> childrenSetter,
               Func<T, int>? sortIndexAccessor = null,
               int depth = 0,
               int maxDepth = 100
           ) where T : class
        {
            if (items == null || !items.Any() || depth >= maxDepth)
                return null;

            var node = items.FirstOrDefault(item => idAccessor(item).Equals(parentId));
            if (node == null)
                return null;

            var children = new List<T>();


            foreach (var item in items.Where(item => parentIdAccessor(item)?.Equals(parentId) == true))
            {
                var child = Build(items, idAccessor(item), idAccessor, parentIdAccessor, childrenSetter, sortIndexAccessor, depth + 1, maxDepth);
                if (child != null)
                {
                    children.Add(child);
                }
            }

            if (sortIndexAccessor != null)
            {
                children = children.OrderBy(x => sortIndexAccessor(x)).ToList();
            }

            childrenSetter(node, children);
            return node;
        }
    }
}

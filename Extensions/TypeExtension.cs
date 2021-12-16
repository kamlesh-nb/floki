namespace Floki.Extensions
{
  using System;
  using System.Linq;
  public static class TypeExtensions
  {
    public static string GetTypeName(this Type type)
    {
      var typeName = string.Empty;

      if (type.IsGenericType)
      {
          var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
          typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
      }
      else
      {
          typeName = type.Name;
      }
      return typeName;
    }

    public static string GetTypeName(this object @object)
    {
      return @object.GetType().GetTypeName();
    }
  }
}
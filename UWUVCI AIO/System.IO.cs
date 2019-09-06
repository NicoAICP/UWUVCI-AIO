using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System.IO
{
  public static class DirectoryInfoExtensions
  {
    public static void CopyTo(this DirectoryInfo source, DirectoryInfo target)
    {
      if (!target.Exists)
        target.Create();

      foreach (var file in source.GetFiles())
        file.CopyTo(Path.Combine(target.FullName, file.Name), true);

      foreach (var subdir in source.GetDirectories())
        subdir.CopyTo(target.CreateSubdirectory(subdir.Name));
    }
       
    }
}
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles
{
  [Serializable]
  public class BaseFile
  {
    public string ID { get; set; }
    public string Name { get; set; }
    public string FileType { get; set; }
    public string FilePath { get; set; }
    public string Instructions { get; set; }

    public BaseFile() { }
    public BaseFile(BaseFile baseFile)
    {
      FileType = baseFile.FileType;
      ID = baseFile.ID;
      Name = baseFile.Name;
      FilePath = baseFile.FilePath;
      Instructions = baseFile.Instructions;
    }
  }
}

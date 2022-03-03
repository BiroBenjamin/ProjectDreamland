using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ProjectDreamland.Data {
    class Map {
        private FileManager _fileManager;
        private string _fileName;
        private MapData _mapData;

        public List<Sprite> Components { get; private set; }

        public Map(string fileName) {
            _fileName = fileName;
        }
        public void Initialize(ContentManager Content) {
            Components = new List<Sprite>();
            _fileManager = new FileManager(_fileName);
            _mapData = new MapData();

            _fileManager.Write();
            // Read map data from file
            Read();
            Load(Content);
        }
        public void Read() {
            using (Stream stream = File.Open(_fileName, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    _mapData.spriteCounter = reader.ReadInt32();
                    _mapData.spriteTypes = new string[_mapData.spriteCounter];
                    _mapData.spriteNames = new string[_mapData.spriteCounter];
                    _mapData.spritePositions = new Vector2[_mapData.spriteCounter];
                    _mapData.spriteIsCollideable = new bool[_mapData.spriteCounter];
                    for (int i = 0; i < _mapData.spriteCounter; i++) {
                        _mapData.spriteTypes[i] = reader.ReadString();
                        _mapData.spriteNames[i] = reader.ReadString();
                        int x = reader.ReadInt32(); int y = reader.ReadInt32();
                        _mapData.spritePositions[i] = new Vector2(x, y);
                        //Debug.WriteLine(x + " - " + y);
                        _mapData.spriteIsCollideable[i] = reader.ReadBoolean();
                    }
                }
            }
        }
        private void Load(ContentManager Content) {
            for (int i = 0; i < _mapData.spriteCounter; i++) {
                switch (_mapData.spriteTypes[i]) {
                    case "tile":
                        Components.Add(
                            new Sprite(
                                Content.Load<Texture2D>(
                                    _mapData.spriteNames[i]),
                                _mapData.spriteIsCollideable[i]) { Position = _mapData.spritePositions[i] });
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

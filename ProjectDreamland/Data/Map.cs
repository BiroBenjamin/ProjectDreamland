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
        public MapData mapData;

        public List<Sprite> Components { get; private set; }

        public Map() {
            mapData = new MapData();
        }
        public void Initialize(ContentManager Content) {
            Components = new List<Sprite>();

            // Read map data from file
            //Read();
            Load(Content);
        }
        /*public void Read() {
            using (Stream stream = File.Open(_fileName, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    
                }
            }
        }*/
        private void Load(ContentManager Content) {
            //Console.WriteLine(_mapData.spriteCounter);
            for (int i = 0; i < mapData.spriteCounter; i++) {
                switch (mapData.spriteTypes[i]) {
                    case "tile":
                        Components.Add(
                            new Sprite(
                                Content.Load<Texture2D>(
                                    mapData.spriteNames[i]),
                                mapData.spriteIsCollideable[i]) { Position = mapData.spritePositions[i] });
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

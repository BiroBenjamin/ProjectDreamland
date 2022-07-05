using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ProjectDreamland.Data {
    class FileManager {
        private string _fileName;
        private Map map;

        public FileManager(string fileName) {
            _fileName = fileName;
            map = new Map();
        }

        public void Write() {
            using (Stream stream = File.Open(_fileName, FileMode.Append)) {
                using (BinaryWriter writer = new BinaryWriter(stream)) {
                    
                }
            }
        }
        public Object Read() {
            using (Stream stream = File.Open(_fileName, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    if(_fileName.Substring(_fileName.Length - 3) == "map"){
                        return ReadMap(reader);
                    }
                }
            }
            return new Map();
        }

        private Map ReadMap(BinaryReader reader){
            map.mapData.spriteCounter = reader.ReadInt32();
            //Console.WriteLine(_mapData.spriteCounter);
            map.mapData.spriteTypes = new string[map.mapData.spriteCounter];
            map.mapData.spriteNames = new string[map.mapData.spriteCounter];
            map.mapData.spritePositions = new Vector2[map.mapData.spriteCounter];
            map.mapData.spriteIsCollideable = new bool[map.mapData.spriteCounter];
            for (int i = 0; i < map.mapData.spriteCounter; i++) {
                map.mapData.spriteTypes[i] = reader.ReadString();
                map.mapData.spriteNames[i] = reader.ReadString();
                //int x = reader.ReadInt32(); int y = reader.ReadInt32();
                map.mapData.spritePositions[i] = new Vector2(reader.ReadInt32(), reader.ReadInt32());
                //Console.WriteLine(_mapData.spritePositions[i].X + " " + _mapData.spritePositions[i].Y);
                map.mapData.spriteIsCollideable[i] = reader.ReadBoolean();
            }
            return map;
        }
    }
}

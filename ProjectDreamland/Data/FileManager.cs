using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ProjectDreamland.Data {
    class FileManager {
        private string _fileName;

        public FileManager(string fileName) {
            _fileName = fileName;
        }

        public void Write() {
            using (Stream stream = File.Open(_fileName, FileMode.Append)) {
                using (BinaryWriter writer = new BinaryWriter(stream)) {
                    writer.Write(100);
                    for(int i = 0; i < 10; i++) {
                        for(int j = 0; j < 10; j++) {
                            writer.Write("tile");
                            writer.Write("Grass");
                            writer.Write(i * 32);
                            writer.Write(j * 32);
                            writer.Write(false);
                            Debug.WriteLine(i * 32 + " - " + j * 32);
                        }
                    }
                    /*writer.Write(4);
                    writer.Write("tile");
                    writer.Write("Grass");
                    writer.Write(10);
                    writer.Write(10);
                    writer.Write(false);
                    writer.Write("tile");
                    writer.Write("Grass");
                    writer.Write(-22);
                    writer.Write(-22);
                    writer.Write(false);
                    writer.Write("tile");
                    writer.Write("Grass");
                    writer.Write(42);
                    writer.Write(42);
                    writer.Write(false);
                    writer.Write("tile");
                    writer.Write("Grass");
                    writer.Write(42);
                    writer.Write(74);
                    writer.Write(false);*/
                }
            }
        }
        public void Read() {
            using (Stream stream = File.Open(_fileName, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    
                }
            }
        }
    }
}

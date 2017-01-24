using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using TSP.Entities;

namespace TSP
{
    /// <summary>
    /// Ist zustaendig eine Datei zu laden oder zu speichern
    /// </summary>
    public class FileManager
    {
        private string m_lastPath;

        public string LastPath
        {
            get { return m_lastPath; }
            set { m_lastPath = value; }
        }


        public FileManager()
        {
            LastPath = String.Empty;
        }


        public SaveEntity LoadMap(string path)
        {
            LastPath = path;
            var serializer = new XmlSerializer(typeof(SaveEntity));
            SaveEntity m = null;
            using (StreamReader reader = new StreamReader(path))
            {
                var read = serializer.Deserialize(reader);
                m = (SaveEntity)Convert.ChangeType(read, typeof(SaveEntity));
            }
            return m;
        }


        public void SaveMap(SaveEntity map, string path)
        {
            LastPath = path;
            XmlSerializer serializer = new XmlSerializer(typeof(SaveEntity));
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, map);
            }
        }


        public List<Point> LoadPoints(string path)
        {
            LastPath = path;
            var points = new List<Point>();
            var read = File.ReadLines(path);
            foreach (var line in read)
            {
                var parts = line.Split(' ');
                if (parts.Count() > 2)
                {
                    int index = -1;
                    int x = -1;
                    int y = -1;
                    if (Int32.TryParse(parts[1], out x) &&
                        Int32.TryParse(parts[2], out y))
                    {
                        var point = new Point(x, y);
                        if (Int32.TryParse(parts[0], out index))
                            point.Index = index;
                        points.Add(point);
                    }
                }
            }
            return points;
        }

    }
}

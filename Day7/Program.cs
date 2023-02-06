using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunProgram();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message?.ToString());
            }
        }

        public class SFile
        {
            public SFile(string name, int size)
            {
                this.Name = name;
                this.Size = size;
            }
            public string Name { get; set; }
            public int Size { get; set; }
        }

        public class SDirectory
        {
            public SDirectory(string name)
            {
                this.Name = name;
                this.Directories = new List<SDirectory>();
                this.Files = new List<SFile>();
            }
            public string Name { get; set; }
            public List<SFile> Files { get; set; }
            public List<SDirectory> Directories { get; set; }
            private long? _totalSize = null;
            public long TotalSize
            {
                get
                {
                    if (_totalSize != null)
                    {
                        return _totalSize.Value;
                    }

                    long size = 0;

                    foreach (var file in Files)
                    {
                        size += file.Size;
                    }

                    foreach (var directory in Directories)
                    {
                        size += directory.TotalSize;
                    }

                    _totalSize = size;
                    return _totalSize.Value;
                }
            }
        }

        
        public static void RunProgram()
        {
            SDirectory fileSys = new SDirectory("");
            string fileName = "input.txt";
            string line;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            List<string> actions = new List<string>() { "$ cd", "$ ls" };
            string root = "$ cd /";
            
            var currentRoot = new SDirectory("");
            List<SDirectory> history = new List<SDirectory>();
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    // root level
                    if(line == root)
                    {
                        SDirectory rootMenu = new SDirectory("Root");
                        fileSys = rootMenu;
                        currentRoot = rootMenu;
                    }
                    if(line.IndexOf("dir ") > -1)
                    {
                        string name = line.Substring(line.IndexOf("dir ") + 4);
                        currentRoot.Directories.Add(new SDirectory(name));
                    }
                    if (line.Any(char.IsDigit))
                    {
                        int size = int.Parse(line.Split(" ")[0]);
                        string name = line.Split(" ")[1];
                        currentRoot.Files.Add(new SFile(name, size));
                    }
                    if(line.IndexOf("$ cd ") > -1 && line != root)
                    {
                        string spath = line.Substring(line.IndexOf("$ cd ") + 5);
                        if(spath == "..")
                        {
                            currentRoot = history[history.Count - 1];
                            history = history.Take(history.Count - 1).ToList();
                        }
                        else
                        {
                            history.Add(currentRoot);
                            currentRoot = currentRoot.Directories.Where(x => x.Name == spath).FirstOrDefault();
                        }
                    }

                }
            }

            // check directories for small folder
            var smallDirectories = new List<SDirectory>();
            long maxSize = 100000;
            TraverseDirectories(fileSys, maxSize, smallDirectories);
            Console.WriteLine("Part ONE");
            Console.WriteLine(smallDirectories.Sum(d => d.TotalSize));

            Console.WriteLine("Part TWO");
            const long totalDiskSz = 70000000;
            const long requiredDiskSz = 30000000;
            long unusedSz = totalDiskSz - fileSys.TotalSize;
            long missingSz = requiredDiskSz - unusedSz;
            var toRemove = new List<SDirectory>();
            TraverseDirectoriesPartTwo(fileSys, missingSz, toRemove);
            Console.WriteLine(toRemove.Min(x => x.TotalSize));
        }

        public static void TraverseDirectories(SDirectory source, long maxSize, List<SDirectory> smallDirectories)
        {

            if (source.TotalSize <= maxSize)
            {
                smallDirectories.Add(source);
            }

            foreach (var directory in source.Directories)
            {
                TraverseDirectories(directory, maxSize, smallDirectories);
            }
        }

        public static void TraverseDirectoriesPartTwo(SDirectory source, long missingSz, List<SDirectory> toRemove)
        {
            if (missingSz <= source.TotalSize)
            {
                toRemove.Add(source);
            }

            foreach (var directory in source.Directories)
            {
                TraverseDirectoriesPartTwo(directory, missingSz, toRemove);
            }
        }
    }
}

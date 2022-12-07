using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace AdventOfCode.Y2022.Day07;

[ProblemName("No Space Left On Device")]
class Solution : Solver
{
    public object PartOne(string input)
    {
        var storage = MapStorage(input);
        var smallDirs = new List<Directory>();
        GetDirectoriesSmallerThan(100000, storage, smallDirs);
        return smallDirs.Sum(x => x.GetSize());
    }

    public object PartTwo(string input)
    {
        var storage = MapStorage(input);
        var spaceRemaining = 70000000 - storage.GetSize();
        var deletionRequired = 30000000 - spaceRemaining;
        var deletionCandidates = new List<Directory>();
        GetDirectoriesLargerThan(deletionRequired, storage, deletionCandidates);
        return deletionCandidates.Min(x => x.GetSize());
    }

    public void GetDirectoriesSmallerThan(int size, Directory directory, List<Directory> resultSizes)
    {
        var dirSize = directory.GetSize();
        if (dirSize <= size)
        {
            resultSizes.Add(directory);
        }

        foreach (var child in directory.children)
        {
            GetDirectoriesSmallerThan(size, child, resultSizes);
        }
    }
    public void GetDirectoriesLargerThan(int size, Directory directory, List<Directory> resultSizes)
    {
        var dirSize = directory.GetSize();
        if (dirSize >= size)
        {
            resultSizes.Add(directory);
        }

        foreach (var child in directory.children)
        {
            GetDirectoriesLargerThan(size, child, resultSizes);
        }
    }

    Directory MapStorage(string input)
    {
        var instructions = input.Substring(2).Split("\n$ ");
        var root = new Directory();
        var currentDir = root;
        foreach (var i in instructions)
        {
            var instructionAndOutput = i.Split('\n');
            switch (instructionAndOutput[0].Substring(0, 2))
            {
                case "cd":
                    currentDir = ChangeDirectory(currentDir, i);
                    break;
                case "ls":
                    currentDir.AddContents(instructionAndOutput.Skip(1));
                    break;
                default: throw new Exception("Unsupported terminal command");
            }
        }

        return root;
    }


    Directory ChangeDirectory(Directory curr, string instruction)
    {
        var path = instruction.Substring(3);
        return path switch
        {
            "/" => curr.GetRoot(),
            ".." => curr.parent,
            _ => curr.GetChildDir(path),
        };
    }
}

class Directory
{
    public Directory parent { set; get; }
    public HashSet<Directory> children = new HashSet<Directory>();
    public HashSet<File> files = new HashSet<File>();
    public bool isRoot;
    public string name { get; init; }

    public Directory(Directory parent, string name)
    {
        this.parent = parent;
        this.isRoot = false;
        this.name = name;
    }

    public Directory()
    {
        this.isRoot = true;
        this.name = "/";
    }

    public Directory GetChildDir(string name)
        => children.Where(x => x.name == name).First();

    public Directory GetRoot()
    {
        var curr = this;
        while (this.parent != null)
        {
            curr = this.parent;
        }

        return curr;
    }

    public void AddContents(IEnumerable<string> list)
    {
        foreach (var item in list)
        {
            var arr = item.Split(' ');
            if (arr[0] == "dir")
            {
                this.children.Add(new Directory(this, arr[1]));
            }
            else
            {
                var size = int.Parse(arr[0]);
                this.files.Add(new File(size, arr[1]));
            }
        }
    }

    public int GetSize()
        => this.children.Sum(child => child.GetSize()) + this.files.Sum(file => file.size);
}

record File(int size, string name);
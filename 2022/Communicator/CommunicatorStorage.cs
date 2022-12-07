using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AdventOfCode.Y2022;

public class CommunicatorStorage
{
    public Directory Root { get; }
    public CommunicatorStorage(string input)
    {
        this.Root = MapStorage(input);
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
                    currentDir = ChangeDirectory(currentDir, i.Substring(3));
                    break;
                case "ls":
                    currentDir.AddContents(instructionAndOutput.Skip(1));
                    break;
                default: throw new Exception("Unsupported terminal command");
            }
        }

        return root;
    }


    Directory ChangeDirectory(Directory curr, string path)
    {
        return path switch
        {
            "/" => curr.GetRoot(),
            ".." => curr.parent,
            _ => curr.GetChildDir(path),
        };
    }
    
}



public class Directory
{
    public Directory parent { get; }
    public HashSet<Directory> children { get; } = new HashSet<Directory>();
    public HashSet<File> files { get; } = new HashSet<File>();
    public string name { get; }
    private int size = 0;

    public Directory(Directory parent, string name)
    {
        this.parent = parent;
        this.name = name;
    }

    public Directory()
    {
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
    {
        if (this.size == 0)
        {
            this.size = this.children.Sum(child => child.GetSize()) + this.files.Sum(file => file.size);
        }

        return this.size;
    }
}

public record File(int size, string name);
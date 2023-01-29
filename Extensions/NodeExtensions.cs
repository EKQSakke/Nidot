﻿namespace Nidot;

using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Godot;

public static class NodeExtensions
{
    public static T GetNodeOfType<T>(this Node node) where T : class
    {
        if (node == null) return default;

        for (int i = 0; i < node.GetChildCount(); i++)
        {
            var resultNode = node.GetNode(node.GetChild(i).Name.ToString());

            if (resultNode is T result)
                return result;
        }

        return default;
    }

    public static T GetNodeFromAll<T>(this Node node) where T : class
    {
        var root = node.GetTree().Root;
        return GetResult<T>(root);
    }

    private static T GetResult<T>(Node node)
    {
        if (node is T result)
            return result;

        for (int i = 0; i < node.GetChildCount(); i++)
        {
            var childResult = GetResult<T>(node.GetChild(i));

            if (childResult is not null)
                return childResult;
        }

        return default;
    }


    public static List<T> GetChildNodesOfType<T>(this Node node) where T : class
    {
        var result = new List<T>();
        if (node == null) return default;

        foreach (var item in node.GetChildren())
            if (item is T childNode)
                result.Add(childNode);

        return result;
    }

    public static T GetAutoload<T>(this Node node) where T : class
    {
        if (node == null) return default;

        return node.GetTree().Root.GetNodeOfType<T>();
    }

    public static T AddChildOfType<T>(this Node node) where T : Node, new() {
        var child = new T();
        node.AddChild(child);
        return child;
    }
}
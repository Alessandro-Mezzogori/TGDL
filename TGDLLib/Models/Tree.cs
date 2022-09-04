namespace TGDLLib;

public class Tree
{
   public TreeNode Root { get; } 
}

public class TreeNode
{
    public List<TreeNode> Childrens { get; set; } = new();
}

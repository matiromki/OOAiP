namespace SpaceBattle.Lib;
using Hwdtech;

public class BuildDecisionTree : ICommand
{
    private string path;
    public BuildDecisionTree(string path)
    {
        this.path = path;
    }

    public void Execute()
    {
        var strategy = IoC.Resolve<Dictionary<int, object>>("SpaceBattle.GetDecisionTree");
        try
        {
            using (StreamReader reader = File.OpenText(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var record = line.Split().Select(int.Parse).ToList();
                    PutInTree(record, strategy);
                }
            }
        }
        catch (FileNotFoundException e)
        {
            throw new FileNotFoundException(e.ToString());
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }

    }

    private void PutInTree(List<int> list, IDictionary<int, object> root)
    {
        var tree = root;
        foreach (var item in list)
        {
            tree.TryAdd(item, new Dictionary<int, object>());
            tree = (Dictionary<int, object>)tree[item];
        }
    }
}
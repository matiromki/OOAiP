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
            string? line;
            using (var reader = File.OpenText(path))
            {
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

    private void PutInTree(List<int> row, IDictionary<int, object> root)
    {
        var tree = root;
        foreach (var item in row)
        {
            tree.TryAdd(item, new Dictionary<int, object>());
            tree = (Dictionary<int, object>)tree[item];
        }
    }
}
using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class DecisionTreeTests
{   
    
    public DecisionTreeTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var getDecisionTree = new Mock<IStrategy>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.GetDecisionTree", (object[] args) => getDecisionTree.Object.RunStrategy(args)).Execute();
        
    }

    [Fact]
    public void PositiveBuildDecisionTreeTest()
    {
        string path = "";
        ICommand bdt = new BuildDecisionTree(path);
        bdt.Execute();
    }
}
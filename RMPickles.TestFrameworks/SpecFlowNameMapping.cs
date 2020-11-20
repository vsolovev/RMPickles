using TechTalk.SpecFlow.Tracing;

namespace RMPickles.Core.TestFrameworks
{
	internal static class SpecFlowNameMapping
	{
	    public static string Build(string name)
	    {
	        return name.ToIdentifier();
        }
	}
}

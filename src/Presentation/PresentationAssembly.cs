using System.Reflection;

namespace REA.Accounting.Presentation;

public static class PresentationAssembly
{
    public static readonly Assembly Instance = typeof(PresentationAssembly).Assembly;
}
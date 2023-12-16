namespace GonToDoApi.Core.Elements;

public class DebugLog: Log
{
    protected override bool NoAllowWrite() => true;
}
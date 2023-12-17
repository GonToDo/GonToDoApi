namespace GonToDoApi.Core;

public abstract class DebugLog : Log
{
    protected override string Title()
    {
        return "DebugLog";
    }

    protected override bool NoAllowWrite()
    {
        return true;
    }
}
namespace RandomPairer.Common.RequestContext
{
    public interface IRequestContext
    {
        string CurrentUserAd { get; }
        string RequestId { get; }
    }
}

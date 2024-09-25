namespace AdvertBoard.Contracts.Shared;

public class PageResponse<T> 
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<T> Response { get; set; }
    
    public int TotalPages { get; set; }
}
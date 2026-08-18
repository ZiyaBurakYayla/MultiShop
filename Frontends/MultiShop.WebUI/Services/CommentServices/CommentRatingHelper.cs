using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Services.CommentServices
{
    public static class CommentRatingHelper
    {
        public static async Task<Dictionary<string, ProductRatingSummary>> GetRatingSummaries(ICommentService commentService)
        {
            var result = new Dictionary<string, ProductRatingSummary>();
            try
            {
                var comments = await commentService.GetAllCommentsAsync();
                if (comments == null)
                {
                    return result;
                }
                foreach (var comment in comments)
                {
                    if (string.IsNullOrEmpty(comment.ProductId) || !comment.Status)
                    {
                        continue;
                    }
                    if (!result.ContainsKey(comment.ProductId))
                    {
                        result.Add(comment.ProductId, new ProductRatingSummary());
                    }
                    result[comment.ProductId].Count++;
                    result[comment.ProductId].Average += comment.Rating;
                }
                foreach (var item in result)
                {
                    if (item.Value.Count > 0)
                    {
                        item.Value.Average = item.Value.Average / item.Value.Count;
                    }
                }
            }
            catch
            {
                return result;
            }
            return result;
        }
    }
}

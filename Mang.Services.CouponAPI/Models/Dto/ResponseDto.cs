namespace Mang.Services.CouponAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Resulte { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

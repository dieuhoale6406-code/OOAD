namespace OOAD.Services
{
    public enum HandleStatus
    {
        Success,
        Conflict,          // Cần user quyết định xem có ghi đè không
        GroupDecision,     // Cần user quyết định xem có tham gia nhóm không
        Error
    }

    public class ServiceResult<T>
    {
        public HandleStatus Status { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; } // Dùng để trả về ID mới hoặc thông tin bổ sung nếu cần

        public static ServiceResult<T> Ok(string msg = "Thành công") => new()
        {
            Status = HandleStatus.Success,
            Message = msg
        };

        public static ServiceResult<T> Ok(T data, string msg = "Thành công") => new()
        {
            Status = HandleStatus.Success,
            Message = msg,
            Data = data
        };

        public static ServiceResult<T> Conflict(string msg) => new()
        {
            Status = HandleStatus.Conflict,
            Message = msg
        };

        public static ServiceResult<T> AskGroup(string msg) => new()
        {
            Status = HandleStatus.GroupDecision,
            Message = msg
        };

        public static ServiceResult<T> Fail(string msg) => new()
        {
            Status = HandleStatus.Error,
            Message = msg
        };
    }
}
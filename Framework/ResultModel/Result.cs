using System.Collections.Generic;
using Framework.Enum;

namespace Framework.ResultModel
{
    public class Result
    {
        public StatusType Status { get; set; }
        public string Message { get; set; }
        public MetaData MetaData { get; set; }
        public object Data { get; set; }
        public List<Error> Errors { get; set; }
    }
}

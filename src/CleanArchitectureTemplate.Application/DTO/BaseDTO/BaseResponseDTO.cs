using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.BaseDTO
{
    public class BaseCommandResponse<T>
    {
        public BaseCommandResponse()
        {
            Errors = new List<string>();
        }
        public T ID { get; set; } = default!;
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public BaseCommandResponse(T id, bool isSucces, List<string> errors)
        {
            Success = isSucces;
            ID = id;
            if (isSucces)
                Message = "Success";
            else
                Message = "Fail";
            Errors = errors;
        }
        public BaseCommandResponse(T id, bool isSucces)
        {
            Success = isSucces;
            ID = id;
            if (isSucces)
                Message = "Success";
            else
                Message = "Fail";
            Errors = new List<string>();
        }
    }
}

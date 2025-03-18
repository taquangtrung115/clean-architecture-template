using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.BaseDTO
{
    public class BaseCommandResponse
    {
        public int ID { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public BaseCommandResponse(int id, bool isSucces, List<string> errors)
        {
            Success = isSucces;
            ID = id;
            if (isSucces)
                Message = "Success";
            else
                Message = "Fail";
            Errors = errors;
        }
        public BaseCommandResponse(int id, bool isSucces)
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

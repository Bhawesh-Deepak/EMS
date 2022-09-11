using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Helpers
{
    public static class ResponseHelper
    {
        public static string ResponseMessage(bool status, OperationType operationType)
        {
            string statusMessage = status ? "Successfully" : "Something wents wrong, Please contact admin";
            if (status)
            {
                switch (operationType)
                {
                    case OperationType.Create:
                        return $"Data created {statusMessage} !";
                    case OperationType.Update:
                        return $"Data Updated {statusMessage} !";
                    case OperationType.Delete:
                        return $"Data Deleted {statusMessage} !";

                }
            }
            return statusMessage;
        }
    }
}

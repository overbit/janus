using System;

namespace overapp.janus.Models.Dtos.Response
{
    public class TransactionResult
    {
        public Guid Guid { get; set; }

        public bool IsSuccess { get; set; }
    }
}

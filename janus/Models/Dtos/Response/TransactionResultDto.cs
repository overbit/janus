namespace overapp.janus.Models.Dtos.Response
{
    public class TransactionResultDto
    {
        /// <summary>
        /// 32 char string representing the payment / transaction id
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Status of the payment / transaction
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}

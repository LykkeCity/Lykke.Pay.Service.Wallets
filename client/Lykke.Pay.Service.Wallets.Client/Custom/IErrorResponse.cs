using System.Collections.Generic;

namespace Lykke.Pay.Service.Wallets.Client.Custom
{
    public interface IErrorResponse
    {
        /// <summary>
        /// </summary>
        IDictionary<string, IList<string>> ErrorMessages { get; }
    }
}
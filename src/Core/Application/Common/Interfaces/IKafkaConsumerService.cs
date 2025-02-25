using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IKafkaConsumerService
    {
        Task<List<T>> ConsumeBatchAsync<T>(string topic, string groupId, int batchSize, CancellationToken cancellationToken);
    }
}

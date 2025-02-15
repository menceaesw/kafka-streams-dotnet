using System;

namespace Streamiz.Kafka.Net.Processors.Public
{
    internal class WrappedTransformer<K, V, K1, V1> : ITransformer<K, V, K1, V1>, ICloneableProcessor
    {
        private readonly Func<Record<K, V>, Record<K1, V1>> transformer;

        public WrappedTransformer(Func<Record<K, V>, Record<K1, V1>> transformer)
        {
            this.transformer = transformer;
        }

        public void Init(ProcessorContext context)
        { }

        public Record<K1, V1> Process(Record<K, V> record)
            => transformer.Invoke(record);

        public void Close()
        { }

        public object Clone()
            => new WrappedTransformer<K, V, K1, V1>(transformer);
    }
}
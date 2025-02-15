﻿using Streamiz.Kafka.Net.Crosscutting;
using Streamiz.Kafka.Net.State.Supplier;
using System;

namespace Streamiz.Kafka.Net.State.InMemory
{
    /// <summary>
    /// A store supplier that can be used to create one or more <see cref="InMemoryWindowStore"/> instances.
    /// </summary>
    public class InMemoryWindowStoreSupplier : IWindowBytesStoreSupplier
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="storeName">Name of store</param>
        /// <param name="retention">Retention period of data</param>
        /// <param name="size">Size of window</param>
        public InMemoryWindowStoreSupplier(string storeName, TimeSpan retention, long? size)
        {
            Name = storeName;
            Retention = (long) retention.TotalMilliseconds;
            WindowSize = size;
        }
        
        /// <summary>
        /// Return a String that is used as the scope for metrics recorded by Metered stores.
        /// </summary>
        public string MetricsScope => "in-memory-window";

        /// <summary>
        /// Name of state store
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Window size of state store
        /// </summary>
        public long? WindowSize { get; set; }

        /// <summary>
        /// Retention period of state store
        /// </summary>
        public long Retention { get; set; }

        /// <summary>
        /// Return a new <see cref="IWindowStore{K, V}"/> instance.
        /// </summary>
        /// <returns>Return a new <see cref="IWindowStore{K, V}"/> instance.</returns>
        public IWindowStore<Bytes, byte[]> Get()
            => new InMemoryWindowStore(Name, TimeSpan.FromMilliseconds(Retention), WindowSize.Value);

    }
}
